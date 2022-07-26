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
        yield return new WaitForSeconds(delay);
        GameObject newSpawn = Instantiate(enemyType, new Vector2(Random.Range(7f, 8.5f), Random.Range(-4.6f, 4.6f)), Quaternion.identity);
        newSpawn.GetComponent<Warrior>().setWarrior("Lizardman");
        Global.curr.enemies.AddLast(newSpawn);
        spawnCount++;
        if (spawnCount < maxEnemies)
        {
            StartCoroutine(spawnEnemy(delay, enemyType));
        }
    }

    public void startFight()
    {
        if (!Global.curr.waveStart)
        {
            if(Global.curr.waveNum == 1)
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

    public void startCoroutines()
    {//so that the coroutines dont get deleted when the buttons that call them get set to inactive
        if (Global.curr.gamePhase != "fight")
        {
            Global.curr.gamePhase = "fight";
            StartCoroutine(spawnEnemy(1.5f, enemy));
        }
    }
}
