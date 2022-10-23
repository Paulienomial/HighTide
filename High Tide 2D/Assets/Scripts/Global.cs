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
        /*if(Input.GetKeyDown(KeyCode.X)){
            defenders.AddLast( Instantiate(fireStarter,new Vector2(0,0), Quaternion.identity) );
            GameObject fireDude = defenders.Last.Value;
            fireDude.GetComponent<Warrior>().setWarrior("Fire starter");
        }*/
        if(Input.GetKeyDown(KeyCode.H)){
            defenders.Add( Instantiate(warriorPrefab, new Vector2(0, 0), Quaternion.identity) );
            GameObject dragon = defenders.Last();
            dragon.GetComponent<Warrior>().setWarrior("Red dragon");
            dragon.GetComponent<Warrior>().coordinates = new Vector3( 0,0, 0 );
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
