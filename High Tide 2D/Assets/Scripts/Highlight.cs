using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    public static Highlight curr;
    public GameObject darken;
    public GameObject go;
    public GameObject stronghold;
    public GameObject top;
    public GameObject arrowUI;
    public GameObject arrowWS;//arrow world space
    public GameObject arrowWSContainer;
    public GameObject canvas;
    public GameObject outlineAnimation;
    public GameObject outlineAnimationUI;
    public GameObject uiOverlay;
    bool busyPlayingOutline=false;



    //private attributes
    GameObject last;
    Transform initParent;
    int initSiblingIndex;
    int initLayer;
    void Awake()
    {
        curr=this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void darkenAllExcept(GameObject g){
        last=g;
        darken.SetActive(true);
        if(g.GetComponent<RectTransform>() != null){//if ui elem
            initParent = g.transform.parent;//initial parent of element
            initSiblingIndex = g.transform.GetSiblingIndex();//initial index withing layer
            g.transform.SetParent(top.transform);
        }else{//if worldspace elem
            g.layer=10;//put on layer above darken
            if(g.transform.childCount==0){
                return;
            }
            foreach(Transform child in g.transform){
                darkenAllExcept(child.gameObject);
            }

            initLayer = g.layer;
        }
    }

    public void arrowElement(GameObject e){
        if(e.GetComponent<RectTransform>() != null){//if ui elem
            //show only ui arrow arrow invisible
            arrowWSContainer.SetActive(false);
            arrowUI.SetActive(true);

            //calculate and set the positions for the ui arrow
            float x = e.GetComponent<RectTransform>().localPosition.x;
            x += e.GetComponent<RectTransform>().rect.width/2f;
            x += arrowUI.GetComponent<RectTransform>().rect.width/2f;
            x += 8f;//padding
            float y = e.GetComponent<RectTransform>().localPosition.y;
            arrowUI.GetComponent<RectTransform>().localPosition = new Vector3(x,y,0);
        }else{//if worldspace elem
            //show only ws arrow
            arrowUI.SetActive(false);
            arrowWSContainer.SetActive(true);

            //calculate and set the positions for the WS arrow
            Vector3 pos = e.transform.position;
            float elemWidth = e.GetComponent<SpriteRenderer>().bounds.size.x;
            float arrowWidth = arrowWS.GetComponent<SpriteRenderer>().bounds.size.x;
            float x = pos.x + elemWidth/2 + arrowWidth/2;
            float y = pos.y;
            arrowWSContainer.transform.position = new Vector2(x, y);
        }
    }

    //highlight and arrow
    public void focus(GameObject g){
        unFocus();
        darkenAllExcept(g);
        arrowElement(g);
    }
    public void unDarken(){
        if(last!=null){
            if(last.GetComponent<RectTransform>() != null){//if ui elem
                last.transform.SetParent(initParent);
                last.transform.SetSiblingIndex(initSiblingIndex);
            }else{//if worldspace elem
                unDarkenAll(last.transform);

                //last.layer=initLayer;
            }
        }
        darken.SetActive(false);
    }

    void unDarkenAll(Transform t){
        t.gameObject.layer=initLayer;
        if(transform.childCount==0){
            return;
        }
        foreach(Transform child in t){
            unDarkenAll(child);
        }
    }

    public void unArrow(){
        arrowWSContainer.SetActive(false);
        arrowUI.SetActive(false);
    }

    public void unFocus(){
        unDarken();
        unArrow();
    }

    public GameObject outlineAnimate(GameObject g, float xOffset=0, float yOffset=0, float widthOffset=0, float heightOffset=0, bool showBackground=false){
        if(isUIElement(g)){//if ui element
            //get pos
            Vector2 v = g.GetComponent<RectTransform>().anchoredPosition;
            //get dimensions
            float width = g.GetComponent<RectTransform>().rect.width;
            float height = g.GetComponent<RectTransform>().rect.height;

            //set outline according to pos and dimensions
            GameObject outline = Instantiate(outlineAnimationUI, new Vector2(0,0), Quaternion.identity);
            outline.transform.SetParent(canvas.transform);
            outline.GetComponent<RectTransform>().localPosition = new Vector2(v.x+xOffset, v.y+yOffset);
            outline.GetComponent<RectTransform>().sizeDelta = new Vector2(width+widthOffset, height+heightOffset);
            outline.GetComponent<RectTransform>().localScale = new Vector2(1,1);

            //show background
            GameObject background = outline.transform.Find("Background").gameObject;
            background.SetActive(showBackground);

            return outline;

        }else{
            //get pos
            Vector3 v = g.transform.position;
            //get dimensions
            float width = g.GetComponent<SpriteRenderer>().bounds.size.x;
            float height = g.GetComponent<SpriteRenderer>().bounds.size.y;

            //set outline according to pos and dimensions
            GameObject outline = Instantiate(outlineAnimation, new Vector3(v.x+xOffset, v.y+yOffset, v.z), Quaternion.identity);
            outline.GetComponent<SpriteRenderer>().size = new Vector3(width+widthOffset, height+heightOffset, width);

            //show background
            GameObject background = outline.transform.Find("Background").gameObject;
            background.SetActive(showBackground);

            return outline;
        }
    }

    public GameObject outlineAnimate(GameObject g, Color32 color, float xOffset=0, float yOffset=0, float widthOffset=0, float heightOffset=0, bool showBackground=false){
        GameObject outline = outlineAnimate(g, xOffset, yOffset, widthOffset, heightOffset, showBackground);
        if(isUIElement(outline)){
            //set colour
            outline.GetComponent<Image>().color = color;
            GameObject background = outline.transform.Find("Background").gameObject;
            background.GetComponent<Image>().color=color;
            
            return outline;
        }else{
            //set colour
            outline.GetComponent<SpriteRenderer>().color = (Color)color;
            GameObject background = outline.transform.Find("Background").gameObject;
            background.GetComponent<SpriteRenderer>().color = (Color)color;

            return outline;
        }      
    }


    public void outlineAnimateDuration(GameObject g, Color32 color, float durationSeconds=2f, float xOffset=0, float yOffset=0, float widthOffset=0, float heightOffset=0, bool showBackground=false){
        GameObject outline = outlineAnimate(g, xOffset, yOffset, widthOffset, heightOffset, showBackground);
        if(isUIElement(outline)){
            //set colour
            outline.GetComponent<Image>().color = color;
        }else{
            //set colour
            outline.GetComponent<SpriteRenderer>().color = (Color)color;
        }
        StartCoroutine(waitThenDestroy(outline, durationSeconds));
    }

    private IEnumerator waitThenDestroy(GameObject g, float durationSeconds){
        busyPlayingOutline=true;
        yield return new WaitForSeconds(durationSeconds);
        busyPlayingOutline=false;
        Destroy(g);
    }

    public void negativeHighlight(GameObject g, float durationSeconds=2.5f, float xOffset=0, float yOffset=0, float widthOffset=12, float heightOffset=12){
        AudioSystem.curr.createAndPlaySound("sword",.75f,1f);
        if(!busyPlayingOutline){
            Highlight.curr.outlineAnimateDuration(g, new Color32(255,21,21,255), durationSeconds, xOffset, yOffset, widthOffset, heightOffset, true);//red
        }
    }

    bool isUIElement(GameObject g){
        if(g.GetComponent<RectTransform>() != null){//if ui elem
           return true;
        }
        return false;
    }

    public void disableUI(){
        darken.SetActive(true);
    }

    public void enableUI(){
        darken.SetActive(false);
    }
}
