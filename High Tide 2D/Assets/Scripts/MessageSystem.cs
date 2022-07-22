using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; //for button events
using System;

public class MessageSystem : MonoBehaviour
{
    public GameObject messageBox;
    public TextMeshProUGUI message;
    public Button button;
    public TextMeshProUGUI buttonText;
    public GameObject shopBtn;
    public GameObject shop;
    public String[] messages;
    public int step=0;
    public GameObject goldDisplay;
    public GameObject playButton;
    void Start()
    {
        playTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playTutorial(){
        Global.curr.gold=3;
        messages = new string[8] {
            "WELCOME TO HIGH TIDE",
            "Your goal is to build an army that will protect this village",
            "You have 3 availabe gold",
            "To purchase a unit, open the shop",
            "Purchase and place a unit from the shop",
            "Click and drag a unit to move it around",
            "Click and drag a unit to move it around",
            "Click on the play button to start your first fight"
        };
        showMessage(true);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener( delegate{ nextStep(); } );
    }
    public void nextStep(){
        step++;
        Debug.Log("Step: "+step);
        if(step==1){//"Your goal is to build an army that will protect this village"
            showMessage();//show the message for the current step in the tutorial
        }

        if(step==2){//"3 available gold"
            HighlightElement.curr.highlight(goldDisplay);
            showMessage();
        }

        if(step==3){//"open shop"
            showMessage(false);
            HighlightElement.curr.unHighlight();
            HighlightElement.curr.highlight(shopBtn);
            shopBtn.GetComponent<Button>().onClick.AddListener( ()=>{
                nextStep();
            } );
        }

        if(step==4){//"purchase and place a unit from the shop"
            showMessage(false);
            HighlightElement.curr.unHighlight();
            Events.curr.onPurchaseDefender += ()=>{
                if(step==4){
                    nextStep();
                }
            };
        }

        if(step==5){//"click and drag a unit to move it around"
            showMessage(false);
            Events.curr.onDropDefender += ()=>{
                nextStep();
            };
        }

        if(step==6){//"click and drag a unit to move it around"
            showMessage(false);
            Events.curr.onDropDefender += ()=>{
                nextStep();
            };
        }

        if(step==7){
            HighlightElement.curr.highlight(playButton);
            showMessage(false);
            playButton.GetComponent<Button>().onClick.AddListener( ()=>{
                HighlightElement.curr.unHighlight();
                messageBox.SetActive(false);
            } );
        }
    }

    public void showMessage(string m){
        messageBox.SetActive(true);
        message.text=m;
    }

    public void showMessage(bool showButton=true){
        messageBox.SetActive(true);
        message.text=messages[step];
        button.gameObject.SetActive(showButton);
    }

    public void messageBaseFunction(string mText, string bText){
        messageBox.SetActive(true);
        message.text=mText;
        buttonText.text=bText;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener( delegate{ hideMessage(); } );
    }

    //paramaters:
    //message to be displayed
    //button text to be displayed
    //the game object to highlight along with the current message
    public void displayMessage(string mText, string bText="NEXT", Action func=null, GameObject highlight=null){
        messageBox.SetActive(true);
        message.text=mText;
        buttonText.text=bText;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener( delegate{ hideMessage(); } );
        if(highlight!=null){
            HighlightElement.curr.highlight(highlight);
        }
        if(func!=null){
            button.onClick.AddListener( delegate{ func(); } );
        }
    }

    public void explainGoal(){
        displayMessage("Your goal is to build an army that will protect this village","NEXT",explainShopBtn);
    }

    public void explainShopBtn(){
        displayMessage("To purchase a unit, open the shop","NEXT",explainShop);
        HighlightElement.curr.highlight(shopBtn);
    }

    public void explainShop(){
        HighlightElement.curr.unHighlight();
        displayMessage("Purchase any unit and place it on the grid", "NEXT");
        HighlightElement.curr.highlight(shop);
    }

    public void hideMessage(){
        messageBox.SetActive(false);
    }
}
