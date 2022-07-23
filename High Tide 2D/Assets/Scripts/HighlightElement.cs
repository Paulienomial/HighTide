using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightElement : MonoBehaviour
{
    public GameObject canvas;
    public GameObject element;
    public GameObject darken;
    public GameObject top;
    public GameObject darkenWS;
    Transform initParent;
    int initSiblingIndex;
    public static HighlightElement curr;
    public GameObject arrowAnimation;
    public Camera camUI;

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

        arrowAnimation.SetActive(true);
        if(element.GetComponent<RectTransform>() != null){//if ui elem
            //darken
            darken.SetActive(true);
            element.transform.SetParent(top.transform);//put element at top

            //arrow animation
            float x = element.GetComponent<RectTransform>().localPosition.x;
            x += element.GetComponent<RectTransform>().rect.width/2f;
            x += arrowAnimation.GetComponent<RectTransform>().rect.width/2f;
            x += 8f;
            float y = element.GetComponent<RectTransform>().localPosition.y;
            arrowAnimation.GetComponent<RectTransform>().localPosition = new Vector3(x,y,0);
        }else{//if worldspace elem
            Vector3 p = element.transform.position;
            Vector3 rightEdge = new Vector3(p.x + element.GetComponent<SpriteRenderer>().bounds.size.x/2f, p.y, p.z);
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(rightEdge);
            float x = screenPoint.x;
            x += arrowAnimation.GetComponent<RectTransform>().rect.width/2f;
            x += 8f;
            float y = screenPoint.y;
            arrowAnimation.GetComponent<RectTransform>().localPosition = new Vector3(x-Screen.width/2f,y-Screen.height/2f,0);
        }
    }

    public void arrow(GameObject g){
        element=g;
        initSiblingIndex = element.transform.GetSiblingIndex();//initial index withing layer
        initParent = element.transform.parent;//initial parent of element

        arrowAnimation.SetActive(true);
        if(element.GetComponent<RectTransform>() != null){//if ui elem
            //darken
            //darken.SetActive(true);
            element.transform.SetParent(top.transform);//put element at top

            //arrow animation
            float x = element.GetComponent<RectTransform>().localPosition.x;
            x += element.GetComponent<RectTransform>().rect.width/2f;
            x += arrowAnimation.GetComponent<RectTransform>().rect.width/2f;
            x += 8f;
            float y = element.GetComponent<RectTransform>().localPosition.y;
            arrowAnimation.GetComponent<RectTransform>().localPosition = new Vector3(x,y,0);
        }else{//if worldspace elem
            Vector3 p = element.transform.position;
            Vector3 rightEdge = new Vector3(p.x + element.GetComponent<SpriteRenderer>().bounds.size.x/2f, p.y, p.z);
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(rightEdge);
            float x = screenPoint.x;
            x += arrowAnimation.GetComponent<RectTransform>().rect.width/2f;
            x += 8f;
            float y = screenPoint.y;
            arrowAnimation.GetComponent<RectTransform>().localPosition = new Vector3(x-Screen.width/2f,y-Screen.height/2f,0);
        }
    }

    public void unHighlight(){
        darken.SetActive(false);
        arrowAnimation.SetActive(false);
        element.transform.SetParent(initParent);
        element.transform.SetSiblingIndex(initSiblingIndex);
    }
}
