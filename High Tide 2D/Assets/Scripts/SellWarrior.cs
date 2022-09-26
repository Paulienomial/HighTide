using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SellWarrior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sellWarrior(){
        if(HighlightSelected.curr.lastSelected!=null && HighlightSelected.curr.lastSelected.GetComponent<Warrior>()){
            GameObject g = HighlightSelected.curr.lastSelected;
            Global.curr.gold += getSellPrice();
            Global.curr.defenders.Remove(g);
            HighlightSelected.curr.deselect();
            Destroy(g);
        }
    }

    public static int getSellPrice(){    
        if(HighlightSelected.curr.lastSelected!=null && HighlightSelected.curr.lastSelected.GetComponent<Warrior>()){
            GameObject g = HighlightSelected.curr.lastSelected;
            return g.GetComponent<Warrior>().attributes.price/2 + g.GetComponent<Warrior>().attributes.mergeCount;
        }
        return -1;
    }
}
