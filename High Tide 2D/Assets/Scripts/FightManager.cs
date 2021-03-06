using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField]
    public int health = 100;
    [SerializeField]
    public int damage = 10;
    [SerializeField]
    GameObject city;
    [SerializeField]
    GameObject projectile;
    private GameObject target = null;
    LinkedList<GameObject> targetList;
    public bool inCombat = false;
    public bool isFriendly = true;
    public bool isAlive = true;
    public bool waveEnd = false;
    private bool facingRight = true;
    public bool projectileInAir = false;
    SpriteRenderer r;
    // Start is called before the first frame update
    void Start()
    {
        isFriendly = gameObject.GetComponent<Warrior>().attributes.isFriendly;
        health = gameObject.GetComponent<Warrior>().attributes.hp;
        damage = gameObject.GetComponent<Warrior>().attributes.damage;
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
        
        if (isFriendly)
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
            if (!isFriendly)
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
                    if (!isFriendly)
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
            if (target == null && !isFriendly)
            {
                target = city;
            }
        }
    }

    void moveToTarget()
    {
        if (!inCombat && !waveEnd)
        {
            float shift = GetComponent<SpriteRenderer>().bounds.size.x;
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
            if (isFriendly)
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
            if (!isFriendly)
            {
                bothAlive = opponent.GetComponent<FightManager>().isAlive;
            }
            bool targetExists = opponent.GetComponent<Warrior>() != null;
            bool checkFriendlyFire = opponent.GetComponent<Warrior>().attributes.isFriendly != gameObject.GetComponent<Warrior>().attributes.isFriendly;
            bool notAlreadyInCombat = !inCombat;

            if (targetExists && bothAlive && checkFriendlyFire && notAlreadyInCombat)
            {
                inCombat = true;
                InvokeRepeating("fight", 0f, 0.75f);
            }
        }
        else
        {
            if (opponent.GetComponent<CityHealthManager>() != null)
            {
                opponent.GetComponent<CityHealthManager>().takeDamage();
                if (Global.curr.enemyWaveDeathCount == 1)
                {
                    die();
                    waveComplete();
                }
                else
                {
                    die();
                }
            }
        }
    }

    void fight()
    {
        if(target != null && !waveEnd)
        {
            if(isFriendly && isAlive && isActiveAndEnabled && target.GetComponent<FightManager>().isAlive)
            {
                if (GetComponent<Warrior>().attributes.isRanged)
                {
                    fireProjectile();
                    //doDamage();
                    
                }
                else
                {
                    doDamage();
                }    
            }
            else
            {
                if (target.GetComponent<FightManager>().isAlive && !waveEnd)
                {
                    if (GetComponent<Warrior>().attributes.isRanged)
                    {
                        fireProjectile();
                        //doDamage();
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
                }
            } 
        }
        else
        {
            CancelInvoke();
            inCombat = false;
        }
        
    }

    void fireProjectile()
    {
        if(target != null && isAlive)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<ProjectileMover>().moveProjectile(this.gameObject, target);
        }
    }

    bool waveComplete()
    {
        if (Global.curr.waveStart)
        {
            if (Global.curr.enemyWaveDeathCount == 0 && !waveEnd)
            {
                CancelInvoke();
                resetWave();
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
            victim.health -= damage;

            if (victim.health <= 0)
            {
                victim.inCombat = false;
                inCombat = false;
                if (!isFriendly)
                victim.isAlive = false;
                CancelInvoke();
                victim.die();
            }
        }
        else
        {
            CancelInvoke();
        }
        
    }

    public void die()
    {
        if (!isFriendly)
        {
            Global.curr.enemyWaveDeathCount--;
            inCombat = false;
            deleteEnemy();
            Destroy(gameObject);
        }
        else
        {
            inCombat = false;
            gameObject.SetActive(false);
        }
    }

    void deleteEnemy()
    {
        Global.curr.enemies.Remove(this.gameObject);

    }

    void resetWave()
    {
        Debug.Log("Resetting Wave");
        Global.curr.waveStart = false;
        Global.curr.waveNum++;
        Global.curr.gamePhase = "shop";
        Global.curr.resetShop();
        foreach (GameObject current in Global.curr.defenders)
        {
            current.transform.position = current.GetComponent<Warrior>().coordinates;
            current.SetActive(true);
            current.GetComponent<FightManager>().isAlive = true;

        }
        Global.curr.gold+=10;
    }
}
