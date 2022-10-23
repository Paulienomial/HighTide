using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup
{
    public string name;
    public int count;
    public int bundleSize=3;//amount of enemies that spawn at a single instance
    public float freezeTime=1;
    public float spawnInterval=3;
    public float hpMultiplier=1;
    public float dmgMultiplier=1;
    public void multiplyHP(float hpMult){
        foreach(WarriorAttributes.attr wa in WarriorTypes.curr.wList.warriors){
            if(wa.name==name){
                wa.hp = Mathf.RoundToInt(wa.hp*hpMult);
            }
        }
    }
    public void multiplyDMG(float dmgMult){
        foreach(WarriorAttributes.attr wa in WarriorTypes.curr.wList.warriors){
            if(wa.name==name){
                wa.hp = Mathf.RoundToInt(wa.hp*dmgMult);
            }
        }
    }

    public int getUnitHP(){
        return Mathf.RoundToInt(WarriorTypes.curr.find(name).hp * hpMultiplier);
    }
    
    public int getUnitDMG(){
        return Mathf.RoundToInt( WarriorTypes.curr.find(name).damage * dmgMultiplier );
    }
}
