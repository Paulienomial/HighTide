using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    //public
    public GameObject bonfireObject;

    
    //private
    Warrior w;
    Global global = Global.curr;
    
    void Awake(){
        w = gameObject.GetComponent<Warrior>();
        Events.curr.onWaveStart += startAbility;
        Events.curr.onWaveComplete += waveComplete;
        startAbilityEnemy();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void waveComplete(){
        if(!this){
            return;
        }
        if(w.attributes.name=="Tree warrior"){
            Global.curr.defenders.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void startAbility(){
        if(!this){
            return;
        }
        Debug.Log("Wave start ability");
        if(w.attributes.isFriendly){
            Debug.Log(w.attributes.ability);
            if(w.attributes.ability=="Bonfire"){
                Debug.Log("Bonfire");
                StartCoroutine(bonfire());
            }
            if(w.attributes.ability=="SpawnTrees"){
                StartCoroutine(spawnTrees());
            }
        }
    }

    public void startAbilityEnemy(){
        if(w.attributes.ability=="Bonfire"){
            StartCoroutine(bonfire());
        }
    }

    IEnumerator bonfire(){
        //wait X seconds before casting
        yield return new WaitForSeconds(4);

        //wait for a target
        while(noTargets()){}

        //the following code might not be thread safe
        //find closest enemy and create bonfire beneath it
        GameObject closestEnemy = findClosestEnemy();
        if(closestEnemy!=null){
            float x = closestEnemy.transform.position.x;
            float y = closestEnemy.transform.position.y;
            GameObject fire = Instantiate(bonfireObject, new Vector2(x, y), Quaternion.identity);
            fire.GetComponent<Fire>().dmg = w.attributes.damagePerTick;
            fire.GetComponent<Fire>().activate();
            AudioSystem.curr.createAndPlaySound("fireLit");
            yield return new WaitForSeconds(5);
            Destroy(fire);
        }else{

        }
    }

    IEnumerator spawnTrees(){
        //wait X seconds before casting
        yield return new WaitForSeconds(0);

        for(int i=0; i<gameObject.GetComponent<Warrior>().getLevel()+1; i++){
            global.defenders.AddLast( Instantiate(global.warriorPrefab, new Vector2(gameObject.transform.position.x+1.5f, gameObject.transform.position.y+.5f-.5f*i), Quaternion.identity) );
            GameObject treeDude = global.defenders.Last.Value;
            treeDude.GetComponent<Warrior>().setWarrior("Tree warrior");
        }
    }

    bool noTargets(){
        return global.enemies.Count==0 && global.gamePhase=="fight";
    }

    public GameObject findClosestEnemy(){
        LinkedList<GameObject> enemies;
        if(w.attributes.isFriendly){
            enemies=Global.curr.enemies;
        }else{
            enemies=Global.curr.defenders;
        }

        float minDistance = float.MaxValue;
        GameObject closestEnemy=null;
        foreach(GameObject enemy in Global.curr.enemies){
            float currDistance = Vector2.Distance( gameObject.transform.position, enemy.transform.position);
            if(currDistance<minDistance){
                closestEnemy=enemy;
                minDistance=currDistance;
            }
        }
        return closestEnemy;
    }
}
