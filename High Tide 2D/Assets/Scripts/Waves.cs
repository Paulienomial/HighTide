using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public List<WaveStats> waves = new List<WaveStats>();
    public static Waves curr;

    void Awake(){
        curr=this;
    }

    // Start is called before the first frame update
    void Start()
    {
        setWaveStats();
    }

    void setWaveStats(){
        //WAVE 1
        waves.Add(new WaveStats());
        waves[0].bonusGold = 3;

        waves[0].enemyGroups.Add(new EnemyGroup());
        waves[0].enemyGroups[0].name = "Pokey boy";
        waves[0].enemyGroups[0].count = 2;
        waves[0].enemyGroups[0].bundleSize = 2;
        waves[0].enemyGroups[0].freezeTime = .5f;
        waves[0].enemyGroups[0].spawnInterval = 3.5f;
        waves[0].enemyGroups[0].hpMultiplier = 1f;
        waves[0].enemyGroups[0].dmgMultiplier = 1f;

        //WAVE 2
        waves.Add(new WaveStats());
        waves[1].bonusGold = 5;

        waves[1].enemyGroups.Add(new EnemyGroup());
        waves[1].enemyGroups[0].name = "Pokey boy";
        waves[1].enemyGroups[0].count = 4;
        waves[1].enemyGroups[0].bundleSize = 2;
        waves[1].enemyGroups[0].freezeTime = .5f;
        waves[1].enemyGroups[0].spawnInterval = 3.5f;
        waves[1].enemyGroups[0].hpMultiplier = 1f;
        waves[1].enemyGroups[0].dmgMultiplier = .8f;

        //WAVE 3: 4 ranged units
        waves.Add(new WaveStats());
        waves[2].bonusGold = 5;

        waves[2].enemyGroups.Add(new EnemyGroup());
        waves[2].enemyGroups[0].name = "Pirate";
        waves[2].enemyGroups[0].count = 4;
        waves[2].enemyGroups[0].bundleSize = 3;
        waves[2].enemyGroups[0].freezeTime = .5f;
        waves[2].enemyGroups[0].spawnInterval = 3;
        waves[2].enemyGroups[0].hpMultiplier = .85f;
        waves[2].enemyGroups[0].dmgMultiplier = .5f;

        //WAVE 4: melee+ranged
        waves.Add(new WaveStats());
        waves[3].bonusGold = 6;

        waves[3].enemyGroups.Add(new EnemyGroup());
        waves[3].enemyGroups[0].name = "Pokey boy";
        waves[3].enemyGroups[0].count = 3;
        waves[3].enemyGroups[0].bundleSize = 3;
        waves[3].enemyGroups[0].freezeTime = .5f;
        waves[3].enemyGroups[0].spawnInterval = 3;
        waves[3].enemyGroups[0].hpMultiplier = 1f;
        waves[3].enemyGroups[0].dmgMultiplier = 1f;

        waves[3].enemyGroups.Add(new EnemyGroup());
        waves[3].enemyGroups[1].name = "Pirate";
        waves[3].enemyGroups[1].count = 2;
        waves[3].enemyGroups[1].bundleSize = 2;
        waves[3].enemyGroups[1].freezeTime = 3.5f;
        waves[3].enemyGroups[1].spawnInterval = 3;
        waves[3].enemyGroups[1].hpMultiplier = 1f;
        waves[3].enemyGroups[1].dmgMultiplier = .75f;

        //WAVE 5
        waves.Add(new WaveStats());
        waves[4].bonusGold = 6;

        waves[4].enemyGroups.Add(new EnemyGroup());
        waves[4].enemyGroups[0].name = "Jellyfish";
        waves[4].enemyGroups[0].count = 6;
        waves[4].enemyGroups[0].bundleSize = 3;
        waves[4].enemyGroups[0].freezeTime = .5f;
        waves[4].enemyGroups[0].spawnInterval = 3;
        waves[4].enemyGroups[0].hpMultiplier = 1f;
        waves[4].enemyGroups[0].dmgMultiplier = 1f;

        //WAVE 6: Teleporters
        waves.Add(new WaveStats());
        waves[5].bonusGold = 7;

        waves[5].enemyGroups.Add(new EnemyGroup());
        waves[5].enemyGroups[0].name = "Ocean farmer";
        waves[5].enemyGroups[0].count = 7;
        waves[5].enemyGroups[0].bundleSize = 4;
        waves[5].enemyGroups[0].freezeTime = .5f;
        waves[5].enemyGroups[0].spawnInterval = 4;
        waves[5].enemyGroups[0].hpMultiplier = 1f;
        waves[5].enemyGroups[0].dmgMultiplier = 1f;

        //WAVE 7: Teleporters+melee
        waves.Add(new WaveStats());
        waves[6].bonusGold = 7;

        waves[6].enemyGroups.Add(new EnemyGroup());
        waves[6].enemyGroups[0].name = "Tooth ball";
        waves[6].enemyGroups[0].count = 4;
        waves[6].enemyGroups[0].bundleSize = 2;
        waves[6].enemyGroups[0].freezeTime = .5f;
        waves[6].enemyGroups[0].spawnInterval = 4;
        waves[6].enemyGroups[0].hpMultiplier = 1f;
        waves[6].enemyGroups[0].dmgMultiplier = 1f;

        waves[6].enemyGroups.Add(new EnemyGroup());
        waves[6].enemyGroups[1].name = "Ocean farmer";
        waves[6].enemyGroups[1].count = 6;
        waves[6].enemyGroups[1].bundleSize = 3;
        waves[6].enemyGroups[1].freezeTime = 1.5f;
        waves[6].enemyGroups[1].spawnInterval = 3;
        waves[6].enemyGroups[1].hpMultiplier = 1f;
        waves[6].enemyGroups[1].dmgMultiplier = 1f;

        //WAVE 8: Nesting units
        waves.Add(new WaveStats());
        waves[7].count = 26;//since this wave splits up enemies, the total count is set manually
        waves[7].bonusGold = 7;

        waves[7].enemyGroups.Add(new EnemyGroup());
        waves[7].enemyGroups[0].name = "Large Jeffrey";
        waves[7].enemyGroups[0].count = 2;
        waves[7].enemyGroups[0].bundleSize = 2;
        waves[7].enemyGroups[0].freezeTime = .5f;
        waves[7].enemyGroups[0].spawnInterval = 3;
        waves[7].enemyGroups[0].hpMultiplier = 1f;
        waves[7].enemyGroups[0].dmgMultiplier = 1f;

        //WAVE 9: Nesting + ranged units
        waves.Add(new WaveStats());
        waves[8].count = 26+4;//since this wave splits up enemies, the total count is set manually
        waves[8].bonusGold = 7;

        waves[8].enemyGroups.Add(new EnemyGroup());
        waves[8].enemyGroups[0].name = "Large Jeffrey";
        waves[8].enemyGroups[0].count = 2;
        waves[8].enemyGroups[0].bundleSize = 2;
        waves[8].enemyGroups[0].freezeTime = .5f;
        waves[8].enemyGroups[0].spawnInterval = 3;
        waves[8].enemyGroups[0].hpMultiplier = 1f;
        waves[8].enemyGroups[0].dmgMultiplier = 1f;

        waves[8].enemyGroups.Add(new EnemyGroup());
        waves[8].enemyGroups[1].name = "Pirate";
        waves[8].enemyGroups[1].count = 4;
        waves[8].enemyGroups[1].bundleSize = 4;
        waves[8].enemyGroups[1].freezeTime = 4.5f;
        waves[8].enemyGroups[1].spawnInterval = 3;
        waves[8].enemyGroups[1].hpMultiplier = 1f;
        waves[8].enemyGroups[1].dmgMultiplier = 1f;

        //WAVE 10: Lightning boss + octupus + ranged
        waves.Add(new WaveStats());
        waves[9].bonusGold = 7;

        waves[9].enemyGroups.Add(new EnemyGroup());
        waves[9].enemyGroups[0].name = "Dark wizard";
        waves[9].enemyGroups[0].count = 1;
        waves[9].enemyGroups[0].bundleSize = 1;
        waves[9].enemyGroups[0].freezeTime = .5f;
        waves[9].enemyGroups[0].spawnInterval = 3;
        waves[9].enemyGroups[0].hpMultiplier = 1f;
        waves[9].enemyGroups[0].dmgMultiplier = 1f;

        waves[9].enemyGroups.Add(new EnemyGroup());
        waves[9].enemyGroups[1].name = "Octupus";
        waves[9].enemyGroups[1].count = 2;
        waves[9].enemyGroups[1].bundleSize = 2;
        waves[9].enemyGroups[1].freezeTime = .5f;
        waves[9].enemyGroups[1].spawnInterval = 3;
        waves[9].enemyGroups[1].hpMultiplier = 1f;
        waves[9].enemyGroups[1].dmgMultiplier = 1f;

        waves[9].enemyGroups.Add(new EnemyGroup());
        waves[9].enemyGroups[2].name = "Pirate";
        waves[9].enemyGroups[2].count = 2;
        waves[9].enemyGroups[2].bundleSize = 4;
        waves[9].enemyGroups[2].freezeTime = 3.5f;
        waves[9].enemyGroups[2].spawnInterval = 3;
        waves[9].enemyGroups[2].hpMultiplier = 1f;
        waves[9].enemyGroups[2].dmgMultiplier = 1f;

        //WAVE 11
        waves.Add(new WaveStats());
        waves[10].bonusGold = 7;

        waves[10].enemyGroups.Add(new EnemyGroup());
        waves[10].enemyGroups[0].name = "Pokey boy";
        waves[10].enemyGroups[0].count = 10;
        waves[10].enemyGroups[0].bundleSize = 5;
        waves[10].enemyGroups[0].freezeTime = .5f;
        waves[10].enemyGroups[0].spawnInterval = 5f;
        waves[10].enemyGroups[0].hpMultiplier = 3f;
        waves[10].enemyGroups[0].dmgMultiplier = 3f;

        //WAVE 12
        waves.Add(new WaveStats());
        waves[11].bonusGold = 7;

        waves[11].enemyGroups.Add(new EnemyGroup());
        waves[11].enemyGroups[0].name = "Pokey boy";
        waves[11].enemyGroups[0].count = 20;
        waves[11].enemyGroups[0].bundleSize = 7;
        waves[11].enemyGroups[0].freezeTime = .5f;
        waves[11].enemyGroups[0].spawnInterval = 5f;
        waves[11].enemyGroups[0].hpMultiplier = 3f;
        waves[11].enemyGroups[0].dmgMultiplier = 3f;

        //WAVE 13: ranged units
        waves.Add(new WaveStats());
        waves[12].bonusGold = 7;

        waves[12].enemyGroups.Add(new EnemyGroup());
        waves[12].enemyGroups[0].name = "Pirate";
        waves[12].enemyGroups[0].count = 14;
        waves[12].enemyGroups[0].bundleSize = 7;
        waves[12].enemyGroups[0].freezeTime = .5f;
        waves[12].enemyGroups[0].spawnInterval = 5;
        waves[12].enemyGroups[0].hpMultiplier = 4f;
        waves[12].enemyGroups[0].dmgMultiplier = 4f;

        //WAVE 14: melee+ranged units
        waves.Add(new WaveStats());
        waves[13].bonusGold = 7;

        waves[13].enemyGroups.Add(new EnemyGroup());
        waves[13].enemyGroups[0].name = "Pokey boy";
        waves[13].enemyGroups[0].count = 8;
        waves[13].enemyGroups[0].bundleSize = 4;
        waves[13].enemyGroups[0].freezeTime = .5f;
        waves[13].enemyGroups[0].spawnInterval = 3;
        waves[13].enemyGroups[0].hpMultiplier = 4f;
        waves[13].enemyGroups[0].dmgMultiplier = 4.5f;

        waves[13].enemyGroups.Add(new EnemyGroup());
        waves[13].enemyGroups[1].name = "Pirate";
        waves[13].enemyGroups[1].count = 10;
        waves[13].enemyGroups[1].bundleSize = 5;
        waves[13].enemyGroups[1].freezeTime = 3.5f;
        waves[13].enemyGroups[1].spawnInterval = 3;
        waves[13].enemyGroups[1].hpMultiplier = 4f;
        waves[13].enemyGroups[1].dmgMultiplier = 4f;

        //WAVE 15: Jellyfish
        waves.Add(new WaveStats());
        waves[14].bonusGold = 7;

        waves[14].enemyGroups.Add(new EnemyGroup());
        waves[14].enemyGroups[0].name = "Jellyfish";
        waves[14].enemyGroups[0].count = 20;
        waves[14].enemyGroups[0].bundleSize = 7;
        waves[14].enemyGroups[0].freezeTime = .5f;
        waves[14].enemyGroups[0].spawnInterval = 4;
        waves[14].enemyGroups[0].hpMultiplier = 7f;
        waves[14].enemyGroups[0].dmgMultiplier = 4f;

        //WAVE 16: Teleporters
        waves.Add(new WaveStats());
        waves[15].bonusGold = 7;

        waves[15].enemyGroups.Add(new EnemyGroup());
        waves[15].enemyGroups[0].name = "Ocean farmer";
        waves[15].enemyGroups[0].count = 16;
        waves[15].enemyGroups[0].bundleSize = 8;
        waves[15].enemyGroups[0].freezeTime = .5f;
        waves[15].enemyGroups[0].spawnInterval = 4.5f;
        waves[15].enemyGroups[0].hpMultiplier = 4f;
        waves[15].enemyGroups[0].dmgMultiplier = 4f;

        //WAVE 17: Teleporters+melee
        waves.Add(new WaveStats());
        waves[16].bonusGold = 7;

        waves[16].enemyGroups.Add(new EnemyGroup());
        waves[16].enemyGroups[0].name = "Tooth ball";
        waves[16].enemyGroups[0].count = 10;
        waves[16].enemyGroups[0].bundleSize = 5;
        waves[16].enemyGroups[0].freezeTime = .5f;
        waves[16].enemyGroups[0].spawnInterval = 4;
        waves[16].enemyGroups[0].hpMultiplier = 3f;
        waves[16].enemyGroups[0].dmgMultiplier = 4f;

        waves[16].enemyGroups.Add(new EnemyGroup());
        waves[16].enemyGroups[1].name = "Ocean farmer";
        waves[16].enemyGroups[1].count = 8;
        waves[16].enemyGroups[1].bundleSize = 4;
        waves[16].enemyGroups[1].freezeTime = 1.5f;
        waves[16].enemyGroups[1].spawnInterval = 4;
        waves[16].enemyGroups[1].hpMultiplier = 4f;
        waves[16].enemyGroups[1].dmgMultiplier = 4f;

        //WAVE 18: Nesting units
        waves.Add(new WaveStats());
        waves[17].count = 39;//since this wave splits up enemies, the total count is set manually
        waves[17].bonusGold = 7;

        waves[17].enemyGroups.Add(new EnemyGroup());
        waves[17].enemyGroups[0].name = "Large Jeffrey";
        waves[17].enemyGroups[0].count = 3;
        waves[17].enemyGroups[0].bundleSize = 3;
        waves[17].enemyGroups[0].freezeTime = .5f;
        waves[17].enemyGroups[0].spawnInterval = 3;
        waves[17].enemyGroups[0].hpMultiplier = 4f;
        waves[17].enemyGroups[0].dmgMultiplier = 7f;

        //WAVE 19: Nesting + ranged units
        waves.Add(new WaveStats());
        waves[18].count = 39+10;//since this wave splits up enemies, the total count is set manually
        waves[18].bonusGold = 7;

        waves[18].enemyGroups.Add(new EnemyGroup());
        waves[18].enemyGroups[0].name = "Large Jeffrey";
        waves[18].enemyGroups[0].count = 3;
        waves[18].enemyGroups[0].bundleSize = 3;
        waves[18].enemyGroups[0].freezeTime = .5f;
        waves[18].enemyGroups[0].spawnInterval = 3;
        waves[18].enemyGroups[0].hpMultiplier = 4f;
        waves[18].enemyGroups[0].dmgMultiplier = 7f;

        waves[18].enemyGroups.Add(new EnemyGroup());
        waves[18].enemyGroups[1].name = "Pirate";
        waves[18].enemyGroups[1].count = 10;
        waves[18].enemyGroups[1].bundleSize = 5;
        waves[18].enemyGroups[1].freezeTime = 4.5f;
        waves[18].enemyGroups[1].spawnInterval = 3;
        waves[18].enemyGroups[1].hpMultiplier = 4f;
        waves[18].enemyGroups[1].dmgMultiplier = 6f;

        //WAVE 20: Final boss + 2 black dragons
        waves.Add(new WaveStats());
        waves[19].bonusGold = 7;

        waves[19].enemyGroups.Add(new EnemyGroup());
        waves[19].enemyGroups[0].name = "Da big boss";
        waves[19].enemyGroups[0].count = 1;
        waves[19].enemyGroups[0].bundleSize = 1;
        waves[19].enemyGroups[0].freezeTime = 0f;
        waves[19].enemyGroups[0].spawnInterval = 3;
        waves[19].enemyGroups[0].hpMultiplier = 1f;
        waves[19].enemyGroups[0].dmgMultiplier = 1f;

        waves[19].enemyGroups.Add(new EnemyGroup());
        waves[19].enemyGroups[1].name = "Black dragon";
        waves[19].enemyGroups[1].count = 2;
        waves[19].enemyGroups[1].bundleSize = 2;
        waves[19].enemyGroups[1].freezeTime = 6.5f;
        waves[19].enemyGroups[1].spawnInterval = 3;
        waves[19].enemyGroups[1].hpMultiplier = 1f;
        waves[19].enemyGroups[1].dmgMultiplier = 1f;


        Events.curr.setWaves();
    }
}
