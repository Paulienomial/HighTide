using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityUpgrade : MonoBehaviour
{
    public static CityUpgrade curr;
    int upgradePopPrice=5;//price of population upgrade
    int popUpgradeAmount=3;//the amount of added population when you upgrade the population
    int maxUnitCap=24;
    
    void Awake(){
        curr=this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)){
            upgradePop();
        }
    }

    public void upgradePop(){
        if(Global.curr.gold>=upgradePopPrice && Global.curr.unitCap<maxUnitCap){//if you can afford the upgrade
            Global.curr.gold-=upgradePopPrice;
            if(Global.curr.unitCap+popUpgradeAmount <= maxUnitCap){//if the upgrade won't exceed the maximum unit cap
                Global.curr.unitCap+=popUpgradeAmount;//increase unit cap
            }else{// if the upgrade will cause it to exceed maximum population size
                Global.curr.unitCap = maxUnitCap;//set the unit cap to the max unit cap
            }
        }else if(Global.curr.unitCap>=maxUnitCap){//if the max unit cap is reached
            Notify.curr.show("Maximum unity capacity reached");
        }else{
            Notify.curr.show("Not enough gold");
        }
    }
}
