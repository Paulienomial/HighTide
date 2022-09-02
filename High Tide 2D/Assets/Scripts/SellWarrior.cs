using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Global.curr.gold+=2;
            GameObject g = HighlightSelected.curr.lastSelected;
            Global.curr.defenders.Remove(g);
            HighlightSelected.curr.deselect();
            Destroy(g);
        }
    }
}
