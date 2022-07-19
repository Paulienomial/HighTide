using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorRender : MonoBehaviour
{
    public Sprite sprite;
    public GameObject warrior;
    Animator animator;
    AnimatorOverrideController animOverride;

    // Start is called before the first frame update
    void Awake()
    {
        //Create a new animation override controller
        animator = GetComponent<Animator>();
        animOverride = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animOverride;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            animator.SetInteger("state",0);//do idle animation
        }
        if(Input.GetKeyDown(KeyCode.W)){
            animator.SetInteger("state",1);//do walk animation
        }
        if(Input.GetKeyDown(KeyCode.E)){
            animator.SetInteger("state",2);//do attack animation
        }
        if(Input.GetKeyDown(KeyCode.R)){
            animator.SetInteger("state",3);//do attack animation
        }
    }

    public void setSprite(){
        //Override all the animation states
        string[] animationStates = {"idle","walk","attack","dead"};
        foreach(string state in animationStates){
            //Debug.Log(state);
            string clipPath = "Art/Animation/"+gameObject.GetComponent<Warrior>().attributes.name+"/"+state;//Eg: Art/Animation/foot soldier/idle
            AnimationClip clip = Resources.Load<AnimationClip>(clipPath);
            if(clip!=null){
                animOverride[state] = clip;
            }
        }
    }


}
