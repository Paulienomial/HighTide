using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoneRangerBuff : Modifier
{
    public static float[] vals={1.5f, 2f, 2.5f};
    public LoneRangerBuff(int l, GameObject a) : base("Lone ranger buff",l,a){
        
    }

    public override void add(){
        attachedTo.GetComponent<Warrior>().attributes.modifiers.Add(this);
        attachedTo.GetComponent<FightManager>().dmgMultiplier *= vals[lvl-1];
    }

    public override void remove(){
        attachedTo.GetComponent<FightManager>().dmgMultiplier /= vals[lvl-1];
        attachedTo.GetComponent<Warrior>().attributes.modifiers.Remove(this);
    }
}
