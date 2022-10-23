using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityUpgradeBarController : MonoBehaviour
{
    public Slider slider;
    public static CityUpgradeBarController curr;
    
    void Awake(){
        curr=this;
    }

    public void setVal(int val){
        slider.value=val;
    }
}
