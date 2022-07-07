using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This reads and stores all the values for different warrior types, as specified in the JSON
public class WarriorVals : MonoBehaviour
{
    public TextAsset warriorJSON;

    //the class that holds all the JSON data
    [Serializable]
    public class WarriorList{
        public WS.WarriorStats[] warriors;
    }

    public WarriorList wList;

    public static WarriorVals curr;
    void Awake(){
        curr=this;//singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        wList = new WarriorList();
        wList = JsonUtility.FromJson<WarriorList>(warriorJSON.text);
        wList = JsonUtility.FromJson<WarriorList>(warriorJSON.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
