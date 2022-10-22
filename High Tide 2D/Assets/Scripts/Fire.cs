using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFriendly=true;
    public int dmg=30;
    public float duration=5;
    public bool active=false;
    public float tickRate=.5f;
    static List<GameObject> affectedEnemies;

    
    void Awake(){
        //destroyAfterTime(duration);
        affectedEnemies = new List<GameObject>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Global.curr.gamePhase=="shop"){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(Global.curr.gamePhase=="fight" && active){
            if(collision.gameObject!=null){
                GameObject g = collision.gameObject;
                if(g.GetComponent<Warrior>()!=null && g.GetComponent<Warrior>().attributes.isFriendly!=isFriendly && !affectedEnemies.Contains(g)){
                    affectedEnemies.Add(g);
                    g.GetComponent<DamageOverTime>().takeDOT(dmg, tickRate);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(Global.curr.gamePhase=="fight"){
            if(collision.gameObject!=null){
                GameObject g = collision.gameObject;
                if(g.GetComponent<Warrior>()!=null && g.GetComponent<Warrior>().attributes.isFriendly!=isFriendly && affectedEnemies.Contains(g)){
                    affectedEnemies.Remove(g);
                    g.GetComponent<DamageOverTime>().stopDOT();
                }
            }
        }
    }

    IEnumerator destroyAfterTime(float seconds){
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public void activate(){
        active=true;
    }
}
