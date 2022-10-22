using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraRangerBuff : Modifier
{
    public static int[] vals={15,45,120};
    public AuraRangerBuff(int l, GameObject a) : base("Aura ranger buff",l,a){
        
    }

    public override void add(){
        attachedTo.GetComponent<Warrior>().attributes.modifiers.Add(this);
        attachedTo.GetComponent<FightManager>().plusDmg += vals[lvl-1];
    }

    public override void remove(){
        attachedTo.GetComponent<FightManager>().plusDmg -= vals[lvl-1];
        attachedTo.GetComponent<Warrior>().attributes.modifiers.Remove(this);
    }
}
