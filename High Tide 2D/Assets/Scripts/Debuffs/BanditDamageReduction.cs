using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditDamageReduction : Modifier
{
    public static int[] vals={20,45,70};
    public BanditDamageReduction(int l, GameObject a) : base("Bandit damage reduction",l,a){
        
    }

    public override void add(){
        attachedTo.GetComponent<Warrior>().attributes.modifiers.Add(this);
        attachedTo.GetComponent<FightManager>().dmgMultiplier *= (100-vals[lvl-1])/100f;//for val 10, dmgMultiplier will be .9
    }

    public override void remove(){
        attachedTo.GetComponent<FightManager>().dmgMultiplier /= (100-vals[lvl-1])/100f;
        attachedTo.GetComponent<Warrior>().attributes.modifiers.Remove(this);
    }
}
