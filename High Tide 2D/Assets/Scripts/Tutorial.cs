using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; //for button events
using System;
using UnityEngine.Events;

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
        step(1);
    }

    public void step(int currStep){
        hideMessage();
        Highlight.curr.unFocus();
        highlightGrid.SetActive(false);
        if(currStep==1){
            displayMessage("Welcome to High Tide!");
            Highlight.curr.darkenAllExcept(messageBox);

            messageButton.onClick.RemoveAllListeners();
            messageButton.onClick.AddListener( ()=>{
                step(++currStep);
            } );
        }
        if(currStep==2){
            displayMessage("The goal of the game is to place units to protect the stronghold");
            Highlight.curr.darkenAllExcept(stronghold);
            outline = Highlight.curr.outlineAnimate(stronghold,-.15f,0,-.15f,.15f);

            messageButton.onClick.RemoveAllListeners();
            messageButton.onClick.AddListener( ()=>{
                Destroy(outline);
                step(++currStep);
            } );
        }
        if(currStep==3){
            displayMessage("You have " + Global.curr.gold.ToString() + " available gold");
            Highlight.curr.darkenAllExcept(goldUI);
            outline = Highlight.curr.outlineAnimate(goldUI,0,0,0,20);

            messageButton.onClick.RemoveAllListeners();
            messageButton.onClick.AddListener( ()=>{
                hideMessage();
                Destroy(outline);
                step(++currStep);
            } );
        }
        if(currStep==4){//highlight shop button
            ShopSystem.curr.shopAvailable=true;
            Highlight.curr.focus( shopButton );

            Button btn = shopButton.GetComponent<Button>();
            btn.onClick.AddListener( ()=>{
                if(currStep==4){
                    step(++currStep);
                }
            } );
        }
        if(currStep==5){//highlight shop
            ShopSystem.curr.shopAvailable=false;
            scroll.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 170);
            scroll.GetComponent<RectTransform>().localPosition = new Vector2(0,250);
            displayMessage("Select a unit", false);
            Highlight.curr.darkenAllExcept(cardsContainer);
            foreach(Transform child in cardsContainer.transform){
                Button button = child.GetComponentInChildren<Button>();
                button.onClick.AddListener( ()=>{
                    if(currStep==5){
                        step(++currStep);
                    }
                } );
            }
        }
        if(currStep==6){//place unit
            highlightGrid.SetActive(true);
            displayMessage("Place your unit",false);
            Events.curr.onPurchaseDefender += ()=>{
                if(currStep==6){
                    Debug.Log("defender dropeed");
                    step(++currStep);
                }
            };
        }
        if(currStep==7){//drag unit
            highlightGrid.SetActive(true);
            displayMessage("Click and drag your unit to move it around", false);
            Events.curr.onDraggedNewSpot += ()=>{
                if(currStep==7){
                    step(++currStep);
                }
            };
        }
        if(currStep==8){//play button
            Global.curr.startButtonEnabled=true;
            displayMessage("Click on the play button to start your first fight", false);
            Highlight.curr.focus(playButton);
            playButton.GetComponent<Button>().onClick.AddListener( ()=>{
                if(currStep==8){
                    ShopSystem.curr.shopAvailable=true;
                    step(++currStep);
                }
            } );
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
        tipText.SetActive(true);
        displayMessage(m, true, "OK");
    }

    public void hideMessage(){
        messageBox.SetActive(false);
    }
}
