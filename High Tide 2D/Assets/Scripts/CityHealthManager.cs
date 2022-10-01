using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHealthManager : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
        
        //CityHP--;
        if(Global.curr.CityHealth>0){
            Global.curr.CityHealth--;
        }
        if (Global.curr.CityHealth <= 0)
        {
            gameOver();
        }
    }

    void gameOver()
    {
        Global.curr.startButtonEnabled=false;
        Global.curr.gameOver=true;
        MessageSystem.curr.displayMessage("Game over", false);
    }
}
