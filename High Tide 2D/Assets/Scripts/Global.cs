using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global curr;//singleton
    public string gamePhase;// shop, fight etc....
    public GameObject gridPlane;
    public LinkedList<GameObject> defenders;//has warrior scripts attached to them
    public LinkedList<GameObject> enemies;//Dynamic list of enemies currently on the battlefield
    public int shopTier;
    public int gold=10;
    public int unitCap;
    public int waveNum = 1;
    public int enemyWaveDeathCount = 1;
    public bool waveStart = false;

    void Awake(){
        curr=this;//singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        defenders = new LinkedList<GameObject>();
        enemies = new LinkedList<GameObject>();
        gamePhase="shop";
        shopTier=1;
        unitCap=5;
        waveNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetShop()
    {
        GetComponent<ShopSystem>().createShop();
    }
}
