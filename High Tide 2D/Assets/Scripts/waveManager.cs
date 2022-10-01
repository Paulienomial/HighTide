using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{

    public static waveManager curr;
    public bool waveEnd = false;
    public bool waveLost = false;

    void Start()
    {
        
    }


    void Update()
    {
        waveEnd = waveComplete();
    }

    void Awake()
    {
        curr = this;
    }

    public bool waveComplete()
    {
        if (Global.curr.waveStart)
        {
            //Debug.Log(Global.curr.enemyWaveDeathCount);
            if (Global.curr.enemyWaveDeathCount == 0 && !waveEnd)
            {
                CancelInvoke();
                resetWave();
                Debug.Log("wave complete");
                WaveBarController.curr.setHealth(WaveBarController.curr.getMaxHealth());
                return true;
            }
            else
            {
                return false;
            }
        }
        else 
        {
            return false;
        }
        
    }

    public void resetWave()
    {
        if (!Global.curr.gameOver && Global.curr.gamePhase == "fight")
        {
            StatScreens.curr.showAfterWaveScreen();
            //Debug.Log("Resetting Wave");
            AudioScript.curr.stopBattleTheme();
            if (waveLost)
            {
                AudioScript.curr.playWaveFailedAndMain();
            }
            else
            {
                AudioScript.curr.playVictoryAndMain();
            }
            waveLost = false;
            Global.curr.waveStart = false;
            Global.curr.waveNum++;
            WaveBarController.curr.setText("Wave " + Global.curr.waveNum);
            WaveBarController.curr.setTopText("Next wave:");
            Global.curr.gamePhase = "shop";
            Global.curr.resetShop();
            foreach (GameObject current in Global.curr.defenders)
            {
                current.transform.position = current.GetComponent<Warrior>().coordinates;
                Debug.Log("Resetting positions");
                current.SetActive(true);
                current.GetComponent<FightManager>().inCombat = false;
                current.GetComponent<FightManager>().isAlive = true;
                current.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
                //current.GetComponent<HealthBarUpdate>().hpBar.setHealth(current.GetComponent<Warrior>().maxHealth);
                current.GetComponent<FightManager>().a.hp = current.GetComponent<Warrior>().maxHealth;
                current.GetComponent<Warrior>().attributes.hp = current.GetComponent<Warrior>().maxHealth;

            }
            //Global.curr.gold+=10;
            Events.curr.waveComplete();//trigger wave complete event
        }
    }
}
