using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStats
{
    public int count=0;
    public int bonusGold=2;
    public List<EnemyGroup> enemyGroups;
    void a(){
        enemyGroups[0] = new EnemyGroup();
    }
    public WaveStats(){
        bonusGold=2;
        enemyGroups = new List<EnemyGroup>();
    }

    public int totalEnemies(){
        if(count==0){
            int total=0;
            foreach(EnemyGroup e in enemyGroups){
                total+=e.count;
            }
            return total;
        }else{
            return count;
        }
    }

    public int totalHP(){
        int total = 0;
        foreach(EnemyGroup eg in enemyGroups){
            if(eg.name!="Large Jeffrey"){
                total += eg.count*eg.getUnitHP();
            }else{
                total += eg.count*eg.getUnitHP();
                total += (eg.count*3)*WarriorTypes.curr.find("Medium Jeffrey").hp;
                total += (eg.count*3*3)*WarriorTypes.curr.find("Tiny Jeffrey").hp;
            }
        }
        return total;
    }
}
