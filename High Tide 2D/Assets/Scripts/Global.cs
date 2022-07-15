using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global curr;//singleton
    public string gamePhase;// shop, fight etc....
    public GameObject gridPlane;
    public LinkedList<GameObject> defenders;//has warrior scripts attached to them
    public int shopTier;
    public int gold;
    public int unitCap;

    void Awake(){
        curr=this;//singleton
    }

    // Start is called before the first frame update
    void Start()
    {
        defenders = new LinkedList<GameObject>();
        gamePhase="shop";
        shopTier=1;
        gold=10;
        unitCap=5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
