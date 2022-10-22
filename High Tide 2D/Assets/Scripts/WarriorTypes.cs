using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarriorTypes : MonoBehaviour
{
    public TextAsset warriorJSON;

    [Serializable]
    public class WarriorAttributesList{
        public WarriorAttributes.attr[] warriors;//c# version of json file
    }

    public WarriorAttributesList wList;

    public static WarriorTypes curr;//singleton

    void Awake(){
        curr=this;//singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        //Read json data into c# data structure
        wList = new WarriorAttributesList();
        wList = JsonUtility.FromJson<WarriorAttributesList>(warriorJSON.text);
        wList = JsonUtility.FromJson<WarriorAttributesList>(warriorJSON.text);
        foreach(WarriorAttributes.attr warrior in WarriorTypes.curr.wList.warriors){
            setDescription(warrior);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public WarriorAttributes.attr find(string n){
        foreach(WarriorAttributes.attr warrior in WarriorTypes.curr.wList.warriors){
            if(warrior.name==n){
                return warrior;
            }
        }
        return null;
    }

    public void setDescription(WarriorAttributes.attr wa){
        if(wa.ability=="Aura ranger buff"){
            for(int i=0; i<3; i++){
                wa.descriptions[i] = "Give all defenders +" + AuraRangerBuff.vals[i] + "DMG. Effect does not stack";
            }
        }else if(wa.ability=="Spawn trees"){
            for(int i=0; i<3; i++){
                string warriorString = "warrior";
                if(i>0) warriorString = "warriors";
                wa.descriptions[i] = "Spawn " + SpawnTrees.spawnAmounts[i] + " tree " + warriorString + " to fight for you";
            }
        }else if(wa.ability=="Bonfire"){
            for(int i=0; i<3; i++){
                wa.descriptions[i] = "Creates a bonfire that does " + (Bonfire.dmgPerTick[i]*2).ToString() + " damage per second. Does not stack";
            }
        }else if(wa.ability=="Delicious meal"){
            for(int i=0; i<3; i++){
                wa.descriptions[i] = "At round start, permanantly give the unit ahead +" + DeliciousMeal.dmgAmounts[i] + "DMG and +" + DeliciousMeal.hpAmounts[i] + "HP";
            }
        }else if(wa.name=="Blue dragon"){
            for(int i=0; i<3; i++){
                wa.descriptions[i] = "Dragon with splash attack. Attacks slow enemy movement by " + DragonSlow.vals[i] + "%";
            }
        }else if(wa.name=="Green dragon"){
            for(int i=0; i<3; i++){
                wa.descriptions[i] = "Dragon with splash attack. Attacks reduce enemy damage by " + DragonDamageReduction.vals[i] + "%";
            }
        }else if(wa.name=="Rogue ranger"){
            for(int i=0; i<3; i++){
                wa.descriptions[i] = "If this is your only ranger, then it deals " + LoneRangerBuff.vals[i] + "X damage";
            }
        }else if(wa.name=="Bandit ranger"){
            for(int i=0; i<3; i++){
                wa.descriptions[i] = "Reduces target's damage output by " + BanditDamageReduction.vals[i] + "%. Effect does not stack";
            }
        }
    }
}
