using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    public LayerMask defenderMask;
    bool dragging=false;
    GameObject currObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!dragging){
            //raycast to warrior prefab
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, defenderMask);
            if(hit.collider!=null){
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    //start dragging
                    currObject=hit.collider.gameObject;
                    dragging=true;
                    Events.curr.defenderDrag(hit.collider.gameObject);//trigger event
                }
            }
        }

        if(dragging){
            if(currObject!=null){
                Events.curr.defenderDrag(currObject);
                if(Input.GetKeyUp(KeyCode.Mouse0)){
                    currObject=null;
                    dragging=false;
                }
            }else{
                dragging=false;
            }
        }
    }
}
