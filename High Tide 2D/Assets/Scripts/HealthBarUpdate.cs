using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUpdate : MonoBehaviour
{
    public HealthBar hpBar;

    void Awake(){
        //hpBar.setMaxHealth(gameObject.GetComponent<Warrior>().maxHealth);
        //hpBar.setHealth(gameObject.GetComponent<Warrior>().attributes.hp);
    }

    // Start is called before the first frame update
    void Start()
    {
        hpBar.setMaxHealth(gameObject.GetComponent<Warrior>().attributes.hp);
        hpBar.setHealth(gameObject.GetComponent<Warrior>().attributes.hp);
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.setHealth(gameObject.GetComponent<Warrior>().attributes.hp);//displays current health
        hpBar.setMaxHealth(gameObject.GetComponent<Warrior>().maxHealth);//displays current health

        if(Input.GetKeyDown(KeyCode.Z)){
            /*if(gameObject.GetComponent<Warrior>().attributes.hp>0){
                gameObject.GetComponent<Warrior>().attributes.hp-=20;
                if(gameObject.GetComponent<Warrior>().attributes.hp<=0){
                    gameObject.GetComponent<Warrior>().attributes.hp=0;
                }
            }*/
            Global.curr.defenders.First.Value.GetComponent<Warrior>().attributes.hp-=20;
        }
    }
}
