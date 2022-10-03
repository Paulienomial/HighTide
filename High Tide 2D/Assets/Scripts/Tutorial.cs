using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; //for button events
using System;
using UnityEngine.Events;
using System.Linq;

public class Tutorial : MonoBehaviour
{
    public static Tutorial curr;
    public GameObject messageBox;
    public GameObject scroll;
    public TextMeshProUGUI messageText;
    public Button messageButton;
    public GameObject tipText;
    public TextMeshProUGUI messageButtonText;
    public GameObject stronghold;
    public GameObject shopButton;
    public GameObject shop;
    public GameObject cardsContainer;
    public GameObject shopButtons;
    public GameObject gridArea;
    public GameObject goldUI;
    public GameObject playButton;
    public GameObject highlightGrid;
    public GameObject farmerButton;
    public int currStep=0;
    public bool showedMergeTip=false;
    public bool showedLevelUnitTip=false;
    public bool showedFarmerTip=false;
    public bool showedPopulationTip=false;
    int tipCount=0;
    public TextMeshProUGUI tipCountText;
    bool showingWSTip=false;
    public bool shouldShowTutorial=true;
    public GameObject dontShowTutButton;


    //private attributes
    GameObject outline=null;

    void Awake(){
        curr=this;
    }

    void Start()
    {
        messageBox.SetActive(false);

        playTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playTutorial(){
        ShopSystem.curr.shopAvailable=false;
        Global.curr.startButtonEnabled=false;
        step();
    }

    public void step(){
        if(shouldShowTutorial){
            currStep++;
            hideMessage();
            Highlight.curr.unFocus();
            highlightGrid.SetActive(false);
            messageButton.onClick.RemoveAllListeners();
            dontShowTutButton.SetActive(false);
            //messageButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,105);
            if(currStep==1){//welcome
                //messageButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,165);
                displayMessage("Welcome to High Tide!");
                Highlight.curr.darkenAllExcept(messageBox);

                messageButton.onClick.RemoveAllListeners();
                messageButton.onClick.AddListener( ()=>{
                    AudioSystem.curr.createAndPlaySound("btnClick");
                    step();
                } );
            }
            if(currStep==2){//explain goal
                displayMessage("The goal of the game is to place units to protect the stronghold.");
                Highlight.curr.darkenAllExcept(stronghold);
                outline = Highlight.curr.outlineAnimate(stronghold,-.15f,0,-.15f,.15f);
                messageButton.onClick.AddListener( ()=>{
                    AudioSystem.curr.createAndPlaySound("btnClick");
                    Destroy(outline);
                    step();
                } );
            }
            if(currStep==3){//show gold
                displayMessage("You have " + Global.curr.gold.ToString() + " available gold.");
                Highlight.curr.darkenAllExcept(goldUI);
                outline = Highlight.curr.outlineAnimate(goldUI,0,0,0,20);
                messageButton.onClick.AddListener( ()=>{
                    AudioSystem.curr.createAndPlaySound("btnClick");
                    hideMessage();
                    Destroy(outline);
                    step();
                } );
            }
            if(currStep==4){//highlight shop button
                ShopSystem.curr.shopAvailable=true;
                Highlight.curr.focus( shopButton );

                Button btn = shopButton.GetComponent<Button>();
                btn.onClick.AddListener( ()=>{
                    if(currStep==4){
                        step();
                    }
                } );
            }
            if(currStep==5){//highlight shop
                ShopSystem.curr.shopAvailable=false;
                scroll.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 165);
                scroll.GetComponent<RectTransform>().localPosition = new Vector2(0,250);
                displayMessage("Select a unit", false);
                Highlight.curr.darkenAllExcept(cardsContainer);
                foreach(Transform child in cardsContainer.transform){
                    Button button = child.GetComponentInChildren<Button>();
                    button.onClick.AddListener( ()=>{
                        if(currStep==5){
                            step();
                        }
                    } );
                }
            }
            if(currStep==6){//place unit
                highlightGrid.SetActive(true);
                displayMessage("Place your unit.",false);
                Events.curr.onPurchaseDefender += ()=>{
                    if(currStep==6){
                        step();
                    }
                };
            }
            if(currStep==7){//drag unit
                highlightGrid.SetActive(true);
                scroll.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 200);
                displayMessage("Click and drag your unit to move it around.", false);
                Events.curr.onDraggedNewSpot += ()=>{
                    if(currStep==7){
                        step();
                    }
                };
            }
            if(currStep==8){//play button
                Global.curr.startButtonEnabled=true;
                displayMessage("Click on the play button to start your first fight.", false);
                Highlight.curr.focus(playButton);
                playButton.GetComponent<Button>().onClick.AddListener( ()=>{
                    if(currStep==8){
                        ShopSystem.curr.shopAvailable=true;
                        step();
                    }
                } );
            }
        }
        
    }

    public void displayMessage(string m, bool showButton=true, string buttonText="NEXT"){
        tipText.SetActive(false);
        messageBox.SetActive(true);
        messageText.text=m;
        messageButton.gameObject.SetActive(showButton);
        messageButtonText.text = buttonText;
    }

    public void displayTip(string m){
        if(shouldShowTutorial){
            tipCount++;
            AudioSystem.curr.createAndPlaySound("bell");
            tipCountText.text = tipCount.ToString()+"/4:";
            messageText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-230f);
            scroll.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            scroll.GetComponent<RectTransform>().sizeDelta = new Vector2(800f, 450f);
            
            displayMessage(m, true, "OK");
            tipText.SetActive(true);
            messageButton.onClick.AddListener( ()=>{
                AudioSystem.curr.createAndPlaySound("btnClick");
                messageBox.SetActive(false);
                messageButton.onClick.RemoveAllListeners();
            } );
        }
        
    }

    public void hideMessage(){
        messageBox.SetActive(false);
    }

    public void showMergeTutorialTip(){
        if(!shouldShowTutorial) return;
        if(Global.curr.waveNum==2 && Global.curr.defenders.Count>=1 && Global.curr.defenders.ElementAt(0).GetComponent<Warrior>().attributes.name!="Farmer" && !showedMergeTip){
            Highlight.curr.disableUI();
            showedMergeTip=true;
            string name = Global.curr.defenders.ElementAt(0).GetComponent<Warrior>().attributes.name;
            displayTip("You found another "+name+"! You can combine two units of the same type by placing them on top of each other.");
            GameObject outline = Highlight.curr.outlineAnimate((GameObject)(ShopSystem.curr.cards[0]), -960,0,20,30);
            messageButton.onClick.AddListener( ()=>{
                AudioSystem.curr.createAndPlaySound("btnClick");
                Highlight.curr.enableUI();
                Destroy(outline);
            } );
        }
    }

    public void showLevelUnitTip(GameObject g){
        if(!shouldShowTutorial) return;
        if(!showedLevelUnitTip){
            showingWSTip=true;
            string name = g.GetComponent<Warrior>().attributes.name;
            showedLevelUnitTip=true;
            displayTip("You've upgraded your " + name + "! When the upgrade bar above the unit fills up, then the unit will level up!");
            if(g.transform.position.x<0){
                scroll.GetComponent<RectTransform>().localPosition = new Vector2(480, 112);
            }else{
                scroll.GetComponent<RectTransform>().localPosition = new Vector2(-480, 112);
            }
            //scroll.GetComponent<RectTransform>().sizeDelta = new Vector2(577.5f, 490.5f);
            GameObject outline = Highlight.curr.outlineAnimate(g,0,.2f,.7f,.7f);
            messageButton.onClick.AddListener( ()=>{
                AudioSystem.curr.createAndPlaySound("btnClick");
                Destroy(outline);
            } );
        }
    }

    public void showFarmerTip(){
        if(!shouldShowTutorial) return;
        if(!showedFarmerTip && Global.curr.waveNum>=3){
            Highlight.curr.disableUI();
            showedFarmerTip=true;
            displayTip("You can purchase farmers to boost your gold income.");
            GameObject outline = Highlight.curr.outlineAnimate(farmerButton, -960f, 0, 20, 30);
            messageButton.onClick.AddListener( ()=>{
                AudioSystem.curr.createAndPlaySound("btnClick");
                Highlight.curr.enableUI();
                Destroy(outline);
            } );
        }
    }

    public void showPopulationTip(){
        if(!shouldShowTutorial) return;
        if(!showedPopulationTip && Global.curr.defenders.Count==Global.curr.unitCap){
            showingWSTip=true;
            Highlight.curr.disableUI();
            showedPopulationTip=true;
            displayTip("You've reached max capacity. You can upgrade your unit capacity using the stronghold.");
            GameObject outline = Highlight.curr.outlineAnimate(stronghold,-.15f,0,-.15f,.15f);
            messageButton.onClick.AddListener( ()=>{
                AudioSystem.curr.createAndPlaySound("btnClick");
                Highlight.curr.enableUI();
                Destroy(outline);
            } );
        }
    }

    public void dontShowTutorial(){
        shouldShowTutorial=false;
        hideMessage();
        Highlight.curr.unFocus();
        highlightGrid.SetActive(false);
        messageBox.SetActive(false);
    }
}
