using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorRender : MonoBehaviour
{
    public Sprite sprite;
    public GameObject warrior;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSprite(){
        string path="Art/Warriors/"+gameObject.GetComponent<Warrior>().attributes.name;
        Sprite mySprite = Resources.Load<Sprite>(path);
        if(mySprite!=null){//if sprite found then load it, else use default sprite
            gameObject.GetComponent<SpriteRenderer>().sprite=mySprite;
        }
    }
}
