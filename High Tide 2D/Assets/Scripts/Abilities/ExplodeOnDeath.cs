using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDeath : Ability
{
    public int damage=999999;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void go(GameObject g){
        if(!this) return;//if awaiting deletion

        if(g==gameObject){
            StartCoroutine( explodeOnDeath(g) );
        }
    }

    IEnumerator explodeOnDeath(GameObject g){
        float x = transform.position.x;
        float y = transform.position.y;
        GameObject explosion = Instantiate(gameObject.GetComponent<InstanceAbility>().explode, new Vector2(x, y), Quaternion.identity);
        explosion.GetComponent<DeathExplode>().damage = gameObject.GetComponent<FightManager>().getModifiedDamage(damage);
        explosion.GetComponent<DeathExplode>().activate();
        AudioSystem.curr.createAndPlaySound("jellyfishBoom", 1, 1);
        yield return new WaitForSeconds(.25f);
        Destroy(explosion);
    }
}
