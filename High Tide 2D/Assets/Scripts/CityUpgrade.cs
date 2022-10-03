using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CityUpgrade : MonoBehaviour
{
    public static CityUpgrade curr;
    int upgradePopPrice=5;//price of population upgrade
    int popUpgradeAmount=2;//the amount of added population when you upgrade the population
    int maxUnitCap=24;
    public GameObject cityObject;
    public Sprite city2;
    public Sprite city3;
    public BoxCollider2D cityCollider;
    public GameObject cityHighlight;
    public int lvl1PopSize=0;
    public int lvl2PopSize=13;
    public int lvl3PopSize=16;
    public Animator animator;
    public TextMeshProUGUI upgradeText;
    
    void Awake(){
        curr=this;
        upgradeText.text = "+" + popUpgradeAmount.ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)){
            upgradePop(0);
        }
        if(Input.GetKeyDown(KeyCode.B)){
            playDamageAnimation(1);
        }
    }

    public void upgradePop(int price){
        int oldPrice=upgradePopPrice;
        upgradePopPrice=price;
        upgradePop();
        upgradePopPrice=oldPrice;
    }
    public void upgradePop(){
<<<<<<< Updated upstream
        if(Global.curr.gold>=upgradePopPrice && Global.curr.unitCap<maxUnitCap){//if you can afford the upgrade
=======
        if(Global.curr.gold>=upgradePopPrice){//if you can afford the upgrade
            StatScreens.curr.totalSpent += upgradePopPrice;
            upgradeCount++;
            int barVal;
            if(upgradeCount<6){
                barVal = upgradeCount%3;
            }else{
                barVal=3;
            }
            CityUpgradeBarController.curr.setVal(barVal);

>>>>>>> Stashed changes
            Global.curr.gold-=upgradePopPrice;
            if(Global.curr.unitCap+popUpgradeAmount <= maxUnitCap){//if the upgrade won't exceed the maximum unit cap
                Global.curr.unitCap+=popUpgradeAmount;//increase unit cap
            }else{// if the upgrade will cause it to exceed maximum population size
                Global.curr.unitCap = maxUnitCap;//set the unit cap to the max unit cap
            }

            //level 2 and 3 buildings
            if(Global.curr.unitCap>=lvl2PopSize && Global.curr.unitCap<lvl2PopSize+popUpgradeAmount){
                //set animator
                animator.SetInteger("state",2);
                //set sprite
                cityObject.GetComponent<SpriteRenderer>().sprite=city2;
            }else if(Global.curr.unitCap>=lvl3PopSize && Global.curr.unitCap<lvl3PopSize+popUpgradeAmount){
                //set animator
                animator.SetInteger("state",3);
                //set sprite
                cityObject.GetComponent<SpriteRenderer>().sprite=city3;
                cityObject.transform.position = new Vector3(cityObject.transform.position.x-(.25f*(1f/18.9f)), cityObject.transform.position.y-(20f/18.9f)/2f, cityObject.transform.position.z);
                //change highlight size
            }
        }else if(Global.curr.unitCap>=maxUnitCap){//if the max unit cap is reached
            Notify.curr.show("Maximum unity capacity reached");
        }else{
            Notify.curr.show("Not enough gold");
        }
    }

    public void playDamageAnimation(int damageTaken){
        animator.Play("buildingDamage");
        AudioSystem.curr.createAndPlaySound("axe"+ Random.Range(1,3).ToString() );
        AnimationController.curr.play("cityDamage", new Vector3(cityHighlight.transform.position.x,cityHighlight.transform.position.y,cityHighlight.transform.position.z), "-"+damageTaken.ToString());
    }
}
