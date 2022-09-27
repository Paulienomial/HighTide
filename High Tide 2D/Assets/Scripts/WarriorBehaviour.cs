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
        if(Global.curr.gamePhase == "fight"){
            setDrawOrder();
        }
    }

    private void afterWave(){
        if(!this){//if awaiting deletion, the don't exec any code
            //Destroy(gameObject);
        }else if(gameObject!=null){
            if(thisWarrior.attributes.name=="Farmer" && thisWarrior.diedLastWave==false){
                if(thisWarrior.getLevel()==1){
                    Global.curr.gold+=2;
                }else if(thisWarrior.getLevel()==2){
                    Global.curr.gold+=4;
                }else if(thisWarrior.getLevel()==3){
                    Global.curr.gold+=6;   
                }
            }

            gameObject.GetComponent<Warrior>().diedLastWave=false;
        }
    }

    void setDrawOrder(){
        float y = gameObject.transform.position.y;
        //y -= GridSystem.curr.grid.GetComponent<SpriteRenderer>().bounds.y/2-.1f;
        y -= 5f; // ensure negative y value, max y=0, min y=-10
        int sortingOrder = Mathf.FloorToInt(y*-1000); //multiply y with 1000 and get and int from that, min int=0, max int=10 000
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;//use that int for the sorting order, aka order in layer
    }
}
