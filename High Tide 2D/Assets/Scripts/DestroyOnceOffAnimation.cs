using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnceOffAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyAnimation(){
        Destroy(gameObject.transform.parent.gameObject.transform.parent.gameObject);
    }
}
