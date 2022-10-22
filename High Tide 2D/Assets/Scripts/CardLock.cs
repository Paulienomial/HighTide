using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lockBorder;
    public GameObject lockBtn;
    public bool locked;
    public bool placed;
    void Start()
    {
        locked = false;
        placed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleLock(){
        if(!locked){
            AudioSystem.curr.createAndPlaySound("doorUnlock",.8f,1);
            locked = true;
            lockBorder.SetActive(true);
        }else{
            AudioSystem.curr.createAndPlaySound("doorUnlock",1.4f,1);
            locked = false;
            lockBorder.SetActive(false);
        }
        Highlight.curr.unFocus();

        //De-activate the lock buttons for the cards
        foreach(Transform child in Tutorial.curr.cardsContainer.transform){
            CardLock cl = child.gameObject.GetComponent<CardLock>();
            cl.lockBtn.SetActive(false);
        }
    }
}
