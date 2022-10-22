using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : Ability
{
    public int splitSize;
    void Start()
    {
        splitSize=3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void go(){
        if(!this) return;//if awaiting deletion

        StartCoroutine( split() );
    }

    IEnumerator split(){
        yield return new WaitForSeconds(0f);
        float x = transform.position.x;
        float y = transform.position.y;
        string warriorName="";
        if(a.ability=="Split 1") warriorName = "Medium Jeffrey";
        if(a.ability=="Split 2") warriorName = "Tiny Jeffrey";
        if(warriorName!=""){
            for(int i=0; i<splitSize; i++){
                GameObject child = Instantiate( Global.curr.warriorPrefab,  new Vector2(x, -2f + y + 2f*i), Quaternion.identity);
                child.GetComponent<Warrior>().setWarrior(warriorName);
                Global.curr.enemies.Add(child);
            }
        }
    }
}
