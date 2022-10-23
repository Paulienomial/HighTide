using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CityUpgrade : MonoBehaviour
{
    public static CityUpgrade curr;
    int upgradePopPrice=3;//price of population upgrade
    int popUpgradeAmount=1;//the amount of added population when you upgrade the population
    public GameObject cityObject;
    public Sprite city2;
    public Sprite city3;
    public BoxCollider2D cityCollider;
    public GameObject cityHighlight;
    public int upgradeCount=0;
    public Animator animator;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI priceText;
    public GameObject upgradeButton;
    public GameObject lvlButton;
    public TextMeshProUGUI lvlPriceText;
    public TextMeshProUGUI lvlUpgradeText;
    
    void Awake(){
        lvlButton.SetActive(false);
        curr=this;
        upgradeText.text = "+" + popUpgradeAmount.ToString();
        lvlUpgradeText.text = "+" + popUpgradeAmount.ToString();
        priceText.text = upgradePopPrice.ToString();
        lvlPriceText.text = upgradePopPrice.ToString();
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
        if(Global.curr.gold>=upgradePopPrice){//if you can afford the upgrade
            upgradeCount++;
            int barVal;
            if(upgradeCount<6){
                barVal = upgradeCount%3;
            }else{
                barVal=3;
            }
            CityUpgradeBarController.curr.setVal(barVal);

            Global.curr.gold-=upgradePopPrice;
            Global.curr.unitCap+=popUpgradeAmount;//increase unit cap

            if(upgradeCount==2 || upgradeCount==5){
                lvlButton.SetActive(true);
            }else{
                lvlButton.SetActive(false);
            }

            if(upgradeCount==3){//upgrade to level 2
                //show button with city hp increase
                //increase city hp
                Global.curr.CityHealth +=5;
                //increase amount and price of upgrade
                popUpgradeAmount*=2;
                upgradePopPrice*=2;
                upgradeText.text = "+" + popUpgradeAmount.ToString();
                lvlUpgradeText.text = "+" + popUpgradeAmount.ToString();
                priceText.text = upgradePopPrice.ToString();
                lvlPriceText.text = upgradePopPrice.ToString();
                //play upgrade animation
                AudioSystem.curr.createAndPlaySound("fanfareCityLvl2");
                AnimationController.curr.createAndPlay("cityUpgradeLvl2", new Vector3(cityObject.transform.position.x, cityObject.transform.position.y, 0f));
                //set animator
                //animator.SetInteger("state",2);
                animator.Play("building2Idle");
                //set sprite
                cityObject.GetComponent<SpriteRenderer>().sprite=city2;
            }else if(upgradeCount==6){//upgrade to level 3
                //increase city hp
                Global.curr.CityHealth +=5;
                //hide upgrade button
                upgradeButton.SetActive(false);
                lvlButton.SetActive(false);
                //AudioSystem.curr.createAndPlaySound("fanfareCityLvl3");
                //play upgrade animation
                AnimationController.curr.createAndPlay("cityUpgradeLvl3", new Vector3(cityObject.transform.position.x, cityObject.transform.position.y, 0f));
                //set animator
                //animator.SetInteger("state",3);
                animator.Play("building3Idle");
                //set sprite
                cityObject.GetComponent<SpriteRenderer>().sprite=city3;
                cityObject.transform.position = new Vector3(cityObject.transform.position.x-(.25f*(1f/18.9f)), cityObject.transform.position.y-(20f/18.9f)/2f, cityObject.transform.position.z);
                //change highlight size
            }else{
                AudioSystem.curr.createAndPlaySound("ping1", Random.Range(.9f, 1.1f));
            }
            Tutorial.curr.showCityLvlTip();
        }else{
            //Notify.curr.show("Not enough gold");
            Highlight.curr.negativeHighlight(Global.curr.goldUI, 2.5f,-.15f,0,2,22);
        }
    }

    public void playDamageAnimation(int damageTaken){
        //visual animation:
        animator.Play("building"+ getCityLevel().ToString() +"Damage");
        AudioSystem.curr.createAndPlaySound("axe"+ Random.Range(1,3).ToString() );
        //text animation:
        AnimationController.curr.play("cityDamage", new Vector3(cityHighlight.transform.position.x,cityHighlight.transform.position.y,cityHighlight.transform.position.z), "-"+damageTaken.ToString());
    }

    public int getCityLevel(){
        if(upgradeCount<3){
            return 1;
        }else if(upgradeCount>=3 && upgradeCount<6){
            return 2;
        }else{
            return 3;
        }
    }
}
