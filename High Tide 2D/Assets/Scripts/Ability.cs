using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public Warrior w;
    public WarriorAttributes.attr a;
    public FightManager fm;
    public Global global;
    void Awake()
    {
        w = gameObject.GetComponent<Warrior>();
        a = w.attributes;
        global= Global.curr;
        fm = gameObject.GetComponent<FightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void go(){//does ability
        
    }

    public virtual void go(GameObject g){
        
    }

    public virtual void go2(){//does ability
        
    }

    public virtual void go3(GameObject g,int i1,int i2){

    }

    public GameObject getDefenderAhead(){
        float tolerance = .05f;
        foreach(GameObject defender in global.defenders){
            Vector2 pos = gameObject.transform.position;
            Vector2 defenderPos = defender.transform.position;
            if(defender!=gameObject){
                if(defenderPos.y <= pos.y+tolerance && defenderPos.y >= pos.y-tolerance){//correct y
                    float x = pos.x+GridSystem.curr.gridCellSize;
                    if(defenderPos.x <= x+tolerance  && defenderPos.x >= x-tolerance){//correct x
                        return defender;
                    }
                }
            }
        }
        return null;
    }

    public bool noTargets(){
        return global.enemies.Count==0 && global.gamePhase=="fight";
    }

    public GameObject findClosestEnemy(){
        List<GameObject> enemies;
        if(w.attributes.isFriendly){
            enemies=Global.curr.enemies;
        }else{
            enemies=Global.curr.defenders;
        }

        float minDistance = float.MaxValue;
        GameObject closestEnemy=null;
        foreach(GameObject enemy in enemies){
            float currDistance = Vector2.Distance( gameObject.transform.position, enemy.transform.position);
            if(currDistance<minDistance){
                closestEnemy=enemy;
                minDistance=currDistance;
            }
        }
        return closestEnemy;
    }

    public GameObject findHighestHPEnemy(){
        List<GameObject> enemies;
        if(w.attributes.isFriendly){
            enemies=Global.curr.enemies;
        }else{
            enemies=Global.curr.defenders;
        }

        int maxHP = 0;
        GameObject highestHPEnemy=null;
        foreach(GameObject enemy in enemies){
            int currHP = enemy.GetComponent<Warrior>().maxHealth;
            if(currHP > maxHP){
                highestHPEnemy=enemy;
                maxHP=currHP;
            }
        }
        return highestHPEnemy;
    }
}
