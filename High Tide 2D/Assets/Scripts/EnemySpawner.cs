using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private int spawnCount = 0;
    public int maxEnemies;
    public int spawnComplete;
    int groupCounter = 0;
    float spawnDelay = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemy(float delay, GameObject enemyType)
    {
<<<<<<< Updated upstream
        yield return new WaitForSeconds(delay);
        GameObject newSpawn = Instantiate(enemyType, new Vector2(Random.Range(7f, 8.5f), Random.Range(-4.6f, 4.6f)), Quaternion.identity);
        newSpawn.GetComponent<Warrior>().setWarrior("Lizardman");
=======
        groupCounter++;
        if (groupCounter >= 3)
        {
            spawnDelay = 2f; //Set spawnDelay after each group here
            groupCounter = 0;
        }
        else
        {
            spawnDelay = 0f;
        }
        Debug.Log("Counter: " + groupCounter);
        Debug.Log("Waiting for: " + spawnDelay + " seconds");
        yield return new WaitForSeconds(spawnDelay);
        GameObject newSpawn = Instantiate(enemyType, new Vector2(Random.Range(6f, 6.5f), Random.Range(-4.6f, 4.6f)), Quaternion.identity);
        Warrior w = newSpawn.GetComponent<Warrior>(); 
        w.setWarrior("Pokey boy");
        w.attributes.damage = calcNewDamage(newSpawn.GetComponent<Warrior>().attributes.damage, Global.curr.waveNum);
        w.setHealth( calcNewHealth(w.attributes.hp, Global.curr.waveNum) );

>>>>>>> Stashed changes
        Global.curr.enemies.AddLast(newSpawn);
        spawnCount++;
        if (spawnCount < maxEnemies)
        {
            StartCoroutine(spawnEnemy(delay, enemyType));
        }
    }

    public void startFight()
    {
        if (Global.curr.startButtonEnabled){
            if (!Global.curr.waveStart)
            {
<<<<<<< Updated upstream
=======
                groupCounter = 0;
                WaveBarController.curr.setTopText("Current wave:");

>>>>>>> Stashed changes
                AudioScript.curr.stopMainTheme();
                AudioScript.curr.playButtonClickSound();
                AudioScript.curr.playBattleHornSound();
                AudioScript.curr.playBattleTheme();

                if (Global.curr.waveNum == 1)
                {
                    maxEnemies = 2;
                }
                else
                {
                    maxEnemies = (3 * Global.curr.waveNum) + Global.curr.waveNum;
                }
                spawnCount = 0;
                Global.curr.enemyWaveDeathCount = maxEnemies;
                Global.curr.waveStart = true;
                startCoroutines();
            }
        }
    }

    public void startCoroutines()
    {//so that the coroutines dont get deleted when the buttons that call them get set to inactive
        if (Global.curr.gamePhase != "fight")
        {
            Global.curr.gamePhase = "fight";
            StartCoroutine(spawnEnemy(spawnDelay, enemy));
        }
    }
}
