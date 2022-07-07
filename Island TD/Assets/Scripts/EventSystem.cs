using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;

    private void Awake(){
        current=this;//singleton
    }

    public event Action<GameObject> onDefenderDrag;//subject
    public void defenderDrag(GameObject g){
        if(onDefenderDrag!=null){
            onDefenderDrag(g);//invoke
        }
    }

    //raycastHit.transform.gameObject;


}
