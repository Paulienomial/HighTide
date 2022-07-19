using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public WarriorAttributes.attr attributes;
    public Vector3 coordinates;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setWarrior(string name){
        //search through aall the warriors and set attributes equal to warrior with given name
        foreach(WarriorAttributes.attr warrior in WarriorTypes.curr.wList.warriors){
            if(warrior.name==name){
                attributes=warrior;
                gameObject.GetComponent<WarriorRender>().setSprite();
                return;
            }
        }
    }
}
