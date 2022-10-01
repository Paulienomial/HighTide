using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveBarController : MonoBehaviour
{
    public Slider slider;
    public static WaveBarController curr;
    public TextMeshProUGUI text;
    public TextMeshProUGUI topText;

    void Awake(){
        curr=this;
    }
    public void setMaxHealth(int maxHealth){
        slider.maxValue=maxHealth;
    }
    public void setHealth(int health){
        slider.value = health;
    }

    public int getMaxHealth(){
        return (int)slider.maxValue;
    }

    public int getHealth(){
        return (int)slider.value;
    }

    public void setText(string textVal){
        text.text=textVal;
    }

    public void setTopText(string textVal){
        topText.text = textVal;
    }
}
