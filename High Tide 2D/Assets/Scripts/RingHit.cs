using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingHit : MonoBehaviour
{
    public int damage;
    public bool isFriendly;
    bool isActive;
    List<GameObject> enemiesHit = new List<GameObject>();
    void Awake()
    {
        damage = 200;
        isFriendly = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.curr.gamePhase=="shop" || Global.curr.CityHealth<=0){
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision){
        if(Global.curr.gamePhase=="fight" && isActive){
            if(collision.gameObject!=null){
                GameObject g = collision.gameObject;
                if(g.GetComponent<Warrior>()!=null && g.GetComponent<Warrior>().attributes.isFriendly!=isFriendly && !enemiesHit.Contains(g)){
                    enemiesHit.Add(g);
                    g.GetComponent<FightManager>().takeDamage( damage );
                }
            }
        }
    }

    public void destroy(){
        Destroy(gameObject);
    }
    public void activate(){
        isActive=true;
    }
}
