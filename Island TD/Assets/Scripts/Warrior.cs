using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public WS.WarriorStats atrributes;

    // Start is called before the first frame update
    void Start()
    {
        //myWarriorArray = JsonUtility.FromJson<WarriorArray>(warriorsJSON.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createWarrior(string name){//set the warrior attributes according to the attributes in the JSON file
        foreach(WS.WarriorStats warrior in WarriorVals.curr.wList.warriors){
            if(warrior.name==name){
                atrributes=warrior;
            }
        }
    }
}
