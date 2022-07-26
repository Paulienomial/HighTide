using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHealthManager : MonoBehaviour
{
    [SerializeField]
    public int CityHP = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
        CityHP--;
        Global.curr.CityHealth--;
        Debug.Log("City HP = " + CityHP);
        if (CityHP <= 0)
        {
            gameOver();
        }
    }

    void gameOver()
    {
        Debug.Log("CITY DESTROYED");
        Application.Quit();
    }
}
