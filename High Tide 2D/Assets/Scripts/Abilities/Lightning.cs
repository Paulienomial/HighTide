using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : Ability
{
    public int damage = 99999;
    float timeToCast = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void go(){
        if(!this){
            return;//if awaiting deletion
        }
        StartCoroutine(castLightning());
    }

    IEnumerator castLightning(){
        //wait X seconds before casting
        yield return new WaitForSeconds(timeToCast);

        //wait for a target
        while(noTargets()){}

        //the following code might not be thread safe
        //find closest enemy and create bonfire beneath it
        GameObject enemy = findHighestHPEnemy();
        if(enemy!=null){
            float x = enemy.transform.position.x;
            float y = enemy.transform.position.y;
            GameObject lightning = Instantiate(gameObject.GetComponent<InstanceAbility>().lightning, new Vector2(x, y), Quaternion.identity);
            enemy.GetComponent<FightManager>().takeDamage( fm.getModifiedDamage(damage) );
            AudioSystem.curr.createAndPlaySound("lightning", 1, 1);
        }else{

        }
    }

    
}
