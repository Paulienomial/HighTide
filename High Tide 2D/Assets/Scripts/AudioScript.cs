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
    public AudioSource BattleTheme1;
    public AudioSource BattleTheme2;
    public AudioSource BattleTheme3;
    public AudioSource BattleTheme4;
    public AudioSource BattleTheme5;
    public AudioSource BossTheme1;
    public AudioSource BossTheme2;
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

    int BattleThemeLooper = 1;
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
        //updateVolumes();
        setInitVols();
    }

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0)){
            if(currentInteraction!=-1){
                setPlayerPrefs();
            }
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

    void setInitVols(){
        setInitPlayerPrefs();
        setSlidersToPrefs();
        updateVolumes();
    }
    void setInitPlayerPrefs(){
        Debug.Log("setting init player prefs");
        if( MainMenu.mainPref==-1 ){
            Debug.Log("could not find main v");
            MainMenu.mainPref =MainMenu.defaultVol;
        }
        if( MainMenu.fxPref==-1 ){
            MainMenu.fxPref=MainMenu.defaultVol;
        }
        if( MainMenu.musicPref==-1 ){
            MainMenu.musicPref=MainMenu.defaultVol;
        }
    }

    void setSlidersToPrefs(){
        MainVolSlider.value = MainMenu.mainPref;
        FXVolSlider.value = MainMenu.fxPref;
        MusicVolSlider.value = MainMenu.musicPref;
    }

    void setPlayerPrefs(){
        MainMenu.mainPref = MainVolSlider.value;
        MainMenu.fxPref = FXVolSlider.value;
        MainMenu.musicPref = MusicVolSlider.value;
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
            BattleTheme1.volume = vol;
            BattleTheme2.volume = vol;
            BattleTheme3.volume = vol;
            BossTheme1.volume = vol;
            BossTheme2.volume = vol;
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
            BattleTheme1.volume = vol;
            BattleTheme2.volume = vol;
            BattleTheme3.volume = vol;
            BossTheme1.volume = vol;
            BossTheme2.volume = vol;
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
        if(Global.curr.waveNum == 10)
        {
            BossTheme1.Play();
            currMusic = 4;
        }
        else
        {
            if (Global.curr.waveNum == 20)
            {
                BossTheme2.Play();
                currMusic = 5;
            }
            else
            {
                int PlayNum = BattleThemeLooper;
                /*if(PlayNum == 1)
                {
                    BattleThemeLooper = 2;
                    BattleTheme1.Play();
                    currMusic = 1;
                }
                else
                {
                    if(PlayNum == 2)
                    {
                        BattleThemeLooper = 3;
                        BattleTheme2.Play();
                        currMusic = 6;
                    }
                    else
                    {
                        BattleThemeLooper = 1;
                        BattleTheme3.Play();
                        currMusic = 7;
                    }
                }*/
                if(PlayNum==1){
                    BattleThemeLooper=2;
                    BattleTheme1.Play();
                    currMusic=1;
                }else if(PlayNum==2){
                    BattleThemeLooper=3;
                    BattleTheme2.Play();
                    currMusic=6;
                }else if(PlayNum==3){
                    BattleThemeLooper=4;
                    BattleTheme3.Play();
                    currMusic=7;
                }else if(PlayNum==4){
                    BattleThemeLooper=5;
                    BattleTheme4.Play();
                    currMusic=8;
                }else if(PlayNum==5){
                    BattleThemeLooper=1;
                    BattleTheme5.Play();
                    currMusic=9;
                }
                
            }
        }    
    }

    public void stopMainTheme()
    {
        MainTheme.Pause();
    }

    public void stopBattleTheme()
    {
        BattleTheme1.Stop();
        BattleTheme2.Stop();
        BattleTheme3.Stop();
        BattleTheme4.Stop();
        BattleTheme5.Stop();
        BossTheme1.Stop();
        BossTheme2.Stop();
    }

    public void playVictoryAndMain()
    {
        VictorySound.Play();
        StartCoroutine(playDelayedMain());
        currMusic = 0;
    }

    IEnumerator playDelayedMain()
    {
        yield return new WaitForSeconds(5);
        MainTheme.Play();
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
            BattleTheme1.Pause();
            BattleTheme2.Pause();
            BattleTheme3.Pause();
            BossTheme1.Pause();
            BossTheme2.Pause();
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
                BattleTheme1.Play();
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

        switch (currMusic)
        {
            case 0:
                MainTheme.Play();
                break;
            case 1:
                BattleTheme1.Play();
                break;
            case 2:
                VictorySound.Play();
                break;
            case 3:
                waveFailed.Play();
                break;
            case 4:
                BossTheme1.Play();
                break;
            case 5:
                BossTheme2.Play();
                break;
            case 6:
                BattleTheme2.Play();
                break;
            case 7:
                BattleTheme3.Play();
                break;
            case 8:
                BattleTheme4.Play();
                break;
            case 9:
                BattleTheme5.Play();
                break;
        }
    }
}
