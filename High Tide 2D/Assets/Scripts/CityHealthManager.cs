using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHealthManager : MonoBehaviour
{
    public bool gameOverCalled=false;
    public static CityHealthManager curr;
    
    void Awake(){
        curr=this;
    }
    
    void Start()
    {
        Events.curr.onWaveComplete+= ()=>{
            gameOverCalled=false;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int amount){
        if(Global.curr.CityHealth-amount<0){
            Global.curr.CityHealth=0;
        }else{
            Global.curr.CityHealth-=amount;
        }
    }

    public void gameOver()
    {
        Global.curr.startButtonEnabled=false;
        Tutorial.curr.playButton.SetActive(false);
        Tutorial.curr.shopButton.SetActive(false);
        Global.curr.gameOver=true;
        //MessageSystem.curr.displayMessage("Game over", false);
        Events.curr.gameOver();
    }
}
