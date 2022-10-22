using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InstanceAbility : MonoBehaviour
{
    Ability myAbility;
    public Warrior w;
    public WarriorAttributes.attr a;
    public Global global;
    public GameObject bonfireObject;
    public GameObject explode;
    public GameObject lightning;
    public GameObject mediumJ;
    public GameObject tinyJ;
    public GameObject greenRing;
    void Start()
    {
        w = gameObject.GetComponent<Warrior>();
        a = w.attributes;
        global= Global.curr;
        Events.curr.onWaveComplete += waveComplete;

        if(a.ability=="Delicious meal"){
            myAbility = gameObject.AddComponent<DeliciousMeal>() as DeliciousMeal;
            Events.curr.onWaveStart += myAbility.go;
        }else if(a.ability=="Egg"){
            myAbility = gameObject.AddComponent<Egg>() as Egg;
            Events.curr.onWaveComplete += myAbility.go;
            Events.curr.onWaveStart += myAbility.go2;
            Events.curr.onUpgradeDefender += myAbility.go3;
        }else if(a.ability=="Bonfire"){
            myAbility = gameObject.AddComponent<Bonfire>() as Bonfire;
            Events.curr.onWaveStart += myAbility.go;
        }else if(a.ability=="Spawn trees"){
            myAbility = gameObject.AddComponent<SpawnTrees>() as SpawnTrees;
            Events.curr.onWaveStart += myAbility.go;
        }else if(a.ability=="Explode on death"){
            myAbility = gameObject.AddComponent<ExplodeOnDeath>() as ExplodeOnDeath;
            Events.curr.onDie += myAbility.go;
        }
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
}
