using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudHandler : MonoBehaviour
{
    Global global;
    public TextMeshProUGUI pop;
    public TextMeshProUGUI gold;
    // Start is called before the first frame update
    void Start()
    {
        global=Global.curr;
    }

    // Update is called once per frame
    void Update()
    {
        pop.text = global.defenders.Count.ToString()+"/"+global.unitCap;
        gold.text = global.gold.ToString();
    }
}
