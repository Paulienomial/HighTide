using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBehaviours : MonoBehaviour
{
    public int globalHPAura=0;
    public int prevDMGAura=0;
    public bool loneRangerBuffApplied=false;
    

    public static GlobalBehaviours curr;
    void Awake()
    {
        curr=this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


  
}
