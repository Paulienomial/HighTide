using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridSystem : MonoBehaviour
{
    public GameObject movable;
    public GameObject grid;
    public LayerMask gridMask;
    public float gridCellSize;
    public bool placingPhase;
    GameObject currObject;//object being placed
    public static GridSystem curr;
    public GameObject tileHighlight;
    GameObject card;
    public bool draggingPhase;
    Vector3 initPos;
    public GameObject shopButton;
    public float cellSizePixels=32f;
    public float characterPixels=20f;//character width and height dimensions in pixels
    public float pixelsPerunit=18.9f;
    float verticalOffset;


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
        Events.curr.onStopDefenderDrag += hideGrid;
        Events.curr.onStopDefenderDrag += dragFalse;
        Events.curr.onStopDefenderDrag += mergeDrop;
        placingPhase=false;

        gridCellSize = cellSizePixels/pixelsPerunit;

        tileHighlight.transform.localScale = new Vector3(gridCellSize, gridCellSize, gridCellSize);

        verticalOffset = (-1f*(cellSizePixels/pixelsPerunit/2f)+(characterPixels/pixelsPerunit/2f));//align to bottom
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
                draggingPhase=false;
                if(!validPos(currObject)){//if released at invalid pos
                    currObject.transform.position=initPos;
                }else{
                    AudioScript.curr.playPlaceWarrior();
                    currObject.GetComponent<Warrior>().coordinates = currObject.transform.position;
                    Events.curr.dropDefender();//trigger event
                }
                tileHighlight.SetActive(false);
                grid.GetComponent<SpriteRenderer>().color=(Color)(new Color32(0,0,0,0));//fully transparent
                currObject.GetComponent<SpriteRenderer>().sortingOrder=3;
            }
        }
        if(currObject && !validPos(currObject)){
            tileHighlight.GetComponent<SpriteRenderer>().color=(Color)(new Color32(255,21,21,255));//red
        }
    }

    public void startPlacingPhase(String warriorType, GameObject c){//step 1 when purchasing a unit
        GameObject g = Instantiate(movable, calcGridSpot( getMousePos() ), Quaternion.identity);
        HighlightSelected.curr.select(g);
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
            if(validPos(currObject)){//if valid placement
                GameObject mergeWith = getMergeDefender();//check if there is an already placed unit to merge with
                if(mergeWith!=null){
                    mergeWith.GetComponent<UpgradeDefender>().merge(currObject);//if merging is possible, then do merge(also destroys object being dragged in)
                }else{//else add a new defender
                    g.GetComponent<SpriteRenderer>().sortingOrder=3;
                    g.GetComponent<Warrior>().coordinates = g.transform.position;
                    Global.curr.defenders.AddLast(g);
                }
                //PURCHASE UNIT
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
        if(g==null) return;
        HighlightSelected.curr.select(g);
        if(Global.curr.gamePhase!="fight"){
            if(draggingPhase==false){
                currObject=g;
                tileHighlight.SetActive(true);
                initPos=new Vector3(g.transform.position.x, g.transform.position.y, g.transform.position.z);
                draggingPhase=true;
                currObject.GetComponent<SpriteRenderer>().sortingOrder=10;
            }else{
                snapToGrid(g);
            }
        }
    }

    private void snapToGrid(GameObject g){
        grid.GetComponent<SpriteRenderer>().color=(Color)(new Color32(21,80,240,120));//transparent blue
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, gridMask);
        if(hit.collider!=null){//if the mouse is over the grid
            //STEP 1: calc grid spot, based on mouse pos
            Vector3 gridSpot = calcGridSpot(getMousePos());
            tileHighlight.transform.position = gridSpot;
            gridSpot.y += verticalOffset;//align to bottom
            g.transform.position = gridSpot;
            
            //STEP 2: if another unit already occupies this grid spot, then unable to place
            foreach(GameObject defender in Global.curr.defenders){//if a unit is already in the spot, then don't go there
                if( defender!=g && sameSpot(g, defender) ){//invalid spot
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
            if( defender!=g && sameSpot(g, defender) ){//same spot
                if(canMerge(g, defender)){
                    return true;
                }
                return false;//invalid grid spot
            }
        }
        return true;
    }

    public bool canMerge(GameObject g1, GameObject g2){
        WarriorAttributes.attr a1 = g1.GetComponent<Warrior>().attributes;
        WarriorAttributes.attr a2 = g2.GetComponent<Warrior>().attributes;
        if(a1.name==a2.name && a1.mergeCount+a2.mergeCount<=Global.curr.maxMergeCount){
            Debug.Log("Can merge");
            return true;
        }
        return false;
    }

    public GameObject getMergeDefender(){//merge if possible and return the defender that was merged with, else return null
        GameObject g = currObject;
        Vector3 gridSpot = g.transform.position;
        foreach(GameObject defender in Global.curr.defenders){//cycle through all defenders
            if( defender!=g && sameSpot(g, defender) ){//same spot
                if(canMerge(g, defender)){//can merge with unit at same spot      
                    return defender;
                }
            }
        }
        return null;//no defenders to merge with
    }

    public bool sameSpot(GameObject g1, GameObject g2){
        if(g1==null || g2==null) return false;
        float tolerance = .1f;//sometimes they won't be EXACTLY on the same spot
        if( Math.Abs(g1.transform.position.x-g2.transform.position.x)<tolerance && Math.Abs(g1.transform.position.y-g2.transform.position.y)<tolerance ){//same spot
            Debug.Log("Same spot");
            return true;        
        }
        return false;
    }

    /*private float calcGridSpotAxis(float f){//return the closest grid spot for a value on the x or y axis
        return f-((f%gridCellSize+gridCellSize)%gridCellSize) + gridCellSize/2f;//f - f%gridCellSize + gridCellSize/2
    }*/

    private float calcGridSpotY(float f){
        float gridY = grid.transform.position.y;
        float gridHeight = grid.GetComponent<SpriteRenderer>().bounds.size.y;
        float botOfGrid = gridY - gridHeight/2f;
        float offset=mod(botOfGrid,gridCellSize);
        float a = f+offset;
        return f-(mod(a,gridCellSize)) + gridCellSize/2f;
    }

    private float calcGridSpotX(float f){
        float gridX = grid.transform.position.x;
        float gridWidth = grid.GetComponent<SpriteRenderer>().bounds.size.x;
        float leftOfGrid = gridX - gridWidth/2f;
        float offset=mod(leftOfGrid,gridCellSize);
        float a = f+offset;
        return f-(mod(a,gridCellSize)) + gridCellSize/2f;
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

    private void hideGrid(GameObject g){
        tileHighlight.SetActive(false);
        grid.GetComponent<SpriteRenderer>().color=(Color)(new Color32(0,0,0,0));//fully transparent
    }

    private void dragFalse(GameObject g){
        draggingPhase=false;
    }

    private void mergeDrop(GameObject g){
        if(currObject!=null){
            GameObject mergeWith = getMergeDefender();//check if there is an already placed unit to merge with
            if(mergeWith!=null){
                mergeWith.GetComponent<UpgradeDefender>().merge(currObject);//if merging is possible, then do merge(also destroys object being dragged in)
            }
        }
    }

    private Vector3 calcGridSpot(Vector3 pos){//calculate the nearest grid spot to the given pos
        float gridX = grid.transform.position.x;
        float gridWidth = grid.GetComponent<SpriteRenderer>().bounds.size.x;
        float leftOfGrid = gridX - gridWidth/2f;
        if(pos.x<leftOfGrid){//if x pos is to the left of grid
            pos.x = leftOfGrid+gridCellSize/2f;
            pos.y = calcGridSpotY(pos.y);
            return pos;
        }

        float rightOfGrid = gridX + gridWidth/2f;
        if(pos.x>rightOfGrid){//if x pos is to the left of grid
            pos.x = rightOfGrid-gridCellSize/2f;
            pos.y = calcGridSpotY(pos.y);
            return pos;
        }

        pos.x = calcGridSpotX(pos.x);
        pos.y = calcGridSpotY(pos.y);
        return pos;
    }

    public float mod(float a, float b){
        return (a%b + b)%b;
    }
}
