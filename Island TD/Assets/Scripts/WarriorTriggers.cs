using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDrag(){
        EventSystem.current.defenderDrag(gameObject);
    }
}
