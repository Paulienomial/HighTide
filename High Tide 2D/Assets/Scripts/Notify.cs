using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notify : MonoBehaviour
{
    public GameObject notificationScroll;
    Animator animator;
    AnimatorOverrideController animOverride;
    public TextMeshProUGUI text;
    public static Notify curr;//singleton

    void Awake()
    {
        curr=this;//singleton
        animator = notificationScroll.GetComponent<Animator>();

        animator.SetInteger("state",0);//no animation
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)){
            show("Awesome!");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("notifyShow")){
            Debug.Log("playing notify");
            // Avoid any reload.
        }
    }

    public void show(string message){
        notificationScroll.SetActive(true);

        //set the text
        text.SetText(message);

        //reset animator, so animation will start playing again
        animator.Rebind();
        animator.Update(0);

        //play animation
        animator.SetInteger("state",1);//show animation
        
    }
}
