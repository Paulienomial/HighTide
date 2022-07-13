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
    bool placingPhase;
    GameObject currObject;//object being placed

    // Start is called before the first frame update
    void Start()
    {
        GameObject d = Instantiate(movable, new Vector3(0f,0f,0f), Quaternion.identity);
        Global.curr.defenders.AddLast(d);
        Events.curr.onDefenderDrag += snapToGrid; //subscribe observer to subject
        placingPhase=false;

        startPlacingPhase("bounty hunter");
    }

    // Update is called once per frame
    void Update()
    {
        if(placingPhase){
            clickToPlaceObject(currObject);
        }
    }

    public void startPlacingPhase(String warriorType){//step 1 when purchasing a unit
        GameObject g = Instantiate(movable, new Vector3(0f,0f,0f), Quaternion.identity);
        g.GetComponent<Warrior>().setWarrior(warriorType);//set g to be a warrior of type warriorType
        if(!placingPhase){
            currObject=g;
            placingPhase=true;
        }
    }

    private void clickToPlaceObject(GameObject g){//step 2 when purchasing a unit
        snapToGrid(currObject);
        if(Input.GetKeyDown(KeyCode.Mouse0)){//place the object
            Global.curr.defenders.AddLast(g);
            placingPhase=false;
        }
    }

    private void snapToGrid(GameObject g){
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, gridMask);

        if(hit.collider!=null){//if the mouse is over the grid
            //STEP 1: calc grid spot, based on mouse pos
            Vector3 gridSpot = calcGridSpot(getMousePos());
            
            //STEP 2: if another unit already occupoes this grid spot, then don't go there
            foreach(GameObject defender in Global.curr.defenders){//if a unit is already in the spot, then don't go there
                if( gridSpot.x == calcGridSpot(defender.transform.position).x && gridSpot.y == calcGridSpot(defender.transform.position).y ){
                    return;
                }
            }

            //STEP 3: move to grid spot
            g.transform.position = gridSpot;
            }
        }

    private Vector3 calcGridSpot(Vector3 pos){//calculate the nearest grid spot to the given pos
        pos.x = calcGridSpotAxis(pos.x);
        pos.y = calcGridSpotAxis(pos.y);
        return pos;
    }

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
}
