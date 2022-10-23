using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController curr;
    public GameObject onceOffAnimation;
    public GameObject imageAnimation;
    // Start is called before the first frame update
    void Awake()
    {
        curr=this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            play("lvl2Upgrade", new Vector3(0f,0f,0f),"","happyTrumpet",2,"image");
        }
    }

    public void play(string animationName, Vector3 pos, string textValue="", string audioName="", int audioCount=1, string animationType="text"){
        if(animationType=="text"){
            GameObject g = Instantiate(onceOffAnimation, pos, Quaternion.identity);
            g.GetComponent<OnceOffAnimation>().play(animationName, textValue, audioName, audioCount);
        }else{
            GameObject g = Instantiate(imageAnimation, pos, Quaternion.identity);
            g.GetComponent<ImageAnimation>().play(animationName, audioName, audioCount);
        }
    }

    public void createAndPlay(string animationName, Vector3 pos){
        if(animationName=="cityUpgradeLvl2"){
            play("cityUpgrade", pos, "", "fanfareCityLvl2", 1, "image");
        }else if(animationName=="cityUpgradeLvl3"){
            play("cityUpgrade", pos, "", "fanfareCityLvl3", 1, "image");
        }
    }
}
