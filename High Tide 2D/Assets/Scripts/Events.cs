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

    public event Action<GameObject> onPurchaseDefender;
    public void purchaseDefender(GameObject g){
        if(onPurchaseDefender!=null){
            onPurchaseDefender(g);
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

    public event Action onWaveStart;
    public void waveStart(){
        if(onWaveStart!=null){
            onWaveStart();
        }
    }

    public event Action<GameObject,GameObject> afterHit;
    public void hit(GameObject g1, GameObject g2){//g1 hit g2
        if(afterHit!=null){
            afterHit(g1,g2);
        }
    }

    public event Action<GameObject,int,int> onUpgradeDefender;
    public void upgradeDefender(GameObject g, int i1=0, int i2=0){
        if(onUpgradeDefender!=null){
            onUpgradeDefender(g,i1,i2);
        }
    }

    public event Action<GameObject> onSellDefender;
    public void sellDefender(GameObject g){
        if(onSellDefender!=null){
            onSellDefender(g);
        }
    }

    public event Action afterReset;
    public void reset(){
        if(afterReset!=null){
            afterReset();
        }
    }

    public event Action<GameObject> onSetDefender;
    public void setDefender(GameObject g){
        if(onSetDefender!=null){
            onSetDefender(g);
        }
    }

    public event Action<GameObject> onDie;
    public void die(GameObject g){
        if(onDie!=null){
            onDie(g);
        }
    }

    public event Action<GameObject> onSpawnEnemy;
    public void spawnEnemy(GameObject g){
        if(onSpawnEnemy!=null){
            onSpawnEnemy(g);
        }
    }

    public event Action onGameOver;
    public void gameOver(){
        if(onGameOver!=null){
            onGameOver();
        }
    }

    public event Action onWinGame;
    public void winGame(){
        if(onWinGame!=null){
            onWinGame();
        }
    }

    public event Action onSetWaves;
    public void setWaves(){
        if(onSetWaves!=null){
            onSetWaves();
        }
    }

    public event Action onReroll;
    public void reroll(){
        if(onReroll!=null){
            onReroll();
        }
    }

    public event Action onSetInitVols;
    public void setInitVols(){
        if(onSetInitVols!=null){
            onSetInitVols();
        }
    }
}
