using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarriorTypes : MonoBehaviour
{
    public TextAsset warriorJSON;

    [Serializable]
    public class WarriorAttributesList{
        public WarriorAttributes.attr[] warriors;//c# version of json file
    }

    public WarriorAttributesList wList;

    public static WarriorTypes curr;//singleton

    void Awake(){
        curr=this;//singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        //Read json data into c# data structure
        wList = new WarriorAttributesList();
        wList = JsonUtility.FromJson<WarriorAttributesList>(warriorJSON.text);
        wList = JsonUtility.FromJson<WarriorAttributesList>(warriorJSON.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public WarriorAttributes.attr find(string n){
        foreach(WarriorAttributes.attr warrior in WarriorTypes.curr.wList.warriors){
            if(warrior.name==n){
                return warrior;
            }
        }
        return null;
    }
}
