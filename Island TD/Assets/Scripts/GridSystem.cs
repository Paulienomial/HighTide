using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridSystem : MonoBehaviour
{
    public int gridCellSize;
    public GameObject movableObject;
    public LayerMask floorMask;

    GameObject currObject;
    GameGlobals global;
    bool placingPhase;

    public static GridSystem curr;//singleton


    void Awake(){
        curr=this;
    }

    // Start is called before the first frame update
    void Start()
    {
        placingPhase=false;
        gridCellSize=2;
        global=GameGlobals.self;
        EventSystem.current.onDefenderDrag += snapToGrid;//subscribe observer to subject
    }

    // Update is called once per frame
    void Update()
    {
        if(placingPhase){
            placeObject(currObject);
        }
    }

    public void startPlacingPhase(String warriorName){
        GameObject g = Instantiate(movableObject, new Vector3(1f,1f,1f), Quaternion.identity);
        g.GetComponent<Warrior>().createWarrior(warriorName);//make g a warriorName
        if(!placingPhase){
            currObject=g;
            placingPhase=true;
        }
    }

    private void placeObject(GameObject g){
        snapToGrid(currObject);
        if(Input.GetKeyDown(KeyCode.Mouse0)){//place the object
            global.defenders.AddLast(g);
            placingPhase=false;
        }
    }

    private void snapToGrid(GameObject g){
        //STEP 1: calculate new grid spot, closest to mouse pos
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 gridSpot=new Vector3();
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, floorMask)){
            gridSpot = calcGridSpot(raycastHit.point, g);//calculated grid spot
        }

        //STEP 2: if another unit already occupoes this grid spot, then don't go there
        foreach(GameObject defender in global.defenders){//if a unit is already in the spot, then don't go there
            if( gridSpot.x == calcGridSpot(defender).x && gridSpot.z == calcGridSpot(defender).z ){
                return;
            }
        }

        //STEP 3: no otherunits occupy this grid spot, so free to snap here
        g.transform.position = gridSpot;
    }

    private Vector3 calcGridSpot(Vector3 pos, GameObject g){//returns the closest valid grid spot, g is just used to get the height
        Vector3 gridSpot = pos;
        pos.y += g.GetComponent<Renderer>().bounds.size.y/2;//calc y pos
        pos.x = calcGridSpotAxis(pos.x);
        pos.z = calcGridSpotAxis(pos.z);
        return pos;
    }

    private Vector3 calcGridSpot(GameObject g){//returns the closest valid grid spot, with height at 0f
        Vector3 gridSpot = g.transform.position;
        gridSpot.y = 0f;//calc y pos
        gridSpot.x = calcGridSpotAxis(g.transform.position.x);
        gridSpot.z = calcGridSpotAxis(g.transform.position.z);
        return gridSpot;
    }

    private float calcGridSpotAxis(float f){//calculates grid spot on singular axis
        return f-((f%gridCellSize+gridCellSize)%gridCellSize) + gridCellSize/2f;
    }
}
