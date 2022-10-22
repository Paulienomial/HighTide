using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    public static Lock curr;
    // Start is called before the first frame update
    void Awale()
    {
        curr=this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void askLock(){
        AudioSystem.curr.createAndPlaySound("btnClick",1,1);

        //Set scroll dimensions and pos
        Tutorial.curr.scroll.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 165);
        Tutorial.curr.scroll.GetComponent<RectTransform>().localPosition = new Vector2(0,250);
        
        //Highlight the cards
        Highlight.curr.darkenAllExcept(Tutorial.curr.cardsContainer);

        //Activate the lock buttons for the cards
        foreach(Transform child in Tutorial.curr.cardsContainer.transform){
            CardLock cl = child.gameObject.GetComponent<CardLock>();
            cl.lockBtn.SetActive(true);
        }
    }
}
