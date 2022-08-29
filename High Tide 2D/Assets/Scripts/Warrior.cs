using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public WarriorAttributes.attr attributes;
    public Vector3 coordinates;
    public int maxHealth;
    public bool diedLastWave=false;
    private HealthBar hpBar;

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
                //set the attributes
                //attributes = new WarriorAttributes.attr(warrior);
                attributes=warrior.clone();
                /*attributes.name = warrior.name;
                attributes.damage = warrior.damage;
                attributes.hp = warrior.hp;
                attributes.tier = warrior.tier;
                attributes.price = warrior.price;
                attributes.isFriendly = warrior.isFriendly;
                attributes.isRanged = warrior.isRanged;
                attributes.mergeCount = warrior.mergeCount;*/
                maxHealth = attributes.hp;
                //set the renderer
                gameObject.GetComponent<WarriorRender>().setSprite();
                //set the max health for the hp bar
                hpBar = gameObject.GetComponent<HealthBarUpdate>().hpBar;
                hpBar.setMaxHealth(maxHealth);
                return;
            }
        }
    }
}
