using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAnimation : MonoBehaviour
{
    public Animator anim;
    public GameObject lightning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroy(){
        Destroy(gameObject);
    }

    public void play(float x, float y){
        lightning.transform.position = new Vector2(x, y);
        lightning.SetActive(true);
        anim.Play("lightning");
    }
}
