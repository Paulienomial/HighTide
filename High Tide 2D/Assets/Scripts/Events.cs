using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Events : MonoBehaviour
{
    public static Events curr;//singleton

    private void Awake(){
        curr=this;//singleton
    }

    public event Action<GameObject> onDefenderDrag;//subject
    public void defenderDrag(GameObject g){
        if(onDefenderDrag!=null){
            onDefenderDrag(g);//invoke
        }
    }

    public event Action onPurchaseDefender;
    public void purchaseDefender(){
        if(onPurchaseDefender!=null){
            onPurchaseDefender();
        }
    }

    public event Action onDropDefender;
    public void dropDefender(){
        if(onDropDefender!=null){
            onDropDefender();
        }
    }
}
