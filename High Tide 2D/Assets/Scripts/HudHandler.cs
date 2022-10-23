using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudHandler : MonoBehaviour
{
    Global global;
    public TextMeshProUGUI waveNum;
    public TextMeshProUGUI lives;
    public TextMeshProUGUI pop;
    public TextMeshProUGUI gold;
    public Slider cityHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        global=Global.curr;
    }

    // Update is called once per frame
    void Update()
    {
        waveNum.text = "WAVE "+Global.curr.waveNum.ToString();
        lives.text = global.CityHealth.ToString();
        pop.text = global.defenders.Count.ToString()+"/"+global.unitCap;
        gold.text = global.gold.ToString();
        cityHealthBar.value = Global.curr.CityHealth;
        cityHealthBar.maxValue = Global.curr.maxCityHealth;
        WaveBarController.curr.setText("Wave " + Global.curr.waveNum +"/"+Waves.curr.waves.Count);
    }
}
