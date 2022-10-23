using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliciousMeal : Ability
{
    public static int[] dmgAmounts = {5, 15, 45};
    public static int[] hpAmounts = {10, 30, 90};
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void go(){
        if(!this) return;//if awaiting deletion
        GameObject ahead = getDefenderAhead();
        if(ahead==null) return;

        Warrior aheadW = ahead.GetComponent<Warrior>();
        aheadW.setHealth(aheadW.maxHealth+hpAmounts[w.lvlIndex()]);
        aheadW.attributes.damage += dmgAmounts[w.lvlIndex()];
    }
}
