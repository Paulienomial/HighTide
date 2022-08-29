using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeDefender : MonoBehaviour
{
    public UpgradeBar upgradeBar;
    public TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void merge(GameObject g){
        //upgrade the unit
        WarriorAttributes.attr myAttr = gameObject.GetComponent<Warrior>().attributes;
        WarriorAttributes.attr otherAttr = g.GetComponent<Warrior>().attributes;
        Debug.Log(g.GetComponent<Warrior>().attributes.mergeCount);

        gameObject.GetComponent<Warrior>().attributes.mergeCount+=g.GetComponent<Warrior>().attributes.mergeCount;
        if(gameObject.GetComponent<Warrior>().attributes.mergeCount<3){//1,2
            upgradeBar.setMax(2);
            upgradeBar.setVal(gameObject.GetComponent<Warrior>().attributes.mergeCount-1);
        }else{//3,4,5,6
            upgradeBar.setMax(3);
            upgradeBar.setVal(gameObject.GetComponent<Warrior>().attributes.mergeCount-3);
        }

        if(gameObject.GetComponent<Warrior>().attributes.mergeCount<3){
            levelText.text="1";
        }
        if(gameObject.GetComponent<Warrior>().attributes.mergeCount>=3 && gameObject.GetComponent<Warrior>().attributes.mergeCount<6){
            levelText.text="2";
        }
        if(gameObject.GetComponent<Warrior>().attributes.mergeCount==6){
            levelText.text="3";
        }

        gameObject.GetComponent<Warrior>().attributes.damage+=g.GetComponent<Warrior>().attributes.damage/2;
        gameObject.GetComponent<Warrior>().attributes.hp+=g.GetComponent<Warrior>().attributes.hp/2;
        gameObject.GetComponent<Warrior>().maxHealth=gameObject.GetComponent<Warrior>().attributes.hp;

        //remove old unit from defenders list, if it was on the list
        if(Global.curr.defenders.Contains(g)){
            Global.curr.defenders.Remove(g);
        }

        //destroy other unit
        Destroy(g);
    }
}
