using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTest : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] sounds;
    public GameObject audioCreator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            GameObject a = Instantiate(audioCreator, new Vector3(0,0,0), Quaternion.identity);
            a.GetComponent<AudioCreator>().createAndPlaySound("axe1");
        }
    }
}
