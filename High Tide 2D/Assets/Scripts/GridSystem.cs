using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridSystem : MonoBehaviour
{
    public GameObject movable;
    public GameObject grid;
    public LayerMask gridMask;
    public int gridCellSize;
    public bool placingPhase;
    GameObject currObject;//object being placed
    public static GridSystem curr;
    public GameObject tileHighlight;
    GameObject card;
    bool draggingPhase;
    Vector3 initPos;
    public GameObject shopButton;


    void Awake(){
        curr=this;
    }

    // Start is called before the first frame update
    void Start()
    {
        draggingPhase=false;
        tileHighlight = Instantiate(tileHighlight, new Vector3(20f, 20f, 20f), Quaternion.identity);
        tileHighlight.GetComponent<SpriteRenderer>().color=(Color)(new Color32(21,255,21,255));//green
        tileHighlight.SetActive(false);
        Events.curr.onDefenderDrag += dragObject; //subscribe observer to subject
        placingPhase=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!draggingPhase && ! placingPhase){
            grid.GetComponent<SpriteRenderer>().color=(Color)(new Color32(0,0,0,0));//fully transparent
        }

        if(Input.GetKeyDown(KeyCode.G)){
            Global.curr.gold++;
        }

        if(placingPhase){
            clickToPlaceObject(currObject);
        }
        if(Input.GetKeyUp(KeyCode.Mouse0)){
            if(currObject!=null && draggingPhase==true){
                if(!validPos(currObject)){//if released at invalid pos
                    currObject.transform.position=initPos;
                }else{
                    currObject.GetComponent<Warrior>().coordinates = currObject.transform.position;
                    Events.curr.dropDefender();//trigger event
                }
                tileHighlight.SetActive(false);
                grid.GetComponent<SpriteRenderer>().color=(Color)(new Color32(0,0,0,0));//fully transparent
                currObject.GetComponent<SpriteRenderer>().sortingOrder=3;
            }
            draggingPhase=false;
        }
        if(currObject && !validPos(currObject)){
            tileHighlight.GetComponent<SpriteRenderer>().color=(Color)(new Color32(255,21,21,255));//red
        }
    }

    public void startPlacingPhase(String warriorType, GameObject c){//step 1 when purchasing a unit
        GameObject g = Instantiate(movable, calcGridSpot( getMousePos() ), Quaternion.identity);
        g.GetComponent<SpriteRenderer>().sortingOrder=10;
        tileHighlight.transform.position = g.transform.position;
        tileHighlight.SetActive(true);
        g.GetComponent<Warrior>().setWarrior(warriorType);//set g to be a warrior of type warriorType
        if(!placingPhase){
            currObject=g;
            card=c;
            placingPhase=true;
        }
    }

    private void clickToPlaceObject(GameObject g){//step 2 when purchasing a unit
        snapToGrid(currObject);
        /*RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, gridMask);
        if(hit.collider!=null && Input.GetKeyDown(KeyCode.Mouse0)){//place the object
            Global.curr.defenders.AddLast(g);
            tileHighlight.SetActive(false);
            Global.curr.gold -= g.GetComponent<Warrior>().attributes.price;
            placingPhase=false;
        }*/

        if( Input.GetKeyDown(KeyCode.Mouse0) ){
            if(validPos(currObject)){
                //PURCHASE UNIT
                g.GetComponent<SpriteRenderer>().sortingOrder=3;
                g.GetComponent<Warrior>().coordinates = g.transform.position;
                Global.curr.defenders.AddLast(g);
                Global.curr.gold -= g.GetComponent<Warrior>().attributes.price;
                Events.curr.purchaseDefender();//trigger event
            }else{
                Destroy(g);
                card.gameObject.SetActive(true);
            }
            tileHighlight.SetActive(false);
            //grid.GetComponent<SpriteRenderer>().color=(Color)(new Color32(0,0,0,0));//fully transparent
            placingPhase=false;
        }
    }

    private void dragObject(GameObject g){
        if(Global.curr.gamePhase!="fight"){
            currObject=g;
            tileHighlight.SetActive(true);
            if(draggingPhase==false){
                draggingPhase=true;
                initPos=new Vector3(g.transform.position.x, g.transform.position.y, g.transform.position.z);
                currObject.GetComponent<SpriteRenderer>().sortingOrder=10;
            }
            snapToGrid(g);
        }
    }

    private void snapToGrid(GameObject g){
        grid.GetComponent<SpriteRenderer>().color=(Color)(new Color32(21,80,240,120));//transparent blue
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, gridMask);
        if(hit.collider!=null){//if the mouse is over the grid
            //STEP 1: calc grid spot, based on mouse pos
            Vector3 gridSpot = calcGridSpot(getMousePos());
            tileHighlight.transform.position = gridSpot;
            g.transform.position = gridSpot;
            
            //STEP 2: if another unit already occupies this grid spot, then unable to place
            foreach(GameObject defender in Global.curr.defenders){//if a unit is already in the spot, then don't go there
                if( defender!=g && gridSpot.x == calcGridSpot(defender.transform.position).x && gridSpot.y == calcGridSpot(defender.transform.position).y ){//invalid spot
                    return;
                }
            }
            tileHighlight.GetComponent<SpriteRenderer>().color=(Color)(new Color32(21,255,21,255));//green

            //STEP 3: move to grid spot
            
        }
    }

    private bool validPos(GameObject g){
        Vector3 gridSpot = g.transform.position;
        foreach(GameObject defender in Global.curr.defenders){//if a unit is already in the spot, then don't go there
            if( defender!=g && gridSpot.x == calcGridSpot(defender.transform.position).x && gridSpot.y == calcGridSpot(defender.transform.position).y ){//invalid spot
                return false;//invalid grid spot
            }
        }
        return true;
    }

    /*private Vector3 calcGridSpot(Vector3 pos){//calculate the nearest grid spot to the given pos
        pos.x = calcGridSpotAxis(pos.x);
        pos.y = calcGridSpotAxis(pos.y);
        return pos;
    }*/

    private float calcGridSpotAxis(float f){//return the closest grid spot for a value on the x or y axis
        return f-((f%gridCellSize+gridCellSize)%gridCellSize) + gridCellSize/2f;
    }

    public static Vector3 getMousePos(){
        Vector3 worldPoint = Input.mousePosition;
        worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(worldPoint);
        mousePos.z = 0f;
        return mousePos;
    }

    private void highlight(GameObject g){
        tileHighlight.SetActive(true);
    }

    private Vector3 calcGridSpot(Vector3 pos){//calculate the nearest grid spot to the given pos
        float gridX = grid.transform.position.x;
        float gridWidth = grid.GetComponent<SpriteRenderer>().bounds.size.x;
        float leftOfGrid = gridX - gridWidth/2f;
        if(pos.x<leftOfGrid){//if x pos is to the left of grid
            pos.x = leftOfGrid+gridCellSize/2f;
            pos.y = calcGridSpotAxis(pos.y);
            return pos;
        }

        float rightOfGrid = gridX + gridWidth/2f;
        if(pos.x>rightOfGrid){//if x pos is to the left of grid
            pos.x = rightOfGrid-gridCellSize/2f;
            pos.y = calcGridSpotAxis(pos.y);
            return pos;
        }

        pos.x = calcGridSpotAxis(pos.x);
        pos.y = calcGridSpotAxis(pos.y);
        return pos;
    }
}
