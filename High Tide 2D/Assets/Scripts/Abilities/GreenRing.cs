using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenRing : Ability
{
    public int damage=275;
    public float freezeTime = 5f;
    public float interval = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void go(){
        if(!this) return;//if awaiting deletion

        StartCoroutine( greenRing() );
    }

    IEnumerator greenRing(){
        float waitTime = freezeTime;
        while(Global.curr.gamePhase=="fight"){
            yield return new WaitForSeconds(waitTime);
            float x = transform.position.x;
            float y = transform.position.y;
            GameObject greenRing = Instantiate(gameObject.GetComponent<InstanceAbility>().greenRing, new Vector2(x, y), Quaternion.identity);
            if(greenRing!=null){
                greenRing.GetComponent<RingHit>().damage = gameObject.GetComponent<FightManager>().getModifiedDamage(damage);
                greenRing.GetComponent<RingHit>().activate();
                AudioSystem.curr.createAndPlaySound("electricShock",1,.8f);
            }
            waitTime = interval;
        }
    }
}
