using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarriorAbility : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Warrior w = gameObject.GetComponent<Warrior>();
        WarriorAttributes.attr a = w.attributes;
        if(a.ability=="Delicious meal"){
            Events.curr.onWaveStart+=giveMeal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void giveMeal(){

    }
}
