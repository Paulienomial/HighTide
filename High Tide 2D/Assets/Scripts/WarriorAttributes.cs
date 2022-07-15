using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarriorAttributes : MonoBehaviour
{
    [Serializable]
    public class attr{
        public string name="archer";
        public int damage=20;
        public int hp=200;
        public int tier=1;
        public int price=3;
    }
}
