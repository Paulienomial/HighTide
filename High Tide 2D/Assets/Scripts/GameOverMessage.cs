using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMessage : MonoBehaviour
{
    public GameObject goMessage;
    public TextMeshProUGUI goHeading;
    public TextMeshProUGUI goText;
    public TextMeshProUGUI btnText;
    bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOver=false;
        Events.curr.onGameOver += loseGame;
        Events.curr.onWinGame += winGame;
        goMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.curr.CityHealth <= 0 )
        {
            gameOver=true;
            CityHealthManager.curr.gameOver();
        }
    }

    void loseGame(){
        goMessage.SetActive(true);
        goText.text = "You reached wave "+Global.curr.waveNum+ "/" + Waves.curr.waves.Count;
    }

    void winGame(){
        goMessage.SetActive(true);
        goHeading.text = "VICTORY!!";
        goText.text = "You completed wave "+Waves.curr.waves.Count+ "/" + Waves.curr.waves.Count + "!!";
        btnText.text = "GO AGAIN";
    }
}
