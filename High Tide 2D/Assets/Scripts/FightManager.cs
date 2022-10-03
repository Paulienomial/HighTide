using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField]
    GameObject city;
    [SerializeField]
    GameObject projectile;
    private GameObject target = null;
    LinkedList<GameObject> targetList;
    public bool inCombat = false;
    public bool isAlive = true;
    public bool waveEnd = false;
    private bool facingRight = true;
    public bool projectileInAir = false;
    public bool waveLost = false;
    public bool unitPaused = false;
    public WarriorAttributes.attr a;
    float fastforwardMovespeed;

    SpriteRenderer r;
    void Start()
    {
        a = gameObject.GetComponent<Warrior>().attributes;
        r = GetComponent<SpriteRenderer>();
        fastforwardMovespeed = a.moveSpeed*3f;
    }

    // Update is called once per frame
    void Update()
    {
        waveEnd = waveManager.curr.waveComplete();
        if (!inCombat && Global.curr.waveStart && !waveManager.curr.waveEnd)
        {
            findTarget();
            if (!unitPaused)
            {
                moveToTarget();
            }
        }
    }

    void findTarget()
    {

        setTargetList();

        if (targetList.Count == 0)
        {
            //There are no potential targets for this unit

            if (!a.isFriendly)
            {
                //If unit is enemy and there are no friendy units on the battlefield, unit targets the city.
                target = city;
                a.moveSpeed = fastforwardMovespeed;
            }
        }
        else
        {
            //Target list has been assigned and is not empty so proceed with finding closest target.
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
                        //ignoring dead target
                    }
                }

            }
            target = bestTarget;
            if (target == null && !a.isFriendly)
            {
                target = city;
                a.moveSpeed = fastforwardMovespeed;
            }
        }
    }

    void moveToTarget()
    {
        if (target != null)
        {
            if (!inCombat && !waveEnd)
            {
                gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 1); //Playing walk animation
                float shiftX = GetComponent<Collider2D>().bounds.size.x; //Calculating how far a unit should be from it's target's centre in order to stop right in front of the target.
                float shiftY = Random.Range(GetComponent<Collider2D>().bounds.size.y / .7f * -1, GetComponent<Collider2D>().bounds.size.y / .7f); //Calculating shift so units won't always stand on top of each other when fighting same enemy
                if (GetComponent<Warrior>().attributes.isRanged)
                {
                    //If the current unit is ranged, this IF makes it stop when the target is in range of it's attacks.
                    if (Vector2.Distance(transform.position, target.transform.position) <= 3)
                    {
                        //archer engaging
                        engageCombat(target);
                    }
                }

                pointTowardsTarget();

                if (a.isFriendly)
                {
                    //Ensures that friendly unit will always be on the left and enemy unit on the right.
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x - shiftX, target.transform.position.y + shiftY), a.moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x + shiftX, target.transform.position.y + shiftY), a.moveSpeed * Time.deltaTime);
                }
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
                StatScreens.curr.lostLives++;
                if (Global.curr.enemyWaveDeathCount == 1)//if last enemy to damage city
                {
                    die();
                    CityUpgrade.curr.playDamageAnimation(a.cityDamage);
                    waveManager.curr.waveComplete();
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
        if (target != null && !waveEnd && !unitPaused)
        {
            if (a.isFriendly && isAlive && isActiveAndEnabled && target.GetComponent<FightManager>().isAlive)
            {
                if (!gameObject.GetComponent<Warrior>().attributes.isRanged)
                {
                    //AudioScript.curr.playAttackSound(this.gameObject);
                    playAttackSound();
                }
                if (GetComponent<Warrior>().attributes.isRanged)
                {
                    //Friendly fire projectile
                    fireProjectile();

                }
                else
                {
                    doDamage();
                }
            }
            else//enemy
            {
                if (target.GetComponent<FightManager>().isAlive && !waveEnd && !unitPaused && target.GetComponent<CityHealthManager>() == null)
                {
                    if (!gameObject.GetComponent<Warrior>().attributes.isRanged)
                    {
                        //AudioScript.curr.playAttackSound(this.gameObject);
                        playAttackSound();
                    }
                    if (GetComponent<Warrior>().attributes.isRanged)
                    {
                        //Enemy fires projectile
                        fireProjectile();
                    }
                    else
                    {
                        doDamage();
                        //Enemy does melee damage
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
        if (target != null && isAlive && !unitPaused)
        {
            //AudioScript.curr.playAttackSound(this.gameObject);
            playAttackSound();
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<ProjectileMover>().moveProjectile(this.gameObject, target);
        }
    }

    public void doDamage()
    {
        if (isAlive && !waveEnd && target != null && target.GetComponent<FightManager>().isAlive && !unitPaused)
        {
            FightManager victim = target.GetComponent<FightManager>();
            int bonusDmg = 0;
            if(a.isFriendly) bonusDmg = GlobalBehaviours.curr.globalDMGAura;
            int damageDealt = a.damage + bonusDmg;
            if ((victim.a.hp - damageDealt) < 0)
            {//if the damage will cause victim's health to fall below zero
                damageDealt = victim.a.hp;
                victim.a.hp = 0;
            }
            else
            {
                victim.a.hp -= damageDealt;
            }
            if (victim.a.isFriendly == false)
            {
                WaveBarController.curr.setHealth(WaveBarController.curr.getHealth() - damageDealt);
            }

            if (victim.a.hp <= 0)
            {
                victim.inCombat = false;
                inCombat = false;
                //if (!a.isFriendly)
                victim.isAlive = false;
                CancelInvoke();
                gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
                if (victim.GetComponent<Warrior>().attributes.isFriendly)
                {
                    StatScreens.curr.fallenBrothers++;
                }
                else
                {
                    StatScreens.curr.enemiesKilled++;
                }
                victim.die();
            }
        }
        else
        {
            CancelInvoke();
            gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
        }

    }

    public void takeDamage(int dmg)
    {
        int damageDealt = dmg;
        if ((a.hp - dmg) < 0)
        {//if the damage will cause victim's health to fall below zero
            damageDealt = a.hp;
            a.hp = 0;
            inCombat = false;
            isAlive = false;
            CancelInvoke();
            die();
        }
        else
        {
            //a.hp -= a.damage;
            a.hp -= dmg;
        }
    }

    public void pauseUnit()
    {
        unitPaused = true;
        gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
        inCombat = false;
    }

    public void unpauseUnit()
    {
        unitPaused = false;
        gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 1);
    }

    public void die()
    {
        if (!a.isFriendly)
        {
            Global.curr.enemyWaveDeathCount--;
            inCombat = false;
            deleteEnemy();
            if (!waveLost)
            {
                playGoldAnimation(a.bounty);
            }
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        else
        {
            inCombat = false;
            gameObject.GetComponent<Warrior>().diedLastWave = true;
            GlobalBehaviours.curr.applyAuraRangerBuff();
            gameObject.SetActive(false);
        }
    }

    void deleteEnemy()
    {
        Global.curr.enemies.Remove(this.gameObject);

    }

    void setTargetList()
    {
        if (a.isFriendly)
        {
            //Current unit is friendly and therefore chooses target from list of enemies
            targetList = Global.curr.enemies;
        }
        else
        {
            //Current unit is enemy and therefore chooses target from list of friendlies
            targetList = Global.curr.defenders;
        }
    }

    void pointTowardsTarget()
    {
        if (transform.position.x - target.transform.position.x < 0) //Ensuring that the unit always faces the direction it is moving.
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
    }

    void playGoldAnimation(int b)
    {
        /*GameObject goldAnim = Instantiate(goldAnimation, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f), Quaternion.identity);
        goldAnim.GetComponentInChildren<GoldAnimation>().play(b);*/
        AnimationController.curr.play("goldDrop", new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f), "+" + a.bounty.ToString(), "coinFlip", 3);
        Global.curr.gold += a.bounty;
        StatScreens.curr.enemyGold += a.bounty;
    }

    void playAttackSound(){
        AudioSystem.curr.createAndPlaySound(a.attackSound, Random.Range(0.9f, 1.1f));
    }
}
