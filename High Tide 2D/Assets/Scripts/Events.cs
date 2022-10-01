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
            if(g!=null){
                onDefenderDrag(g);//invoke
            }
        }
    }

    public event Action<GameObject> onStopDefenderDrag;
    public void stopDefenderDrag(GameObject g){
        if(onStopDefenderDrag!=null){
            onStopDefenderDrag(g);
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

    public event Action onWaveComplete;
    public void waveComplete(){
        if(onWaveComplete!=null){
            onWaveComplete();
        }
    }

    public event Action onHoverCity;
    public void hoverCity(){
        if(onHoverCity!=null){
            onHoverCity();
        }
    }

    public event Action onNotHoverCity;
    public void notHoverCity(){
        if(onNotHoverCity!=null){
            onNotHoverCity();
        }
    }

    public event Action onDraggedNewSpot;
    public void draggedNewSpot(){
        if(onDraggedNewSpot!=null){
            onDraggedNewSpot();
        }
    }
}
