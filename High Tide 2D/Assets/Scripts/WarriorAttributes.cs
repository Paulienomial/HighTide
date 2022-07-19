using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarriorAttributes : MonoBehaviour
{
    [Serializable]
    public class attr{
        public string name="foot soldier";
        public int damage=30;
        public int hp=300;
        public int tier=1;
        public int price=3;
    }
}
