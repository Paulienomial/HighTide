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
    public GameObject nextBtn;
    public GameObject scroll;
    public static MessageSystem curr;//singleton

    void Awake(){
        curr=this;
    }
    void Start()
    {
        //playTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playTutorial(){
        HighlightElement.curr.arrow(nextBtn);
        ShopSystem.curr.shopAvailable=false;
        Global.curr.startButtonEnabled=false;
        
        Global.curr.gold=3;
        messages = new string[8] {
            "WELCOME TO HIGH TIDE",
            "Your goal is to build an army that will protect the village",
            "You have 3 availabe gold",
            "To purchase a unit, open the shop",
            "Purchase and place a unit from the shop",
            "Click and drag a unit to move it around",
            "Click and drag a unit to move it around",
            "Click on the play button to start your first fight"
        };
        displayMessage(true);
        HighlightElement.curr.arrow(scroll);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener( delegate{ nextStep(); } );
    }
    public void nextStep(){
        step++;
        if(step==1){//"Your goal is to build an army that will protect this village"
            displayMessage();//show the message for the current step in the tutorial
        }

        if(step==2){//"3 available gold"
            HighlightElement.curr.unHighlight();
            HighlightElement.curr.arrow(goldDisplay);
            displayMessage();
        }

        if(step==3){//"open shop"
            ShopSystem.curr.shopAvailable=true;
            displayMessage(false);
            HighlightElement.curr.unHighlight();
            HighlightElement.curr.arrow(shopBtn);
            shopBtn.GetComponent<Button>().onClick.AddListener( ()=>{
                nextStep();
            } );
        }

        if(step==4){//"purchase and place a unit from the shop"
            displayMessage(false);
            HighlightElement.curr.unHighlight();
            shopBtn.GetComponent<Button>().onClick.RemoveAllListeners();
            //HighlightElement.curr.arrow(shop);
            Events.curr.onPurchaseDefender += ()=>{
                if(step==4){
                    nextStep();
                }
            };
        }

        if(step==5){//"click and drag a unit to move it around"
            displayMessage(false);
            //HighlightElement.curr.unHighlight();
            Events.curr.onDropDefender += ()=>{
                if(step==5){
                    nextStep();
                }
            };
        }

        if(step==6){//"click and drag a unit to move it around"
            displayMessage(false);
            Events.curr.onDropDefender += ()=>{
                if(step==6){
                    nextStep();
                }
            };
        }

        if(step==7){
            Global.curr.startButtonEnabled=true;
            HighlightElement.curr.arrow(playButton);
            displayMessage(false);
            playButton.GetComponent<Button>().onClick.AddListener( ()=>{
                if(step==7){
                    HighlightElement.curr.unHighlight();
                    messageBox.SetActive(false);
                    step++;
                }
            } );
        }
    }

    public void displayMessage(string m, bool showButton=true){
        messageBox.SetActive(true);
        message.text=m;
        button.gameObject.SetActive(showButton);
    }

    public void displayMessage(bool showButton=true){
        messageBox.SetActive(true);
        message.text=messages[step];
        button.gameObject.SetActive(showButton);
    }
}
