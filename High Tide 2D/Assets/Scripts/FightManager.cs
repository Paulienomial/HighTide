using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    //[SerializeField]
    //public int health = 100;
    //[SerializeField]
    //public int damage = 10;
    [SerializeField]
    GameObject city;
    [SerializeField]
    GameObject projectile;
    private GameObject target = null;
    LinkedList<GameObject> targetList;
    public bool inCombat = false;
    //public bool isFriendly = true;
    public bool isAlive = true;
    public bool waveEnd = false;
    private bool facingRight = true;
    public bool projectileInAir = false;
    public bool waveLost = false;
    //Fix met attributes wat nie update nie
    public WarriorAttributes.attr a;

    SpriteRenderer r;
    // Start is called before the first frame update
    void Start()
    {
        //
        a = gameObject.GetComponent<Warrior>().attributes;

        //hierdie werk nie, want dit word net by start gecall en dan update die vals nie
        /*isFriendly = gameObject.GetComponent<Warrior>().attributes.isFriendly;
        health = gameObject.GetComponent<Warrior>().attributes.hp;
        damage = gameObject.GetComponent<Warrior>().attributes.damage;*/
        r = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        waveEnd = waveComplete();
        if (!inCombat && Global.curr.waveStart && !waveEnd)
        {
            findTarget();
            if(target != null)
            {
                moveToTarget();
            }   
        }
    }

    void findTarget()
    {
        
        if (a.isFriendly)
        {
            targetList = Global.curr.enemies;
        }
        else
        {
            targetList = Global.curr.defenders;
        }

        if (targetList.Count == 0)
        {
           // Debug.Log("No Targets");
            if (!a.isFriendly)
            {
                target = city;
            }
        }
        else
        {
            float minDist = 999999;
            GameObject bestTarget = null;
            bool flag = true;
            foreach (GameObject current in targetList)
            {
                if (current != null)
                {
                    if (!a.isFriendly)
                    {
                        flag = current.GetComponent<FightManager>().isAlive;
                    }
                    if (flag)
                    {
                        float currDist = Vector2.Distance(current.transform.position, this.transform.position);
                        if (currDist < minDist)
                        {
                            minDist = currDist;
                            bestTarget = current;

                        }
                    }
                    else
                    {
                        //Debug.Log("Ignoring dead target");
                    }
                }

            }
            target = bestTarget;
            if (target == null && !a.isFriendly)
            {
                target = city;
            }
        }
    }

    void moveToTarget()
    {
        if (!inCombat && !waveEnd)
        {
            gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 1);
            float shift = GetComponent<Collider2D>().bounds.size.x;
            if (GetComponent<Warrior>().attributes.isRanged)
            {
                if (Vector2.Distance(transform.position, target.transform.position) <= 3)
                {
                    //Debug.Log("Archer engaging");
                    engageCombat(target);
                }
            }

            if (transform.position.x - target.transform.position.x < 0)
            {
                if (!facingRight)
                {
                    facingRight = true;
                    r.flipX = false;
                }
            }
            else
            {
                if (facingRight)
                {
                    facingRight = false;
                    r.flipX = true;
                }
            }
            if (a.isFriendly)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x - shift, target.transform.position.y), 0.5f * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x + shift, target.transform.position.y), 0.5f * Time.deltaTime);
            }
        }
    }

    void OnTriggerStay2D(Collider2D target)
    {
        if (!GetComponent<Warrior>().attributes.isRanged)
        {
            engageCombat(target.gameObject);
        } 
    }

    public void engageCombat(GameObject opponent)
    {
        if (opponent.GetComponent<Warrior>() != null && !waveEnd) //Checking that target is a warrior and not the grid since grid also has a Rigidbody and thus triggers this
        {
            bool bothAlive = true;
            if (!a.isFriendly)
            {
                bothAlive = opponent.GetComponent<FightManager>().isAlive;
            }
            bool targetExists = opponent.GetComponent<Warrior>() != null;
            bool checkFriendlyFire = opponent.GetComponent<Warrior>().attributes.isFriendly != gameObject.GetComponent<Warrior>().attributes.isFriendly;
            bool notAlreadyInCombat = !inCombat;

            if (targetExists && bothAlive && checkFriendlyFire && notAlreadyInCombat)
            {
                inCombat = true;
                gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 2);
                InvokeRepeating("fight", 0.3f, 0.75f);
            }
        }
        else
        {
            if (opponent.GetComponent<CityHealthManager>() != null && !GetComponent<Warrior>().attributes.isFriendly)
            {
                waveLost = true;
                opponent.GetComponent<CityHealthManager>().takeDamage();
                if (Global.curr.enemyWaveDeathCount == 1)//if last enemy to damage city
                {
                    die();
                    CityUpgrade.curr.playDamageAnimation(a.cityDamage);
                    waveComplete();
                }
                else //if not last enemy to damage city
                {
                    die();
                    CityUpgrade.curr.playDamageAnimation(a.cityDamage);
                }
            }
        }
    }

    void fight()
    {
        if (target != null && !waveEnd)
        {
            if(a.isFriendly && isAlive && isActiveAndEnabled && target.GetComponent<FightManager>().isAlive)
            {
                if (!gameObject.GetComponent<Warrior>().attributes.isRanged)
                {
                    AudioScript.curr.playAttackSound(this.gameObject);
                }
                if (GetComponent<Warrior>().attributes.isRanged)
                {
                    fireProjectile();
                    
                }
                else
                {
                    doDamage();
                }    
            }
            else//enemy
            {
                if (target.GetComponent<FightManager>().isAlive && !waveEnd)
                {
                    if (!gameObject.GetComponent<Warrior>().attributes.isRanged)
                    {
                        AudioScript.curr.playAttackSound(this.gameObject);
                    }
                    if (GetComponent<Warrior>().attributes.isRanged)
                    {
                        fireProjectile();
                    }
                    else
                    {
                        doDamage();
                    }
                }
                else
                {
                    CancelInvoke();
                    inCombat = false;
                    gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
                }
            } 
        }
        else
        {
            CancelInvoke();
            inCombat = false;
            gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
        }
        
    }

    void fireProjectile()
    {
        if(target != null && isAlive)
        {
            AudioScript.curr.playAttackSound(this.gameObject);
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<ProjectileMover>().moveProjectile(this.gameObject, target);
        }
    }

    bool waveComplete()
    {
        if (Global.curr.waveStart)
        {
            //Debug.Log(Global.curr.enemyWaveDeathCount);
            if (Global.curr.enemyWaveDeathCount == 0 && !waveEnd)
            {
                CancelInvoke();
                gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
                resetWave();
                Debug.Log("wave complete");
                WaveBarController.curr.setHealth(WaveBarController.curr.getMaxHealth());
                return true;
            }
            else
            {
                return false;
            }
        }
        else 
        {
            return false;
        }
        
    }

    public void doDamage()
    {
        if (isAlive && !waveEnd && target != null && target.GetComponent<FightManager>().isAlive)
        {
            FightManager victim = target.GetComponent<FightManager>();
            int damageDealt = a.damage;
            if( (victim.a.hp-a.damage)<0 ){//if the damage will cause victim's health to fall below zero
                damageDealt=victim.a.hp;
                victim.a.hp=0;
            }else{
                victim.a.hp -= a.damage;
            }
            if(victim.a.isFriendly==false){
                WaveBarController.curr.setHealth(WaveBarController.curr.getHealth()-damageDealt);
            }
            //target.GetComponent<Warrior>().attributes.hp -= a.damage;

            if (victim.a.hp <= 0)
            {
                victim.inCombat = false;
                inCombat = false;
                //if (!a.isFriendly)
                victim.isAlive = false;
                CancelInvoke();
                gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
                victim.die();
            }
        }
        else
        {
            CancelInvoke();
            gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
        }
        
    }

    public void die()
    {
        if (!a.isFriendly)
        {
            Global.curr.enemyWaveDeathCount--;
            Debug.Log(Global.curr.enemyWaveDeathCount);
            inCombat = false;
            deleteEnemy();
            if(!waveLost){
                playGoldAnimation(a.bounty);
            }
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        else
        {
            inCombat = false;
            gameObject.GetComponent<Warrior>().diedLastWave=true;
            gameObject.SetActive(false);
        }
    }

    void deleteEnemy()
    {
        Global.curr.enemies.Remove(this.gameObject);

    }

    void resetWave()
    {
        if(!Global.curr.gameOver){
            //Debug.Log("Resetting Wave");
            AudioScript.curr.stopBattleTheme();
            if (waveLost)
            {
                AudioScript.curr.playWaveFailedAndMain();
            }
            else
            {
                AudioScript.curr.playVictoryAndMain();
            }
            waveLost = false;
            Global.curr.waveStart = false;
            Global.curr.waveNum++;
            WaveBarController.curr.setText("Wave "+Global.curr.waveNum);
            WaveBarController.curr.setTopText("Next wave:");
            Global.curr.gamePhase = "shop";
            Global.curr.resetShop();
            foreach (GameObject current in Global.curr.defenders)
            {
                current.transform.position = current.GetComponent<Warrior>().coordinates;
                current.SetActive(true);
                current.GetComponent<FightManager>().inCombat = false;
                current.GetComponent<FightManager>().isAlive = true;
                current.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
               //current.GetComponent<HealthBarUpdate>().hpBar.setHealth(current.GetComponent<Warrior>().maxHealth);
                current.GetComponent<FightManager>().a.hp = current.GetComponent<Warrior>().maxHealth;
                current.GetComponent<Warrior>().attributes.hp = current.GetComponent<Warrior>().maxHealth;

            }
            //Global.curr.gold+=10;
            Events.curr.waveComplete();//trigger wave complete event
        }
    }

    void playGoldAnimation(int b){
        /*GameObject goldAnim = Instantiate(goldAnimation, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f), Quaternion.identity);
        goldAnim.GetComponentInChildren<GoldAnimation>().play(b);*/
        AnimationController.curr.play("goldDrop", new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f), "+"+a.bounty.ToString(), "coinFlip", 3);
        Global.curr.gold+=a.bounty;
    }
}
