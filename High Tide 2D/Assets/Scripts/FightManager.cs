using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField]
    GameObject city;
    [SerializeField]
    GameObject projectile;
    public GameObject target = null;
    public List<GameObject> targetList;
    public bool inCombat = false;
    public bool isAlive = true;
    public bool waveEnd = false;
    private bool facingRight = true;
    public bool projectileInAir = false;
    public bool waveLost = false;
    public bool unitPaused = false;
    public WarriorAttributes.attr a;
    public Warrior w;
    float fastforwardMovespeed;
    public int minusDmg=0;
    public float msMultiplier=1;
    public int plusDmg;
    public float dmgMultiplier;

    SpriteRenderer r;
    void Start()
    {
        w = gameObject.GetComponent<Warrior>();
        a = gameObject.GetComponent<Warrior>().attributes;
        r = GetComponent<SpriteRenderer>();
        fastforwardMovespeed = a.moveSpeed*3f;
        dmgMultiplier=1f;
    }

    // Update is called once per frame
    void Update()
    {
        waveEnd = waveManager.curr.waveComplete();
        if (!inCombat && Global.curr.waveStart && !waveManager.curr.waveEnd /*&& !isEnemyAndDead(gameObject)*/)
        {
            findTarget();
            if(target==null || gameObject==null || !this) return;
            if (!unitPaused && !isEnemyAndDead(gameObject))
            {
                moveToTarget();
            }
        }
    }

    void findTarget()
    {
        if(isEnemyAndDead(gameObject)) return;
        setTargetList();

        if (targetList.Count == 0)
        {
            //There are no potential targets for this unit
            target=null;

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
                    if (!a.isFriendly)//alive enemy
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
            GameObject prevTarget = target;
            target = bestTarget;
            if(target != prevTarget || isEnemyAndDead(gameObject)){//if target changed or this is a dead enemy
                CancelInvoke();
            }
            if (target == null && !a.isFriendly)//enemy and no target
            {
                target = city;
                a.moveSpeed = fastforwardMovespeed;
            }
        }
    }

    public static bool deadEnemy(GameObject g){
        return Global.curr.deadEnemies.Contains(g);
    }

    public static bool isEnemyAndDead(GameObject g){
        return g.GetComponent<Warrior>().attributes.isFriendly==false && deadEnemy(g);
    }

    void moveToTarget()
    {
        if(isEnemyAndDead(gameObject)) return;
        if (target != null)//if target alive
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
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x - shiftX, target.transform.position.y + shiftY), (a.moveSpeed*msMultiplier) * Time.deltaTime);
                }
                else
                {
                    if(target==null || gameObject==null || !this) return;
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.transform.position.x + shiftX, target.transform.position.y + shiftY), (a.moveSpeed*msMultiplier) * Time.deltaTime);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (!a.isRanged)
        {
            engageCombat(c.gameObject);
        }
    }

    public void engageCombat(GameObject opponent)
    {
        if (opponent.GetComponent<Warrior>() != null && !waveEnd) //Checking that target is a warrior and not the grid since grid also has a Rigidbody and thus triggers this
        {
            /*bool bothAlive = true;
            if (!a.isFriendly)
            {
                bothAlive = opponent.GetComponent<FightManager>().isAlive;
            }*/
            bool bothAlive=true;
            if(a.isFriendly){
                bothAlive = isAlive && !deadEnemy(opponent);
            }else{
                bothAlive = !deadEnemy(gameObject) && opponent.GetComponent<FightManager>().isAlive;
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
            if (opponent.GetComponent<CityHealthManager>() != null && !a.isFriendly && !isEnemyAndDead(gameObject))//enemy, that's targeting a city
            {
                waveLost = true;
                opponent.GetComponent<CityHealthManager>().takeDamage( a.cityDamage );
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
        float distance=0;
        if(gameObject!=null && target!=null){
            distance = Vector2.Distance (transform.position, target.transform.position);
            if(distance<0) distance*=-1;
        }
        if (target != null && !waveEnd && !unitPaused && target.GetComponent<FightManager>().isAlive && distance<3.5f)
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
                    doAttackDamage();
                }
            }
            else//enemy
            {
                if (target.GetComponent<FightManager>().isAlive && !waveEnd && !unitPaused && target.GetComponent<CityHealthManager>() == null && !isEnemyAndDead(gameObject))
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
                        doAttackDamage();
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
            newProjectile.GetComponent<ProjectileMover>().setProjectileSprite(a.projectile);
            newProjectile.GetComponent<ProjectileMover>().moveProjectile(this.gameObject, target);
        }
    }

    public void doDamage()
    {
        if (isAlive && !waveEnd && target != null && target.GetComponent<FightManager>().isAlive && !unitPaused)
        {
            FightManager victim = target.GetComponent<FightManager>();
            
            //int damageDealt = a.damage + plusDmg - minusDmg;
            int damageDealt = Mathf.RoundToInt((a.damage+plusDmg)*dmgMultiplier);
            if ((victim.a.hp - damageDealt) < 0)
            {//if the damage will cause victim's health to fall below zero
                damageDealt = victim.a.hp;
                victim.a.hp = 0;
            }
            else
            {
                victim.a.hp -= damageDealt;
            }
            Events.curr.hit(gameObject ,victim.gameObject);
            
            if (victim.a.isFriendly == false)
            {
                WaveBarController.curr.setHealth(WaveBarController.curr.getHealth() - damageDealt);
            }

            if (victim.a.hp <= 0)
            {
                victim.inCombat = false;//yes
                inCombat = false;
                //if (!a.isFriendly)
                victim.isAlive = false;//yes
                CancelInvoke();//yes
                gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);//yes
                if (victim.GetComponent<Warrior>().attributes.isFriendly)
                {
                    StatScreens.curr.fallenBrothers++;
                }
                else
                {
                    StatScreens.curr.enemiesKilled++;
                }
                victim.die();//yes
            }
        }
        else
        {
            CancelInvoke();
            gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
        }

    }

    public void doAttackDamage(){

        doAttackDamage(target.GetComponent<FightManager>());
    }

    public void doAttackDamage(FightManager victim){

        doDamage(victim, a.damage);

        Events.curr.hit(gameObject, victim.gameObject);
    }

    public void doDamage(FightManager victim, int dmg){

        int modifiedDamage = getModifiedDamage(dmg);//other classes, like Aura ranger might change the plusDmg or dmgMultiplier attributes
        victim.takeDamage(modifiedDamage, gameObject);
    }

    public void takeDamage(int dmg, GameObject damageDealer=null)
    {
        if(isEnemyAndDead(gameObject)) return;
        int damageDealt = dmg;
        if ((a.hp - dmg) <= 0)
        {//if the damage will cause victim's health to be smaller or equal to zero
            damageDealt = a.hp;
            a.hp = 0;
            inCombat = false;
            if(damageDealer!=null){
                damageDealer.GetComponent<FightManager>().inCombat=false;
            }
            isAlive = false;
            CancelInvoke();
            gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 0);
            die();
        }
        else
        {
            //a.hp -= a.damage;
            a.hp -= damageDealt;    
        }
        if (a.isFriendly == false)
        {
            WaveBarController.curr.setHealth(WaveBarController.curr.getHealth() - damageDealt);
        }
    }

    public int getExtraDamage(){
        return Mathf.RoundToInt( getModifiedDamage(a.damage) - (a.damage) );//eg (15+0)*1 -(15)=0
    }

    public int getModifiedDamage(int d){
        return Mathf.RoundToInt((d+plusDmg)*dmgMultiplier);
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
        isAlive=false;
        if (!a.isFriendly && !isEnemyAndDead(gameObject))
        {
            Global.curr.enemyWaveDeathCount--;
            inCombat = false;
            //deleteEnemy();
            if (!waveLost)
            {
                playGoldAnimation(a.bounty);
            }
            //Destroy(gameObject);
            Global.curr.enemies.Remove(gameObject);
            Global.curr.deadEnemies.Add(gameObject);
            target=null;
            gameObject.GetComponent<SpriteRenderer>().enabled=false;
            gameObject.GetComponent<WarriorRender>().canvas.GetComponent<Canvas>().enabled=false;
            //gameObject.transform.position = new Vector2(14f,14f);
        }
        else
        {
            inCombat = false;
            gameObject.GetComponent<Warrior>().diedLastWave = true;
            gameObject.SetActive(false);
        }
        Events.curr.die(gameObject);
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
        if(target==null || gameObject==null || !this) return;
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
        if(b>0){
            AnimationController.curr.play("goldDrop", new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f), "+" + a.bounty.ToString(), "coinFlip", 3);
            Global.curr.gold += a.bounty;
            StatScreens.curr.enemyGold += a.bounty;
        }
    }

    void playAttackSound(){
        float pitch = a.attackPitch;
        float minPitch = pitch-.1f;
        float maxPitch = pitch+.1f;
        AudioSystem.curr.createAndPlaySound(a.attackSound, Random.Range(minPitch, maxPitch), a.attackVolume);
    }
}
