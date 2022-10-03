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
            c.description=description;
            c.description2=description2;
            c.description3=description3;
            c.bounty=bounty;
            c.cityDamage=cityDamage;
            c.moveSpeed=moveSpeed;
            c.attackSound=attackSound;
            c.attackSoundAmount=attackSoundAmount;
            c.ability=ability;
            c.damagePerTick=damagePerTick;

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
        public string description = "Basic melee unit";
        public string description2 = "Lvl2 Basic melee unit";
        public string description3 = "Lvl3 Basic melee unit";
        public int bounty=1;//bounty for killing a unit
        public int cityDamage=1;
        public float moveSpeed=1f;
        public string attackSound="sword";
        public int attackSoundAmount=1;
        public string ability="";
        public int damagePerTick=0;
    }
}
