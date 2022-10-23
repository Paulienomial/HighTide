using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarriorAttributes : MonoBehaviour
{
    [Serializable]
    public class attr{
        /*public attr(attr a){//copy constructor
            name=a.name;
            damage=a.damage;
            hp=a.hp;
            tier=a.tier;
            price=a.price;
            isFriendly=a.isFriendly;
            isRanged=a.isRanged;
            mergeCount=a.mergeCount;
        }*/
        public attr clone(){
            attr c=new attr();
            c.name=name;
            c.damage=damage;
            c.hp=hp;
            c.tier=tier;
            c.price=price;
            c.isFriendly=isFriendly;
            c.isRanged=isRanged;
            c.mergeCount=mergeCount;
            for(int i=0; i<3; i++){
                c.descriptions[i] = descriptions[i];
            }
            c.bounty=bounty;
            c.cityDamage=cityDamage;
            c.moveSpeed=moveSpeed;
            c.attackSound=attackSound;
            c.attackSoundAmount=attackSoundAmount;
            c.ability=ability;
            c.projectile = projectile;
            c.attackModifiers = new List<string>();
            foreach(string am in attackModifiers){
                c.attackModifiers.Add(am);
            }
            c.attackPitch = attackPitch;
            c.attackVolume = attackVolume;

            return c;
        }


        public string name="foot soldier";
        public int damage=30;
        public int hp=300;
        public int tier=1;
        public int price=3;
        public bool isFriendly = true;
        public bool isRanged = false;
        public int mergeCount=1;//amount of units combine to make this unit
        public string[] descriptions = {
            "Basic melee unit",
            "Lvl2 basic melee unit",
            "Lvl3 basic melee unit"
        };
        public int bounty=0;//bounty for killing the unit
        public int cityDamage=1;//the amount of damage this unit does to the city
        public float moveSpeed=1f;
        public string attackSound="sword";
        public string abilitySound="";
        public int attackSoundAmount=1;
        public string ability="";
        public string projectile = "arrow";
        public List<string> attackModifiers = new List<string>();
        public List<Modifier> modifiers = new List<Modifier>();
        public float attackPitch = 1f;
        public float attackVolume=.5f;
    }
}
