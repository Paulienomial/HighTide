using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFuncs : MonoBehaviour
{
    public GameObject friendlyWarrior;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startPlacing(string warriorType){
        GridSystem.curr.startPlacingPhase(warriorType);
    }
}
