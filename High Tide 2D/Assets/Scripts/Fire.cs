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

    
    void Awake(){
        destroyAfterTime(duration);
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
                if(collision.gameObject.GetComponent<Warrior>()!=null && collision.gameObject.GetComponent<Warrior>().attributes.isFriendly!=isFriendly){
                    GameObject g = collision.gameObject;
                    g.GetComponent<DamageOverTime>().takeDOT(dmg, tickRate);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(Global.curr.gamePhase=="fight"){
            if(collision.gameObject!=null){
                if(collision.gameObject.GetComponent<Warrior>()!=null && collision.gameObject.GetComponent<Warrior>().attributes.isFriendly!=isFriendly){
                    GameObject g = collision.gameObject;
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
