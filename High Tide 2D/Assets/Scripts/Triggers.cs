using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    public LayerMask defenderMask;
    bool dragging=false;
    GameObject currObject;//for defender drag
    public LayerMask cityMask;
    bool hovering=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //**** DRAGGING UNITS: ****
        //STEP 1: if not currently dragging and left click on a unit
            //then set dragging to true
            //trigger drag event
        
        //STEP 2: if dragging
            //then trigger drag event
            //if left mouse button up
                //then set drag to false

        if(!dragging){
            //Debug.Log("not dragging");
            //raycast to warrior prefab
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, defenderMask);
            if(hit.collider!=null && ShopSystem.curr.shopOpen==false){
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    //start dragging
                    //Debug.Log("start dragging");
                    currObject=hit.collider.gameObject;
                    dragging=true;
                    Events.curr.defenderDrag(hit.collider.gameObject);//trigger event
                }
            }
        }

        if(dragging){
            if(currObject!=null){
                //Debug.Log("continue dragging");
                Events.curr.defenderDrag(currObject);
                if(Input.GetKeyUp(KeyCode.Mouse0)){
                    //release
                    //Debug.Log("stop dragging");
                    currObject=null;
                    dragging=false;
                    Events.curr.stopDefenderDrag(currObject);
                }
            }else{
                dragging=false;
            }
        }


        /**** HOVER OVER CITY ****/
        if(!hovering){
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, cityMask);
            if(hit.collider!=null){
                hovering=true;
                Events.curr.hoverCity();
            }
        }
        if(hovering){
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, cityMask);
            if(hit.collider==null){
                hovering=false;
                Events.curr.notHoverCity();
            }
        }
    }
}
