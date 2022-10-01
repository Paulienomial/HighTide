using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Warrior thisWarrior;
    void Start()
    {
        
    }

    void Awake(){
        thisWarrior = gameObject.GetComponent<Warrior>();
        Events.curr.onWaveComplete += afterWave;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void afterWave(){
        if(!this){//if awaiting deletion, the don't exec any code
            //Destroy(gameObject);
        }else if(gameObject!=null){
            if(thisWarrior.attributes.name=="Farmer" && thisWarrior.diedLastWave==false){
                if(thisWarrior.getLevel()==1){
                    StatScreens.curr.farmGold += 2;
                    //Debug.Log("Farmer gold = " + StatScreens.curr.farmGold);
                    Global.curr.gold+=2;
                }
                else if(thisWarrior.getLevel()==2){
                    StatScreens.curr.farmGold += 4;
                    //Debug.Log("Farmer gold = " + StatScreens.curr.farmGold);
                    Global.curr.gold+=4;
                }
                else if(thisWarrior.getLevel()==3){
                    StatScreens.curr.farmGold += 6;
                    //Debug.Log("Farmer gold = " + StatScreens.curr.farmGold);
                    Global.curr.gold+=6;   
                }
            }

            gameObject.GetComponent<Warrior>().diedLastWave=false;
        }
    }
}
