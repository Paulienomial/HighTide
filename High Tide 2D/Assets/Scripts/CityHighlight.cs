using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHighlight : MonoBehaviour
{
    public GameObject ch;//city highlight
    public GameObject ch2;//city highlight text
    // Start is called before the first frame update
    void Start()
    {
        Events.curr.onHoverCity += show;
        Events.curr.onNotHoverCity += hide;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void show(){
        if(!ShopSystem.curr.shopOpen && !GridSystem.curr.placingPhase && !GridSystem.curr.draggingPhase){
            ch.GetComponent<SpriteRenderer>().color=(Color)(new Color32(21,80,240,120));//transparent blue
            ch2.GetComponent<SpriteRenderer>().color=(Color)(new Color32(255,255,255,255));//white
        }
    }

    void hide(){
        ch.GetComponent<SpriteRenderer>().color=(Color)(new Color32(0,0,0,0));//fully transparent
        ch2.GetComponent<SpriteRenderer>().color=(Color)(new Color32(0,0,0,0));//fully transparent
    }

    void OnMouseOver(){
        if(ShopSystem.curr.shopOpen==false && GridSystem.curr.draggingPhase==false && GridSystem.curr.placingPhase==false && Input.GetMouseButtonDown(0)){
            HighlightSelected.curr.select(gameObject);
        }
    }
}
