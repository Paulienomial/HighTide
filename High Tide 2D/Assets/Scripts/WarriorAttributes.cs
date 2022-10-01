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
            c.attacksound = attacksound;
            c.description=description;
            c.bounty=bounty;
            c.cityDamage=cityDamage;

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
        public string attacksound;
        public string description = "Basic melee unit";
        public int bounty=1;//bounty for killing a unit
        public int cityDamage=1;
    }
}
