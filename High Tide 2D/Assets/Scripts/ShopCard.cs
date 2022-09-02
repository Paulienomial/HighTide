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
        if(Global.curr.gamePhase != "fight"){
            if(Global.curr.gold >= warrior.price && Global.curr.defenders.Count<Global.curr.unitCap){
            GridSystem.curr.startPlacingPhase(warrior.name, card);
            AudioScript.curr.playButtonClickSound();
            card.SetActive(false);
            shop.SetActive(false);
            ShopSystem.curr.shopOpen=false;
            }
            if(Global.curr.gold<warrior.price){
                Notify.curr.show("Not enough gold");
            }
            if(Global.curr.defenders.Count>=Global.curr.unitCap){
                Notify.curr.show("Unit capacity reached");
            }
        }
    }

    public void purchaseFarner(string name){
        if(Global.curr.gamePhase != "fight"){
            WarriorAttributes.attr w = WarriorTypes.curr.find(name);
            if(Global.curr.gold >= w.price && Global.curr.defenders.Count<Global.curr.unitCap){
            GridSystem.curr.startPlacingPhase(w.name, gameObject);
            AudioScript.curr.playButtonClickSound();
            shop.SetActive(false);
            ShopSystem.curr.shopOpen=false;
            }
            if(Global.curr.gold<w.price){
                Notify.curr.show("Not enough gold");
            }
            if(Global.curr.defenders.Count>=Global.curr.unitCap){
                Notify.curr.show("Unit capacity reached");
            }
        }
    }
}
