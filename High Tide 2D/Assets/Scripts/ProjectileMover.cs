using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectileMover : MonoBehaviour
{
    public bool startMoving = false;
    public GameObject shooter;
    GameObject target;
    Vector2 targetPos;
    bool hitTarget=false;
    Warrior shooterW;
    WarriorAttributes.attr shooterA;
    bool locationSet=false;
    bool leftToRight=true;
    bool topToBottom=false;
    // Start is called before the first frame update
    void Start()
    {
        shooterW = shooter.GetComponent<Warrior>();
        shooterA = shooterW.attributes;
        targetPos = new Vector2(0, 11);
    }

    // Update is called once per frame
    void Update()
    {
        if(startMoving && ( target==null || FightManager.isEnemyAndDead(target)) ){
            Destroy(gameObject);
        }
        shootProjectile();
    }

    public void moveProjectile(GameObject s, GameObject t)
    {
        shooter = s;
        target = t;
        startMoving = true;
    }

    void shootProjectile(){
        if (startMoving)
        {   
            setLocation();
            moveTowardsLocation();
            stopIfLocationReached();
        }
    }

    void setLocation(){
            if(target!=null && shooter!=null && shooter.GetComponent<FightManager>().isAlive /*&& !locationSet*/){//to set location, shooter and target must be alive
                //set location to move towards only once
                transform.right = target.transform.position - transform.position;
                float targetX = target.transform.position.x;
                float targetY = target.transform.position.y;
                targetPos = new Vector2(targetX, targetY);
                leftToRight = transform.position.x<targetX;
                topToBottom = transform.position.y>targetY;

                locationSet=true;
            }
            if(targetPos.y==11){
                Destroy(gameObject);
            }
    }

    void moveTowardsLocation(){
        transform.position = Vector2.MoveTowards(transform.position, targetPos, 3f * Time.deltaTime);
    }

    void stopIfLocationReached(){
        //if final location reached, then destroy gameobject
        float tolerance = .01f;
        bool reachedXPos;
        bool reachedYPos;
        if(leftToRight){
            reachedXPos = gameObject.transform.position.x >= targetPos.x-tolerance;
        }else{
            reachedXPos = gameObject.transform.position.x <= targetPos.x+tolerance;
        }
        if(topToBottom){
            reachedYPos = gameObject.transform.position.y <= targetPos.y+tolerance;
        }else{
            reachedYPos = gameObject.transform.position.y >= targetPos.y-tolerance;
        }
        if(reachedXPos && reachedYPos){
            //startMoving=false;
            Destroy(gameObject);
        }
    }

    //Do damage on trigger enter
    void OnTriggerEnter2D(Collider2D targetCollider){
        //if(!this) return;
        GameObject tcg = targetCollider.gameObject;
        bool targetExists = targetCollider!=null;
        bool targetIsWarrior = targetExists && tcg.GetComponent<Warrior>() != null;

        bool targetIsEnemy = targetIsWarrior && tcg.GetComponent<Warrior>().attributes.isFriendly != shooterA.isFriendly;

        bool primaryTarget = targetIsEnemy && tcg==target;
        if( shooterA.attackModifiers.Contains("splash") ){
            if(targetIsEnemy && shooter!=null){
                shooter.GetComponent<FightManager>().doAttackDamage(tcg.GetComponent<FightManager>());
            }
        }else{
            if(primaryTarget && shooter!=null){
                shooter.GetComponent<FightManager>().doAttackDamage(tcg.GetComponent<FightManager>());
            }
        }
    }

    public void setProjectileSprite(string projectileName){
        //step 1: set sprite
        string spritePath = "Art/Projectiles/"+projectileName;
        Sprite sprite = Resources.Load<Sprite>(spritePath);
        gameObject.GetComponent<SpriteRenderer>().sprite=sprite;

        //step 2: set box collider
        float width = sprite.bounds.size.x;
        float height = sprite.bounds.size.y;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
    }
}
