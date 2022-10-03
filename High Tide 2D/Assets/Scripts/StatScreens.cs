using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatScreens : MonoBehaviour
{
    public static StatScreens curr;

    [SerializeField]
    GameObject AfterWaveScreen;
    public TextMeshProUGUI livesLost;
    public TextMeshProUGUI enemiesSlain;
    public TextMeshProUGUI friendlyCasualties;
    public TextMeshProUGUI goldFarmers;
    public TextMeshProUGUI goldEnemies;
    public TextMeshProUGUI bonusGold;

    public int lostLives = 0;
    public int enemiesKilled = 0;
    public int fallenBrothers = 0;
    public int farmGold = 0;
    public int enemyGold = 0;
    public int extraGold = 2;

    [SerializeField]
    GameObject GameOverScreen;
    public TextMeshProUGUI totalEnemiesKilled;
    public TextMeshProUGUI wavesSurvived;
    public TextMeshProUGUI goldEarned;
    public TextMeshProUGUI goldSpent;

    public int totalDeadFoes = 0;
    public int totalGold = 0;
    public int totalSpent = 0;

    void Awake()
    {
        curr = this;
    }

    void Update()
    {
        if(farmGold != 0)
        {
            Debug.Log("Farmgold = " + farmGold.ToString());
        }
    }

    public void showAfterWaveScreen()
    {
        AfterWaveScreen.active = true;
        writeStats("Afterwave");
    }

    public void closeAfterWaveScreen()
    {
        AfterWaveScreen.active = false;
        resetWaveStats();
    }

    public void showGameOverScreen()
    {
        GameOverScreen.active = true;
        writeStats("Gameover");
    }

    void resetWaveStats()
    {
        curr.lostLives = 0;
        curr.enemiesKilled = 0;
        curr.fallenBrothers = 0;
        curr.farmGold = 0;
        curr.enemyGold = 0;
        curr.extraGold = 2;
    }

    void writeStats(string type)
    {
        if(type == "Afterwave")
        {
            livesLost.text = lostLives.ToString();
            enemiesSlain.text = enemiesKilled.ToString();
            friendlyCasualties.text = fallenBrothers.ToString();
            goldFarmers.text = "+ " + curr.farmGold.ToString();
            goldEnemies.text = "+ " + enemyGold.ToString();
            bonusGold.text = "+ " + extraGold.ToString();
        }
        else
        {
            totalEnemiesKilled.text = totalDeadFoes.ToString();
            wavesSurvived.text = Global.curr.waveNum.ToString();
            goldEarned.text = totalGold.ToString();
            goldSpent.text = totalSpent.ToString();
        }
    }

}
