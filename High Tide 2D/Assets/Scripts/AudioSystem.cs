using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public GameObject audioCreator;
    public static AudioSystem curr;
    // Start is called before the first frame update
    void Awake()
    {
        curr=this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createAndPlaySound(string sound, float pitch=1f, float volume=.5f){
        GameObject a = Instantiate(audioCreator, new Vector3(0,5f,0), Quaternion.identity);
        a.GetComponent<AudioCreator>().createAndPlaySound(sound, pitch, volume);
    }
}
