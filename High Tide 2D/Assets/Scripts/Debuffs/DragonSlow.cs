using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSlow : Modifier
{
    public static int[] vals={30,40,50};
    public DragonSlow(string n, int l, GameObject a) : base(n,l,a){
        
    }

    public override void add(){
        attachedTo.GetComponent<Warrior>().attributes.modifiers.Add(this);
        attachedTo.GetComponent<FightManager>().msMultiplier *= (100-vals[lvl-1])/100f;//for slow amount 30, msMultiplier=.7
    }

    public override void remove(){
        attachedTo.GetComponent<FightManager>().msMultiplier /= (100-vals[lvl-1])/100f;//for slow amount 30, msMultiplier=.7
        attachedTo.GetComponent<Warrior>().attributes.modifiers.Remove(this);
    }
}
