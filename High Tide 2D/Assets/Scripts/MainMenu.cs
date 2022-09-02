using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource menuTheme;
    public AudioSource ClickSound;
    public Slider MainVolSlider;
    public Slider FXVolSlider;
    public Slider MusicVolSlider;

    [SerializeField]
    GameObject SettingsPopup;

    public float mainVol;
    public float FXVol;
    public float MusicVol;

    public void Update()
    {
        mainVol = MainVolSlider.value;

        if(FXVolSlider.value > mainVol)
        {
            FXVolSlider.value = mainVol;
        }
        FXVol = FXVolSlider.value;

        if (MusicVolSlider.value > mainVol)
        {
            MusicVolSlider.value = mainVol;
        }
        MusicVol = MusicVolSlider.value;

        menuTheme.volume = MusicVol;
        ClickSound.volume = FXVol;
    }

    public void playGame()
    {
        ClickSound.Play();
        PlayerPrefs.SetFloat("mainvol", mainVol);
        PlayerPrefs.SetFloat("fxvol", FXVol);
        PlayerPrefs.SetFloat("musicvol", MusicVol);
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenSettings()
    {
        ClickSound.Play();
        SettingsPopup.active = true;
    }

    public void CloseSettings()
    {
        ClickSound.Play();
        SettingsPopup.active = false;
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
