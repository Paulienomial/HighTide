using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarriorTriggers : MonoBehaviour
{
    public LayerMask warriorMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, float.MaxValue, warriorMask);
    }

    void OnMouseDrag(){
        //Events.curr.defenderDrag(gameObject);//trigger event
    }
}
