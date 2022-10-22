using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier
{
    public string name;
    public int lvl;
    public GameObject attachedTo;
    public Modifier(string n, int l, GameObject a){
        name=n;
        lvl=l;
        attachedTo=a;
    }
    public virtual void add(){

    }
    public virtual void remove(){

    }
}
