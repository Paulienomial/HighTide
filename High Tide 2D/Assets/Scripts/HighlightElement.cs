using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightElement : MonoBehaviour
{
    public GameObject canvas;
    public GameObject element;
    public GameObject darken;
    public GameObject top;
    Transform initParent;
    int initSiblingIndex;
    public static HighlightElement curr;

    void Awake(){
        curr=this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            highlight(element);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            unHighlight();
        }
    }

    public void highlight(GameObject g){
        element=g;
        initSiblingIndex = element.transform.GetSiblingIndex();//initial index withing layer
        initParent = element.transform.parent;//initial parent of element
        darken.SetActive(true);
        element.transform.parent = top.transform;//put element at top
    }

    public void unHighlight(){
        darken.SetActive(false);
        element.transform.parent = initParent;
        element.transform.SetSiblingIndex(initSiblingIndex);
    }
}
