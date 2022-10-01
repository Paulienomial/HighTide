using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioCreator : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    bool startedPlaying=false;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource!=null  && audioSource.clip!=null){
            if(startedPlaying && !audioSource.isPlaying){
                Destroy(gameObject);
            }
        }
    }

    public void createAndPlaySound(string name, float pitch=1f, float volume=.5f){
        if(name=="fanfareCityLvl3"){
            volume=1f;
        }

        AudioClip audioClip = Resources.Load("Audio/" + name) as AudioClip;
        if(audioClip!=null){
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0;
            audioSource.pitch = pitch;
            audioSource.volume = volume;
            audioSource.clip = audioClip;
            audioSource.Play();
            startedPlaying=true;
        }else{
            Destroy(gameObject);
        }
    }
}
