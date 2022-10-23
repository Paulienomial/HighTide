using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : Ability
{
    public static int[] dmgPerTick = {12, 40, 70};//dps is dmgPerTick*2
    public static float timeToCast = 4;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void go(){
        if(!this) return;//if awaiting deletion
        
        StartCoroutine(bonfire());
    }

    IEnumerator bonfire(){
        //wait X seconds before casting
        yield return new WaitForSeconds(timeToCast);

        //wait for a target
        while(noTargets()){}

        //the following code might not be thread safe
        //find closest enemy and create bonfire beneath it
        GameObject closestEnemy = findClosestEnemy();
        if(closestEnemy!=null){
            float x = closestEnemy.transform.position.x;
            float y = closestEnemy.transform.position.y;
            GameObject fire = Instantiate(gameObject.GetComponent<InstanceAbility>().bonfireObject, new Vector2(x, y), Quaternion.identity);
            fire.GetComponent<Fire>().dmg = dmgPerTick[w.lvlIndex()];
            fire.GetComponent<Fire>().activate();
            AudioSystem.curr.createAndPlaySound("fireWhoosh",1,1);
            //Wait X seconds then destroy fire
            yield return new WaitForSeconds(30);
            Destroy(fire);
        }else{

        }
    }
}
