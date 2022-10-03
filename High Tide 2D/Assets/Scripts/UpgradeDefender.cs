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

    public void merge(GameObject g){//merge g into this unit
        //upgrade the unit
        WarriorAttributes.attr myAttr = gameObject.GetComponent<Warrior>().attributes;
        WarriorAttributes.attr otherAttr = g.GetComponent<Warrior>().attributes;

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
            if(gameObject.GetComponent<Warrior>().attributes.name=="Fire starter"){
                gameObject.GetComponent<Warrior>().attributes.damagePerTick=30;
            }
        }
        if(gameObject.GetComponent<Warrior>().attributes.mergeCount==6){
            levelText.text="3";
            if(gameObject.GetComponent<Warrior>().attributes.name=="Fire starter"){
                gameObject.GetComponent<Warrior>().attributes.damagePerTick=40;
            }
        }

        if(gameObject.GetComponent<Warrior>().attributes.mergeCount==3){//upgrade to level 2
            AnimationController.curr.play("lvl2Upgrade", new Vector3(g.transform.position.x,g.transform.position.y-.5f,0f),"","tadaTrumpet",1,"image");
        }else if(gameObject.GetComponent<Warrior>().attributes.mergeCount==6){//upgrade to level 3
            AnimationController.curr.play("lvl3Upgrade", new Vector3(g.transform.position.x,g.transform.position.y-.65f,0f),"","happyTrumpet",2,"image");
        }else{
            AudioSystem.curr.createAndPlaySound("ping"+Random.Range(4,7).ToString(), 10f);//merge sound
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

        //show this unit at currently selected
        HighlightSelected.curr.select(gameObject);

        //Show tutprial tip
        Tutorial.curr.showLevelUnitTip(gameObject);
    }
}
