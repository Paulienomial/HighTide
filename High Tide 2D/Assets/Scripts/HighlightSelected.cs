using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighlightSelected : MonoBehaviour
{
    public GameObject lastSelected;
    public GameObject highlightPrefab;
    public GameObject highlight;
    public GameObject cityInfo;
    public GameObject warriorInfo;
    public static HighlightSelected curr;
    bool warriorSelected;

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
        if(warriorSelected){
            fillWarriorInfo();
        }
    }

    public void select(GameObject g){
        if(highlight==null){
            highlight = Instantiate(highlightPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
        if(lastSelected!=g){
            warriorSelected=false;
            if(g.GetComponent<CityHighlight>()){
                Debug.Log("Selecting city");
                high(g);
                showCityInfo();
            }else if (g.GetComponent<Warrior>()){
                high(g);
                showWarriorInfo();
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
        warriorSelected=false;
        lastSelected=null;
        highlight.SetActive(false);
        warriorInfo.SetActive(false);
        cityInfo.SetActive(false);
    }

    public void showCityInfo(){
        cityInfo.SetActive(true);
        warriorInfo.SetActive(false);
    }

    public void showWarriorInfo(){
        warriorInfo.SetActive(true);
        fillWarriorInfo();
        warriorSelected=true;
        cityInfo.SetActive(false);
    }

    public void fillWarriorInfo(){
        if(lastSelected!=null && lastSelected.GetComponent<Warrior>()){
            GameObject currCard = warriorInfo;
            WarriorAttributes.attr currWarrior = lastSelected.GetComponent<Warrior>().attributes;
            
            //Preview image
            GameObject previewBackground = currCard.transform.Find("PreviewBackground").gameObject;
            GameObject preview = previewBackground.transform.Find("Preview").gameObject;
            string path="Art/Warriors/preview"+currWarrior.name;
            Sprite mySprite = Resources.Load<Sprite>(path);
            preview.GetComponent<Image>().sprite=mySprite;

            //Level
            GameObject levelBackground = currCard.transform.Find("Level").gameObject;
            GameObject levelText = levelBackground.transform.Find("LevelText").gameObject;
            levelText.GetComponent<TextMeshProUGUI>().text = lastSelected.GetComponent<Warrior>().getLevel().ToString();

            //Name
            GameObject nameText = currCard.transform.Find("Name").gameObject;
            nameText.GetComponent<TextMeshProUGUI>().text = currWarrior.name;

            //Damage
            GameObject dmgIcon = currCard.transform.Find("dmgIcon").gameObject;
            GameObject damageText = dmgIcon.transform.Find("Damage").gameObject;
            damageText.GetComponent<TextMeshProUGUI>().text = currWarrior.damage.ToString();

            //HP
            GameObject hpIcon = currCard.transform.Find("hpIcon").gameObject;
            GameObject hpText = hpIcon.transform.Find("HP").gameObject;
            hpText.GetComponent<TextMeshProUGUI>().text = currWarrior.hp.ToString();

            //Description
            GameObject description = currCard.transform.Find("para").gameObject;
            description.GetComponent<TextMeshProUGUI>().text = currWarrior.description;
        }
    }

}
