using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonDamageReduction : Modifier
{
    public static int[] vals={20,30,40};
    public DragonDamageReduction(int l, GameObject a) : base("Dragon damage reduction",l,a){
        
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
