using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public bool startMoving = false;
    GameObject shooter;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            if(target != null && shooter.GetComponent<FightManager>().isAlive)
            {
                transform.right = target.transform.position - transform.position;
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 3f * Time.deltaTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void moveProjectile(GameObject s, GameObject t)
    {
        shooter = s;
        target = t;
        startMoving = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.GetComponent<Warrior>() != null && target != null && shooter.GetComponent<FightManager>().isAlive)
        {
            if (!target.GetComponent<Warrior>().attributes.isFriendly)
            {
                Debug.Log("Archer hit");
                startMoving = false;
                if(target != null && shooter.GetComponent<FightManager>().isAlive)
                {
                    shooter.GetComponent<FightManager>().doDamage();
                }
                Destroy(gameObject);
            }
        }
    }
}
