using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{
    public AudioSource buttonSound;
    public AudioSource battleHorn;
    public AudioSource bowAttack;
    public AudioSource enemyAttack;
    public AudioSource swordAttack;
    public AudioSource MainTheme;
    public AudioSource BattleTheme;
    public AudioSource VictorySound;
    public AudioSource waveFailed;
    public AudioSource placeWarrior;

    public Slider MainVolSlider;
    public Slider FXVolSlider;
    public Slider MusicVolSlider;

    public int currMusic = 0;
    /*
    0: Main Theme
    1: Battle Theme
    */


    public static AudioScript curr;

    void Awake()
    {
        curr = this;
    }

    private void Start()
    {
        Global.curr.MainVolume = PlayerPrefs.GetFloat("mainvol");
        MainVolSlider.value = Global.curr.MainVolume;
        Global.curr.FXVolume = PlayerPrefs.GetFloat("fxvol");
        FXVolSlider.value = Global.curr.FXVolume;
        Global.curr.MusicVolume = PlayerPrefs.GetFloat("musicvol");
        MusicVolSlider.value = Global.curr.MusicVolume;

        changeMainVolume(Global.curr.MainVolume);
        changeFXVolume(Global.curr.FXVolume);
        changeMusicVolume(Global.curr.MusicVolume);
    }

    public void Update()
    {
        Global.curr.MainVolume = MainVolSlider.value;

        if (FXVolSlider.value > Global.curr.MainVolume)
        {
            FXVolSlider.value = Global.curr.MainVolume;
        }
        Global.curr.FXVolume = FXVolSlider.value;

        if (MusicVolSlider.value > Global.curr.MainVolume)
        {
            MusicVolSlider.value = Global.curr.MainVolume;
        }
        Global.curr.MusicVolume = MusicVolSlider.value;

        changeMainVolume(Global.curr.MainVolume);
        changeFXVolume(Global.curr.FXVolume);
        changeMusicVolume(Global.curr.MusicVolume);
    }

    public void changeMainVolume(float vol)
    {
        Global.curr.MainVolume = vol;
        buttonSound.volume = vol;
        battleHorn.volume = vol;
        bowAttack.volume = vol;
        enemyAttack.volume = vol;
        swordAttack.volume = vol;
        MainTheme.volume = vol;
        BattleTheme.volume = vol;
        VictorySound.volume = vol;
        waveFailed.volume = vol;
        placeWarrior.volume = vol;
    }

    public void changeFXVolume(float vol)
    {
        Global.curr.FXVolume = vol;
        buttonSound.volume = vol;
        battleHorn.volume = vol;
        bowAttack.volume = vol;
        enemyAttack.volume = vol;
        swordAttack.volume = vol;
        placeWarrior.volume = vol;
    }

    public void changeMusicVolume(float vol)
    {
        Global.curr.MusicVolume = vol;
        MainTheme.volume = vol;
        BattleTheme.volume = vol;
        VictorySound.volume = vol;
        waveFailed.volume = vol;
    }

    public void playAttackSound(GameObject type)
    {
        Debug.Log("Attack type is " + type.GetComponent<Warrior>().attributes.attacksound);
        if(type.GetComponent<Warrior>().attributes.attacksound == "sword")
        {

            playSwordAttackSound();
        }
        else
        {
            if (type.GetComponent<Warrior>().attributes.attacksound == "bow")
            {
                playBowAttackSound();
            }
            else
            {
                if (type.GetComponent<Warrior>().attributes.attacksound == "enemymelee")
                {
                    playEnemyMeleeSound();
                }
            }
        }
    }

    public void playButtonClickSound()
    {
        buttonSound.Play();
    }

    public void playBattleHornSound()
    {
        battleHorn.Play();
    }

    public void playBowAttackSound()
    {
        bowAttack.Play();
    }

    public void playEnemyMeleeSound()
    {
        enemyAttack.Play();
    }

    public void playSwordAttackSound()
    {
        swordAttack.Play();
    }

    public void playMainTheme()
    {
        currMusic = 0;
        MainTheme.Play();
    }

    public void playBattleTheme()
    {
        currMusic = 1;
        BattleTheme.Play();
    }

    public void stopMainTheme()
    {
        MainTheme.Stop();
    }

    public void stopBattleTheme()
    {
        BattleTheme.Stop();
    }

    public void playVictoryAndMain()
    {
        VictorySound.Play();
        MainTheme.PlayDelayed(5);
        currMusic = 0;
    }

    public void playWaveFailedAndMain()
    {
        waveFailed.Play();
        MainTheme.PlayDelayed(5);
        currMusic = 0;
    }

    public void playPlaceWarrior()
    {
        placeWarrior.Play();
    }

    public void pauseMusic()
    {
        if(currMusic == 0)
        {
            MainTheme.Pause();
            VictorySound.Stop();
            waveFailed.Stop();
        }
        else
        {
            BattleTheme.Pause();
            battleHorn.Stop();
        }
    }

    public void resumeMusic()
    {
        if (currMusic == 0)
        {
            MainTheme.Play();
        }
        else
        {
            if (currMusic == 1)
            {
                BattleTheme.Play();
            }
            else
            {
                if (currMusic == 2)
                {
                    VictorySound.Play();
                }
                else
                {
                    waveFailed.Play();
                }
            }

        }
    }
}
