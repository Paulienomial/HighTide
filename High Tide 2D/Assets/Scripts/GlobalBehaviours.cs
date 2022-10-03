using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviours : MonoBehaviour
{
    public int globalDMGAura=0;
    public int globalHPAura=0;
    public int prevDMGAura=0;
    

    public static GlobalBehaviours curr;
    void Awake()
    {
        curr=this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void applyAuraRangerBuff(){
        Debug.Log("Yeet");
        //find highest level aura dude
        Warrior w = findBestAuraRanger();

        //if there is an aura dude
        if(w!=null){
            globalDMGAura = w.getLevel()*10;
            Debug.Log("DMG aura: "+globalDMGAura.ToString());
        }else{
            globalDMGAura = 0;
        }

    }

    private Warrior findBestAuraRanger(){
        Warrior best=null;
        foreach(GameObject g in Global.curr.defenders){
            if( g.GetComponent<Warrior>().attributes.name=="Aura ranger" ){
                //if aura ranger is alive or it is shopping phase
                Debug.Log("Found aura ranger");
                if( g.GetComponent<Warrior>().diedLastWave==false  || Global.curr.gamePhase=="shop"){
                    if(best==null){
                        best=g.GetComponent<Warrior>();
                    }else if(g.GetComponent<Warrior>().getLevel() > best.getLevel()){
                        best=g.GetComponent<Warrior>();
                    }
                }
            }
        }
        return best;
    }
}
