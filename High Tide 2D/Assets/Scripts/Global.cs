using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Global : MonoBehaviour
{
    public static Global curr;//singleton
    public string gamePhase;// shop, fight etc....
    public GameObject gridPlane;
    public List<GameObject> defenders;//has warrior scripts attached to them
    public List<GameObject> enemies;//Dynamic list of enemies currently on the battlefield
    public int shopTier;
    public int gold=8;
    public int unitCap;
    public int CityHealth = 10;
    public int waveNum = 1;
    public int enemyWaveDeathCount = 1;
    public bool waveStart = false;
    public bool startButtonEnabled=true;//when true, the start button can be used
    public bool gameOver=false;
    public int maxMergeCount=6;//max units combine into one unit
    public float MainVolume=.5f;//To be able to save player's volume changes.
    public float FXVolume=.5f;
    public float MusicVolume=.5f;
    public bool gamePaused = false;
    public int maxCityHealth = 10;
    public GameObject shopButton;
    public GameObject playButton;
    public GameObject goldUI;
    public GameObject warriorPrefab;
    public List<GameObject> deadEnemies;


    void Awake(){
        curr=this;//singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        defenders = new List<GameObject>();
        enemies = new List<GameObject>();
        gamePhase="shop";
        shopTier=1;
        unitCap=3;
        waveNum = 1;
    }

    void setInitialVolumes(){
        MainVolume=.5f;//To be able to save player's volume changes.
        FXVolume=.5f;
        MusicVolume=.5f;
        Events.curr.setInitVols();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)){//gold cheat
            Global.curr.gold+=10;
        }
        if(Input.GetKeyDown(KeyCode.F2)){//reroll cheat
            ShopSystem.curr.createShop();
        }
        if(Input.GetKeyDown(KeyCode.F3)){//wavenum-- cheat
            if(Global.curr.waveNum>1){
                Global.curr.waveNum--;
                WaveInfo.curr.drawWaveInfo();
            }
        }
        if(Input.GetKeyDown(KeyCode.F4)){//wavenum++ cheat
            if(Global.curr.waveNum<20){
                Global.curr.waveNum++;
                WaveInfo.curr.drawWaveInfo();
            }
        }
        if(Input.GetKeyDown(KeyCode.F5)){//cityhealth++ cheat
            Global.curr.CityHealth+=20;
        }
        if(Input.GetKeyDown(KeyCode.F6)){//city upgrade cheat
            CityUpgrade.curr.upgradePop(0);
        }
        if(Input.GetKeyDown(KeyCode.F11)){//strong soldier cheat
            if(defenders.Count>=Global.curr.unitCap) return;
            defenders.Add( Instantiate(warriorPrefab, new Vector2(-0.793651f, -0.1587303f), Quaternion.identity) );
            GameObject soldier = defenders.Last();
            soldier.GetComponent<Warrior>().setWarrior("Foot soldier");
            soldier.GetComponent<Warrior>().attributes.hp=10000;
            soldier.GetComponent<Warrior>().coordinates = new Vector3( -0.793651f,-0.1587303f, 0 );
        }
        if(Input.GetKeyDown(KeyCode.F12)){//very strong dragon cheat
            if(defenders.Count>=Global.curr.unitCap) return;
            defenders.Add( Instantiate(warriorPrefab, new Vector2(-0.793651f, -0.1587303f), Quaternion.identity) );
            GameObject dragon = defenders.Last();
            dragon.GetComponent<Warrior>().setWarrior("Red dragon");
            dragon.GetComponent<Warrior>().attributes.hp=200000;
            dragon.GetComponent<Warrior>().coordinates = new Vector3( -0.793651f,-0.1587303f, 0 );
        }
    }

    public void resetShop()
    {
        GetComponent<ShopSystem>().createShop();
    }
    public void resetShop(int price)
    {
        if(Global.curr.gold-price<0){
            Highlight.curr.negativeHighlight(goldUI);
            return;
        }
        GetComponent<ShopSystem>().createShop();
        Global.curr.gold-=price;
        AudioSystem.curr.createAndPlaySound("rerollPurchase", 1, 0.3f);

        Events.curr.reroll();
    }
}
