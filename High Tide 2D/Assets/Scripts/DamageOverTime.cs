using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    public int damage=5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDOT(int dmg, float interval){
        damage=dmg;
        InvokeRepeating("takeDamage",0,interval);
    }

    public void stopDOT(){
        CancelInvoke("takeDamage");
    }

    void takeDamage(){
        gameObject.GetComponent<FightManager>().takeDamage(damage);
    }
}
