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

    public GameObject settingsPopup;
    public int currMusic = 0;

    public Slider MainVolSlider;
    public Slider FXVolSlider;
    public Slider MusicVolSlider;

    float fxPercOfMain = 1;
    float musicPercOfMain = 1;
    bool changingMainVol=false;
    bool changingFxVol=false;
    bool changingMusicVol=false;
    int currentInteraction=-1;
    public LayerMask sliderMask;
    bool mouseDownOnFX=false;
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
        updateVolumes();
    }

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0)){
            int oldInteraction=currentInteraction;
            currentInteraction=-1;
            if(oldInteraction==0){
                changingMainVol=false;
                onMainRelease();
            }else if(oldInteraction==1){
                changingFxVol=false;
                onFXRelease();
            }else if(oldInteraction==2){
                changingMusicVol=false;
                onMusicRelease();
            }
        }
        
        
    }

    void updateVolumes(){
        if(MainVolSlider.value<=.01f){
            changeMainVolume(0);
        }else{
            changeMainVolume( MainVolSlider.value );
        }

        if(FXVolSlider.value<=.01f){
            changeFXVolume(0);
        }else{
            changeFXVolume( FXVolSlider.value );
        }

        if(MusicVolSlider.value<=.01f){
            changeMusicVolume(0);
        }else{
            changeMusicVolume( MusicVolSlider.value );
        }
        
        
        
    }
    public void onMainChange(){
        changingMainVol=true;
        if(MainVolSlider.value<.01f){
            MainVolSlider.value=.01f;
        }
        if(currentInteraction==-1 || currentInteraction==0){
            currentInteraction=0;//currently interacting with main vol slider
            float fxModified = fxPercOfMain*MainVolSlider.value;
            FXVolSlider.value = fxModified;
            float musicModified = musicPercOfMain*MainVolSlider.value;
            MusicVolSlider.value = musicModified;
        }else if(changingFxVol){
            float fxModified = fxPercOfMain*MainVolSlider.value;
            FXVolSlider.value = fxModified;
        }else if(changingMusicVol){
            float musicModified = musicPercOfMain*MainVolSlider.value;
            MusicVolSlider.value = musicModified;
        }
        updateVolumes();
    }

    void onMainRelease(){
        musicPercOfMain = MusicVolSlider.value/MainVolSlider.value;
        currentInteraction=-1;
    }

    public void onFXChange(){
        if(currentInteraction==-1){
            currentInteraction=1;//currently interacting with fx vol slider
            swordAttack.Play();
            MainTheme.volume=0;
        }
        changingFxVol=true;

        if(FXVolSlider.value > MainVolSlider.value){
            MainVolSlider.value = FXVolSlider.value;
            //musicPercOfMain = musicOldValue/MainVolSlider.value;
        }
        fxPercOfMain = FXVolSlider.value/MainVolSlider.value;
        //musicPercOfMain = MusicVolSlider.value/MainVolSlider.value;
        updateVolumes();
    }

    void onFXRelease(){
        Debug.Log("Release fx");
        musicPercOfMain = MusicVolSlider.value/MainVolSlider.value;
        currentInteraction=-1;
        swordAttack.Stop();
        updateVolumes();
    }

    public void onMusicChange(){
        if(currentInteraction==-1){
            currentInteraction=2;//currently interacting with music vol slider
        }
        changingMusicVol=true;

        if(MusicVolSlider.value > MainVolSlider.value){
            MainVolSlider.value = MusicVolSlider.value;
            //fxPercOfMain = fxOldValue/MainVolSlider.value;
        }
        //fxPercOfMain = FXVolSlider.value/MainVolSlider.value;
        musicPercOfMain = MusicVolSlider.value/MainVolSlider.value;
        updateVolumes();
    }

    void onMusicRelease(){
        fxPercOfMain = FXVolSlider.value/MainVolSlider.value;
        currentInteraction=-1;
    }

    void playFXPreview(){
        AudioSystem.curr.createAndPlaySound("sword", 1, 1f);
    }

    public void changeMainVolume(float vol)
    {
        Global.curr.MainVolume = vol;
        buttonSound.volume = vol;
        placeWarrior.volume = vol;
        bowAttack.volume = vol;
        enemyAttack.volume = vol;
        swordAttack.volume = vol;
        battleHorn.volume = vol;

        if(currentInteraction!=1){
            MainTheme.volume = vol;
            BattleTheme.volume = vol;
            VictorySound.volume = vol;
            waveFailed.volume = vol;
        }  
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
        if(currentInteraction!=1){
            MainTheme.volume = vol;
            BattleTheme.volume = vol;
            VictorySound.volume = vol;
            waveFailed.volume = vol;
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
