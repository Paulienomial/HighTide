using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighlightSelected : MonoBehaviour
{
    public GameObject lastSelected;
    public GameObject highlightPrefab;
    public GameObject highlight;
    public GameObject cityInfo;
    public static HighlightSelected curr;

    void Awake(){
        curr=this;
    }
    // Start is called before the first frame update
    void Start()
    {
        highlight = Instantiate(highlightPrefab, new Vector3(0,0,0), Quaternion.identity);
        highlight.SetActive(false);
        cityInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select(GameObject g){
        if(highlight==null){
            highlight = Instantiate(highlightPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
        if(lastSelected!=g){

            if(g.GetComponent<CityHighlight>() /*&& ShopSystem.curr.shopOpen==false && GridSystem.curr.draggingPhase==false && GridSystem.curr.placingPhase==false*/){
                high(g);
                showCityInfo();
            }else{
                high(g);
                hideCityInfo();
            }
        }
    }

    public void high(GameObject g){
        highlight.SetActive(true);
        lastSelected=g;
        Vector3 size = lastSelected.GetComponent<Renderer>().bounds.size;
        highlight.GetComponent<SpriteRenderer>().size=size;
        highlight.transform.SetParent(lastSelected.transform);
        highlight.transform.position = lastSelected.transform.position;
    }

    public void deselect(){
        highlight.SetActive(false);
    }

    public void showCityInfo(){
        cityInfo.SetActive(true);
    }

    public void hideCityInfo(){
        cityInfo.SetActive(false);
    }

}
