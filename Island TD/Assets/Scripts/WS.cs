using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WS : MonoBehaviour//contains the class that holds the default warrior stats
{
    [Serializable]
    public class WarriorStats{
        public string name;
        public int damage;
        public int hp;
        public float baseAttackTime=2f;
    }
}
