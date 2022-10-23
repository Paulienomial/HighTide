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
            if (Global.curr.enemyWaveDeathCount == 0 && !waveEnd)
            {
                CancelInvoke();
                WaveBarController.curr.setHealth(WaveBarController.curr.getMaxHealth());
                resetWave();

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
            foreach(GameObject deadEnemy in Global.curr.deadEnemies){
                Destroy(deadEnemy);
            }
            Global.curr.deadEnemies.Clear();
            //StatScreens.curr.showAfterWaveScreen();
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
            
            
            //set gamephase to shop
            Global.curr.gamePhase = "shop";
            Global.curr.shopButton.SetActive(true);
            Global.curr.playButton.SetActive(true);
            //////////////////////////
            foreach (GameObject current in Global.curr.defenders)
            {
                current.transform.position = current.GetComponent<Warrior>().coordinates;
                //resetting positions
                current.SetActive(true);
                current.GetComponent<FightManager>().inCombat = false;
                current.GetComponent<FightManager>().isAlive = true;
                current.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
                //current.GetComponent<HealthBarUpdate>().hpBar.setHealth(current.GetComponent<Warrior>().maxHealth);
                current.GetComponent<FightManager>().a.hp = current.GetComponent<Warrior>().maxHealth;
                current.GetComponent<Warrior>().attributes.hp = current.GetComponent<Warrior>().maxHealth;

            }
            Global.curr.gold+= Waves.curr.waves[Global.curr.waveNum-1].bonusGold;

            if(Global.curr.waveNum == Waves.curr.waves.Count && Global.curr.CityHealth>0){
                Events.curr.winGame();
                return;
            }

            Global.curr.waveNum++;
            Global.curr.resetShop();
            
            WaveBarController.curr.setTopText("Next wave:");
            Events.curr.waveComplete();//trigger wave complete event
        }
    }
}
