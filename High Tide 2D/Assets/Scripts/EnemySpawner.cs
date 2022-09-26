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
        if(Input.GetKeyDown(KeyCode.M)){
            AudioScript.curr.playBattleTheme();
        }
    }

    private IEnumerator spawnEnemy(float delay, GameObject enemyType)
    {
        yield return new WaitForSeconds(delay);
        GameObject newSpawn = Instantiate(enemyType, new Vector2(Random.Range(6f, 6.5f), Random.Range(-4.6f, 4.6f)), Quaternion.identity);
        Warrior w = newSpawn.GetComponent<Warrior>(); 
        w.setWarrior("Pokey boy");
        w.attributes.damage = calcNewDamage(newSpawn.GetComponent<Warrior>().attributes.damage, Global.curr.waveNum);
        w.setHealth( calcNewHealth(w.attributes.hp, Global.curr.waveNum) );

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
                WaveBarController.curr.setTopText("Current wave:");

                AudioScript.curr.stopMainTheme();
                AudioScript.curr.playButtonClickSound();
                AudioScript.curr.playBattleHornSound();
                AudioScript.curr.playBattleTheme();

                if (Global.curr.waveNum == 1)
                {
                    maxEnemies = 2;
                }else if(Global.curr.waveNum == 8){
                    maxEnemies = 10;
                }else{
                    //maxEnemies = (3 * Global.curr.waveNum) + Global.curr.waveNum;
                    maxEnemies = 2+Global.curr.waveNum;
                }

                WaveBarController.curr.setMaxHealth(calcCombinedHP("Pokey boy", maxEnemies, Global.curr.waveNum));
                WaveBarController.curr.setHealth( calcCombinedHP("Pokey boy", maxEnemies, Global.curr.waveNum) );

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
            StartCoroutine(spawnEnemy(1.5f, enemy));
        }
    }

    int calcNewDamage(int dmg, int waveNum){
        float multiplier = calcDamageMultiplier(waveNum);
        float damageF = multiplier * dmg;
        int damage = (int)(Mathf.Round(damageF));
        return damage;
    }

    float calcDamageMultiplier(int waveNum)//return a damage or hp multiplier based on a wave number
    {
        //**** EQUATION ****//
        // m = (a-1)*(2/pi) * atan(r*(x-1)) + 1
        //m: multiplier, a: horizontal asimptote/ceiling, r: rate of increase, x: wave num

        float a=8f; //the horizontal asimptote
        float r=.05f; //the rate of increase

        float n = (a-1) * (2f/Mathf.PI); //the left side of equation : n*atan(r*x)
        float multiplier = (n * Mathf.Atan( r *((float)waveNum - 1f ))) + 1f;
        
        return multiplier;
    }

    int calcNewHealth(int dmg, int waveNum){
        float multiplier = calcHealthMultiplier(waveNum);
        float damageF = multiplier * dmg;
        int damage = (int)(Mathf.Round(damageF));
        return damage;
    }

    float calcHealthMultiplier(int waveNum)//return a damage or hp multiplier based on a wave number
    {
        //**** EQUATION ****//
        // m = (a-1)*(2/pi) * atan(r*(x-1)) + 1
        //m: multiplier, a: horizontal asimptote/ceiling, r: rate of increase, x: wave num

        float a=10f; //the horizontal asimptote
        float r=.05f; //the rate of increase
        Debug.Log("Wave num: "+waveNum);
        Debug.Log("a: "+a);
        Debug.Log("r: "+r);

        float n = (a-1) * (2f/Mathf.PI); //the left side of equation : n*atan(r*x)
        float multiplier = (n * Mathf.Atan( r *((float)waveNum - 1f ))) + 1f;
        
        Debug.Log("Wave num: "+waveNum);
        Debug.Log("a: "+a);
        Debug.Log("r: "+r);
        Debug.Log("Multiplier: "+multiplier);

        return multiplier;
    }

    int calcCombinedHP(string warriorName, int amount, int waveNum){
        int baseHP = WarriorTypes.curr.find(warriorName).hp;
        int warriorHP = calcNewHealth(baseHP, waveNum);
        int combinedHP = warriorHP*amount;

        return combinedHP;
    }
}
