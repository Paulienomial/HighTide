using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Ability
{
    public float delay = 2f;
    void Start()
    {
        delay=2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void go(){
        if(!this) return;//if awaiting deletion

        StartCoroutine( teleport() );
    }

    IEnumerator teleport(){
        yield return new WaitForSeconds(delay);
        if(GetComponent<FightManager>()!=null){
            AudioSystem.curr.createAndPlaySound("teleport", 1, 0.5f);
            GetComponent<FightManager>().pauseUnit();
            float x = transform.position.x;
            float y = transform.position.y;
            transform.position = new Vector2(-4, y); 
            GetComponent<FightManager>().unpauseUnit();
        }
    }
}
