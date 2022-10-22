using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnTrees : Ability
{
    public static int[] spawnAmounts = {1, 3, 6};//dps is dmgPerTick*2
    public static float timeToCast = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void go(){
        if(!this) return;//if awaiting deletion
        
        StartCoroutine(spawnTrees());
    }

    IEnumerator spawnTrees(){
        //wait X seconds before casting
        yield return new WaitForSeconds(timeToCast);

        for(int i=0; i<spawnAmounts[w.getLevel()-1]; i++){
            global.defenders.Add( Instantiate(global.warriorPrefab, new Vector2(gameObject.transform.position.x+1.5f, gameObject.transform.position.y+.75f-.75f*i), Quaternion.identity) );
            GameObject treeDude = global.defenders.Last();
            treeDude.GetComponent<Warrior>().setWarrior("Tree warrior");
            treeDude.GetComponent<Warrior>().setHealth( treeDude.GetComponent<Warrior>().attributes.hp+w.lvlIndex()*50 );
        }
    }
}
