using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCard : MonoBehaviour
{
    public WarriorAttributes.attr warrior;
    public GameObject card;
    public GameObject shop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void purchaseUnit(){
        if(Global.curr.gold >= warrior.price && Global.curr.defenders.Count<Global.curr.unitCap){
            GridSystem.curr.startPlacingPhase(warrior.name, card);
            card.SetActive(false);
            shop.SetActive(false);
        }
        if(Global.curr.gold<warrior.price){
            Debug.Log("Not enough gold");
        }
        if(Global.curr.defenders.Count>=Global.curr.unitCap){
            Debug.Log("Not enough food");
        }
    }
}
