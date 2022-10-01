using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAnimation : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play(string animationName, string audioName="", int audioCount=1){
        string audioSuffix="";
        int minInclusive=0, maxExclusive=0;
        if (audioCount>1){
            minInclusive=1;
            maxExclusive=audioCount+1;
            audioSuffix = Random.Range(minInclusive, maxExclusive).ToString();
            Debug.Log("Rand num:" +audioSuffix);
        }

        AudioSystem.curr.createAndPlaySound(audioName+ audioSuffix );

        anim = gameObject.GetComponentInChildren<Animator>();
        anim.Play(animationName ,-1,0f);
    }
}
