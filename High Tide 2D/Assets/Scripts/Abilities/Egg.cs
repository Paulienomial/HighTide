using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Egg : Ability
{
    bool movingOffScreen=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movingOffScreen){
            if(transform.position.x>-10f){
                transform.Translate (transform.right * -4f * Time.deltaTime);
            }else{
                a.hp=0;
                gameObject.GetComponent<FightManager>().isAlive=false;
                gameObject.GetComponent<FightManager>().die();
            }
        }
    }

    public override void go(){//after wave
        if(!this) return;//if awaiting deletion

        movingOffScreen=false;
        gameObject.GetComponent<UpgradeDefender>().upgradeMergeCount(1);
        tryHatch();
    }

    public override void go2(){//wave start
        if(!this) return;//if awaiting deletion

        gameObject.GetComponent<FightManager>().pauseUnit();
        gameObject.GetComponent<WarriorRender>().animator.SetInteger("state", 1);
        movingOffScreen=true;
    }

    public void tryHatch(){
        if(!this) return;//if awaiting deletion
        if(w.getLevel()==3){
            string[] dragonNames = {"Red dragon", "Blue dragon", "Green dragon"};
            string dragonName = dragonNames[ Random.Range(0,3) ];
            global.defenders.Add( Instantiate(global.warriorPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity) );
            GameObject dragon = global.defenders.Last();
            dragon.GetComponent<Warrior>().setWarrior(dragonName);
            dragon.GetComponent<Warrior>().coordinates = new Vector3( gameObject.GetComponent<Warrior>().coordinates.x, gameObject.GetComponent<Warrior>().coordinates.y, gameObject.GetComponent<Warrior>().coordinates.z );

            global.defenders.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public override void go3(GameObject g,int i1,int i2){//on upgrade defender
        if(!this) return;//if awaiting deletion
        tryHatch();
    }
}
