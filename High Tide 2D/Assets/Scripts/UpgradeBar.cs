using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
{
    public Slider slider;
    public Slider smallSlider;
    public Slider largeSlider;

    void Start(){
        slider=smallSlider;
        largeSlider.gameObject.SetActive(false);
    }
    public void setMax(int m){
        if(m==2){
            slider=smallSlider;
            largeSlider.gameObject.SetActive(false);
            smallSlider.gameObject.SetActive(true);
        }
        if(m==3){
            slider=largeSlider;
            largeSlider.gameObject.SetActive(true);
            smallSlider.gameObject.SetActive(false);
        }
        slider.maxValue = m;
    }

    public void setVal(int v){
        slider.value=v;
    }
}
