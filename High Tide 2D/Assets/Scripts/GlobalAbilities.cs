using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAbilities : MonoBehaviour
{
    // Start is called before the first frame update 
    void Start()
    {
        Events.curr.onSetDefender += setDefender;
        Events.curr.onDie += warriorDie;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setDefender(GameObject g){
        if(!this) return;
        if(g.GetComponent<Warrior>().attributes.ability=="Teleport"){
            Ability myAbility = g.AddComponent<Teleport>() as Teleport;
            myAbility.go();
        }else if(g.GetComponent<Warrior>().attributes.ability=="Lightning"){
            Ability myAbility = g.AddComponent<Lightning>() as Lightning;
            myAbility.go();
        }else if(g.GetComponent<Warrior>().attributes.ability=="Green ring"){
            Ability myAbility = g.AddComponent<GreenRing>() as GreenRing;
            myAbility.go();
        }else if(g.GetComponent<Warrior>().attributes.name=="Large Jeffrey"){
            //g.GetComponent<BoxCollider2D>().size = new Vector2(1f,1f);
            //g.transform.localScale = new Vector2(1f,1f);
        }else if(g.GetComponent<Warrior>().attributes.name=="Medium Jeffrey"){
            //g.GetComponent<BoxCollider2D>().size = new Vector2(1f,1f);
            //g.transform.localScale = new Vector2(1.25f,1.25f);
        }else if(g.GetComponent<Warrior>().attributes.name=="Tiny Jeffrey"){
            //g.transform.localScale = new Vector2(1f,1f);
        }
    }

    void warriorDie(GameObject g){
        if(!this) return;
        if(g.GetComponent<Warrior>().attributes.ability=="Split 1" || g.GetComponent<Warrior>().attributes.ability=="Split 2"){
            Ability myAbility = g.AddComponent<Split>() as Split;
            myAbility.go();
        }
    }
}
