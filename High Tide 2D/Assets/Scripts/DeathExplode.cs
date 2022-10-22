using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplode : MonoBehaviour
{
    public int damage;
    public bool isFriendly;
    bool active;
    List<GameObject> enemiesHit = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        damage = 30;
        isFriendly = false;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.curr.gamePhase=="shop"){
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision){
        if(Global.curr.gamePhase=="fight" && active){
            if(collision.gameObject!=null){
                GameObject g = collision.gameObject;
                if(g.GetComponent<Warrior>()!=null && g.GetComponent<Warrior>().attributes.isFriendly!=isFriendly && !enemiesHit.Contains(g)){
                    enemiesHit.Add(g);
                    g.GetComponent<FightManager>().takeDamage( damage );
                }
            }
        }
    }

    public void activate(){
        active=true;
    }
}
