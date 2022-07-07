using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGlobals : MonoBehaviour
{
    public static GameGlobals self;
    public string gamePhase;  //phase can be: shop, fight etc....
    public GameObject spawnPlane;
    public GridSystem gridSyst; // so other classes can use the grid system
    public LinkedList<GameObject> defenders;//will have warrior scripts attached to them
    public LinkedList<GameObject> enemies;//will have warrior scripts attached to them
    public GameObject warrior;

    // Start is called before the first frame update

    void Start()
    {
        defenders = new LinkedList<GameObject>();
        enemies = new LinkedList<GameObject>();
        gamePhase="shop";
        GameObject go = Instantiate(warrior, new Vector3(1f,1f,1f), Quaternion.identity);//get the game object
        //Warrior wa = (Warrior)go.GetComponent(typeof(Warrior));//from the gameobject get the warrior script
        defenders.AddLast( go );//add the warrior to the defenders list
        self=this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
