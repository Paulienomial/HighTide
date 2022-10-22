using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOld : MonoBehaviour
{
    //public
    public GameObject bonfireObject;

    
    //private
    Warrior w;
    Global global = Global.curr;
    
    void Awake(){
        w = gameObject.GetComponent<Warrior>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
