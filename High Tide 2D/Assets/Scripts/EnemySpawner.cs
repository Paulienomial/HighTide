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
    public string[] enemies = {"Pokey boy","Jellyfish","Tooth ball","Octupus","Dark wizard"};
    public GameObject pauseMenu;

    public List<WaveStats> waves;
    Global global;
    
    // Start is called before the first frame update
    void Start()
    {
        global = Global.curr;
        waves = new List<WaveStats>();
        waves = Waves.curr.waves;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.M)){
            AudioScript.curr.playBattleTheme();
        }*/
    }

    

    private IEnumerator spawnEnemy(float delay, GameObject enemyType)
    {
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
        yield return new WaitForSeconds(spawnDelay);
        GameObject newSpawn = Instantiate(enemyType, new Vector2(Random.Range(6f, 6.5f), Random.Range(-4.6f, 4.6f)), Quaternion.identity);
        Warrior w = newSpawn.GetComponent<Warrior>(); 
        w.setWarrior(enemies[ (Global.curr.waveNum-1)%5 ]);
        w.attributes.damage = calcNewDamage(newSpawn.GetComponent<Warrior>().attributes.damage, Global.curr.waveNum);
        w.setHealth( calcNewHealth(w.attributes.hp, Global.curr.waveNum) );

        Global.curr.enemies.Add(newSpawn);
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
                groupCounter = 0;
                WaveBarController.curr.setTopText("Current wave:");

                AudioScript.curr.stopMainTheme();
                AudioScript.curr.playButtonClickSound();
                AudioScript.curr.playBattleHornSound();
                AudioScript.curr.playBattleTheme();

                if (Global.curr.waveNum == 1)
                {
                    maxEnemies = 1;
                }else if(Global.curr.waveNum == 18){
                    maxEnemies = 20;
                }else{
                    //maxEnemies = (3 * Global.curr.waveNum) + Global.curr.waveNum;
                    maxEnemies = 1+Global.curr.waveNum;
                }

                WaveBarController.curr.setMaxHealth(calcCombinedHP("Pokey boy", maxEnemies, Global.curr.waveNum));
                WaveBarController.curr.setHealth( calcCombinedHP("Pokey boy", maxEnemies, Global.curr.waveNum) );

                spawnCount = 0;
                Global.curr.enemyWaveDeathCount = maxEnemies;
                Global.curr.waveStart = true;
                startCoroutines();


                Events.curr.waveStart();
            }
        }
    }

    public void startFight2(){
        if(Global.curr.waveNum>Waves.curr.waves.Count) return;
        if (Global.curr.startButtonEnabled){
            if (!Global.curr.waveStart)
            {
                WaveBarController.curr.setTopText("Current wave:");

                AudioScript.curr.stopMainTheme();
                AudioScript.curr.playButtonClickSound();
                AudioScript.curr.playBattleHornSound();
                AudioScript.curr.playBattleTheme();

                WaveBarController.curr.setMaxHealth( waves[global.waveNum-1].totalHP() );
                WaveBarController.curr.setHealth( waves[global.waveNum-1].totalHP() );

                spawnCount = 0;
                Global.curr.enemyWaveDeathCount = waves[global.waveNum-1].totalEnemies();
                Global.curr.waveStart = true;

                if(Global.curr.gamePhase!="fight"){
                    Global.curr.gamePhase = "fight";
                    Global.curr.shopButton.SetActive(false);
                    Global.curr.playButton.SetActive(false);
                    ShopSystem.curr.shop.SetActive(false);
                    ShopSystem.curr.shopOpen=false;
                }
                
                Events.curr.waveStart();
                foreach(EnemyGroup eg in waves[global.waveNum-1].enemyGroups){
                    StartCoroutine(spawnWaves(eg));
                }
            }
        }
    }

    IEnumerator spawnWaves(EnemyGroup eg){
        //Wait for freeze time before starting to spawn
        yield return new WaitForSeconds(eg.freezeTime);
        int totalSpawns=0;
        bool done=false;
        while(!done){
            //spawn bundleSize amount of units at a time
            for(int i=0; i<eg.bundleSize; i++){
                if(totalSpawns==eg.count){
                    done=true;
                }else{
                    GameObject enemy = Instantiate(global.warriorPrefab, new Vector2(Random.Range(6f, 7.5f), Random.Range(-4.6f, 4.6f)), Quaternion.identity );
                    Warrior enemyW = enemy.GetComponent<Warrior>();
                    enemyW.setWarrior(eg.name);
                    enemyW.setHealth( eg.getUnitHP() );
                    enemyW.attributes.damage = eg.getUnitDMG();
                    Global.curr.enemies.Add(enemy);
                    totalSpawns++;

                    //Events.curr.spawnEnemy(enemy);
                }
            }
            //wait spawn interval amount of seconds
            if(!done){
                yield return new WaitForSeconds(eg.spawnInterval);
            }
        }
    }

    public void startCoroutines()
    {//so that the coroutines dont get deleted when the buttons that call them get set to inactive
        if (Global.curr.gamePhase != "fight")
        {
            Global.curr.gamePhase = "fight";
            Global.curr.shopButton.SetActive(false);
            Global.curr.playButton.SetActive(false);
            ShopSystem.curr.shop.SetActive(false);
            ShopSystem.curr.shopOpen=false;

            
            StartCoroutine(spawnEnemy(spawnDelay, enemy));
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
        if(waveNum<=5){
            return .75f;
        }
        //**** EQUATION ****//
        // m = (a-1)*(2/pi) * atan(r*(x-1)) + 1
        //m: multiplier, a: horizontal asimptote/ceiling, r: rate of increase, x: wave num

        float a=10f; //the horizontal asimptote
        float r=.1f; //the rate of increase

        float n = (a-1) * (2f/Mathf.PI); //the left side of equation : n*atan(r*x)
        float multiplier = (n * Mathf.Atan( r *((float)(waveNum-3) - 1f ))) + 1f;
        
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
        if(waveNum<=5){
            return .75f;
        }
        //**** EQUATION ****//
        // m = (a-1)*(2/pi) * atan(r*(x-1)) + 1
        //m: multiplier, a: horizontal asimptote/ceiling, r: rate of increase, x: wave num

        float a=10f; //the horizontal asimptote
        float r=.1f; //the rate of increase

        float n = (a-1) * (2f/Mathf.PI); //the left side of equation : n*atan(r*x)
        float multiplier = (n * Mathf.Atan( r *((float)(waveNum-3) - 1f ))) + 1f;

        return multiplier;
    }

    int calcCombinedHP(string warriorName, int amount, int waveNum){
        int baseHP = WarriorTypes.curr.find(warriorName).hp;
        int warriorHP = calcNewHealth(baseHP, waveNum);
        int combinedHP = warriorHP*amount;

        return combinedHP;
    }
}
