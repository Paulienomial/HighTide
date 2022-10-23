using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OnceOffAnimation : MonoBehaviour
{
    Animator anim;
    Animation lightningAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play(string animationName, string textValue="", string audioName="", int audioCount=1){
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = textValue;

        string audioSuffix="";
        int minInclusive=0, maxExclusive=0;
        if (audioCount>1){
            minInclusive=1;
            maxExclusive=audioCount+1;
            audioSuffix = Random.Range(minInclusive, maxExclusive).ToString();
        }

        AudioSystem.curr.createAndPlaySound(audioName+ audioSuffix );

        anim = gameObject.GetComponentInChildren<Animator>();
        anim.Play(animationName ,-1,0f);
    }
}
