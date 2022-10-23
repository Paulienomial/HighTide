using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource menuTheme;
    public AudioSource ClickSound;
    public AudioSource swordSound;
    public Slider MainVolSlider;
    public Slider FXVolSlider;
    public Slider MusicVolSlider;
    public static float mainPref = -1f;
    public static float fxPref = -1f;
    public static float musicPref = -1f;
    public static float defaultVol=.2f;

    [SerializeField]
    GameObject SettingsPopup;

    public float mainVol;
    public float FXVol;
    public float MusicVol;



    //Charl
    float fxPercOfMain = 1;
    float musicPercOfMain = 1;
    bool changingMainVol=false;
    bool changingFxVol=false;
    bool changingMusicVol=false;
    int currentInteraction=-1;
    public LayerMask sliderMask;
    bool mouseDownOnFX=false;

    void Start(){
        //updateVolumes();
        setInitVols();
    }

    public void Update()
    {
        if(currentInteraction==1){
            menuTheme.volume=0;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0)){
            swordSound.Stop();
        }

        if(Input.GetKeyUp(KeyCode.Mouse0)){
            if(currentInteraction!=-1){
                setPlayerPrefs();
            }
            int oldInteraction = currentInteraction;
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
        /*mainVol = MainVolSlider.value;

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
        ClickSound.volume = FXVol;*/
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

    //Charl
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
            playFXPreview();
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
        Debug.Log("Released fx");
        musicPercOfMain = MusicVolSlider.value/MainVolSlider.value;
        currentInteraction=-1;
        menuTheme.volume = MusicVolSlider.value;
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
        swordSound.Play();
    }

    void changeMainVolume(float vol){
        if(currentInteraction!=1){
            menuTheme.volume = MusicVolSlider.value;
        }
        ClickSound.volume = FXVolSlider.value;
        swordSound.volume = FXVolSlider.value;
    }

    void changeFXVolume(float vol){
        ClickSound.volume = FXVolSlider.value;
        swordSound.volume = FXVolSlider.value;
    }

    void changeMusicVolume(float vol){
        if(currentInteraction!=1){
            menuTheme.volume = MusicVolSlider.value;
        }
    }

    void setInitVols(){
        setInitPlayerPrefs();
        setSlidersToPrefs();
        updateVolumes();
    }
    void setInitPlayerPrefs(){
        Debug.Log("setting init player prefs");
        if( mainPref==-1 ){
            Debug.Log("could not find main v");
            mainPref =defaultVol;
        }
        if( fxPref==-1 ){
            fxPref=defaultVol;
        }
        if( musicPref==-1 ){
            musicPref=defaultVol;
        }
    }

    void setSlidersToPrefs(){
        MainVolSlider.value = mainPref;
        FXVolSlider.value = fxPref;
        MusicVolSlider.value = musicPref;
    }

    void setPlayerPrefs(){
        mainPref = MainVolSlider.value;
        fxPref = FXVolSlider.value;
        musicPref = MusicVolSlider.value;
    }

    ///////////////////////////////////////////////////////////////////

    public void playGame()
    {
        ClickSound.Play();
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenSettings()
    {
        ClickSound.Play();
        SettingsPopup.SetActive(true);
    }

    public void CloseSettings()
    {
        ClickSound.Play();
        SettingsPopup.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
