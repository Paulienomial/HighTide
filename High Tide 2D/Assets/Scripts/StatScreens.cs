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
    void Awake()
    {
        curr = this;
    }

    public void showAfterWaveScreen()
    {
        AfterWaveScreen.active = true;
        writeStats();
    }

    public void closeAfterWaveScreen()
    {
        resetWaveStats();
        AfterWaveScreen.active = false;
    }

    void resetWaveStats()
    {
        lostLives = 0;
        enemiesKilled = 0;
        fallenBrothers = 0;
        farmGold = 0;
        enemyGold = 0;
        extraGold = 2;
    }

    void writeStats()
    {
        livesLost.text = lostLives.ToString();
        enemiesSlain.text = enemiesKilled.ToString();
        friendlyCasualties.text = fallenBrothers.ToString();
        goldFarmers.text = "+ " + farmGold.ToString();
        goldEnemies.text = "+ " + enemyGold.ToString();
        bonusGold.text = "+ " + extraGold.ToString();
    }

}
