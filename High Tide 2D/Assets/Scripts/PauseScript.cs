using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    [SerializeField]
    GameObject pauseScreen;
    [SerializeField]
    GameObject settingsScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Global.curr.gamePaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        pauseScreen.active = true;
        AudioScript.curr.pauseMusic();
        Time.timeScale = 0;
        Global.curr.gamePaused = true;
    }

    public void restartGame()
    {
        AudioScript.curr.playButtonClickSound();
        PlayerPrefs.SetFloat("mainvol", Global.curr.MainVolume);
        PlayerPrefs.SetFloat("fxvol", Global.curr.FXVolume);
        PlayerPrefs.SetFloat("musicvol", Global.curr.MusicVolume);
        resumeGame();
        SceneManager.LoadScene("SampleScene");
    }

    public void toMenu()
    {
        AudioScript.curr.playButtonClickSound();
        PlayerPrefs.SetFloat("mainvol", Global.curr.MainVolume);
        PlayerPrefs.SetFloat("fxvol", Global.curr.FXVolume);
        PlayerPrefs.SetFloat("musicvol", Global.curr.MusicVolume);
        resumeGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void resumeGame()
    {
        AudioScript.curr.playButtonClickSound();
        pauseScreen.active = false;
        settingsScreen.active = false;
        AudioScript.curr.resumeMusic();
        Time.timeScale = 1;
        Global.curr.gamePaused = false;
    }

    public void OpenSettings()
    {
        AudioScript.curr.playButtonClickSound();
        //pauseScreen.active = false;
        settingsScreen.active = true;
    }

    public void CloseSettings()
    {
        AudioScript.curr.playButtonClickSound();
        //pauseScreen.active = true;
        settingsScreen.active = false;
    }

    public void pauseButtonPress()
    {
        if (Global.curr.gamePaused)
        {
            resumeGame();
        }
        else
        {
            pauseGame();
        }
    }
}
