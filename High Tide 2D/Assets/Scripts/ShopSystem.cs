using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public GameObject shop;
    public GameObject unitCard;
    int amountOfUnits;
    ArrayList shopSelection;//stores the selection of possible units from curr shop tier
    public LinkedList<WarriorAttributes.attr> shopUnits;//the 5 randomm units in the shop
    public ArrayList cards;//5 cards that display shopUnits
    public static ShopSystem curr;
    public bool shopAvailable;

    void Awake(){
        curr=this;
    }

    // Start is called before the first frame update
    void Start()
    {
        amountOfUnits=5;
        shopAvailable=true;

        shopSelection = new ArrayList();
        shopUnits = new LinkedList<WarriorAttributes.attr>();
        cards = new ArrayList();

        createShop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            createShop();
        }
    }

    public void createShop(){
        createShopList();
        createCards();
    }

    private void createShopList(){
        shopUnits.Clear();
        fillShopSelection(Global.curr.shopTier);//the total amount of units that is of the curr shop tier or lower
        for(int i=0; i<amountOfUnits; i++){
            int unitIndex = Random.Range(0,shopSelection.Count);//select a random unit from the shop selection
            shopUnits.AddLast( (WarriorAttributes.attr)shopSelection[unitIndex] );//add that unit to the shopUnits list
        }
    }

    public void fillShopSelection(int shopTier){
        shopSelection.Clear();
        foreach(WarriorAttributes.attr warrior in WarriorTypes.curr.wList.warriors){
            if(warrior.tier<=shopTier && warrior.isFriendly){
                shopSelection.Add(warrior);
            }
        }
    }

    public void createCards(){
        foreach(GameObject card in cards){//destroy prev cards
            Destroy(card);
        }
        cards.Clear();

        //create cards from shopUnits
        for(int i=0; i<amountOfUnits; i++){
            //instantiate a unitCard as a child of the shop
            cards.Add( Instantiate(unitCard, shop.transform) );
            //change card position
            GameObject currCard = (GameObject)cards[i];
            int k=0;
            if(i==0){
                k=0;
            }
            if(i>0 && i<=2){
                k=1;
            }
            if(i>2){
                k=2;
            }
            currCard.GetComponent<RectTransform>().anchoredPosition= new Vector3(-125f+272.5f*((i+1)%2), 242f-244.5f*k,0f);

            //Background
            GameObject backgroundUpper = currCard.transform.Find("BackgroundUpper").gameObject;
            GameObject backgroundLower = currCard.transform.Find("BackgroundLower").gameObject;
            
            if(i<=1 || i==4){
                backgroundUpper.GetComponent<Image>().color = new Color32(178,192,212,255);//dark grey cool
                backgroundLower.GetComponent<Image>().color = new Color32(218,232,247,255);//light grey cool
            }else{
                backgroundUpper.GetComponent<Image>().color = new Color32(255,255,255,255);//white
                backgroundLower.GetComponent<Image>().color = new Color32(218,225,229,255);//light grey neutral
            }

            //Preview image
            GameObject preview = currCard.transform.Find("Preview").gameObject;
            string path="Art/Warriors/preview"+shopUnits.ElementAt(i).name;
            Sprite mySprite = Resources.Load<Sprite>(path);
            preview.GetComponent<Image>().sprite=mySprite;

            //Name
            GameObject nameText = currCard.transform.Find("Name").gameObject;
            nameText.GetComponent<TextMeshProUGUI>().text = shopUnits.ElementAt(i).name;

            //Damage
            GameObject damageText = currCard.transform.Find("Damage").gameObject;
            damageText.GetComponent<TextMeshProUGUI>().text = "DMG: " + shopUnits.ElementAt(i).damage.ToString();

            //HP
            GameObject hpText = currCard.transform.Find("HP").gameObject;
            hpText.GetComponent<TextMeshProUGUI>().text = "HP: " + shopUnits.ElementAt(i).hp.ToString();

            //Tier
            GameObject tierText = currCard.transform.Find("Tier").gameObject;
            tierText.GetComponent<TextMeshProUGUI>().text = "Tier: " + shopUnits.ElementAt(i).tier.ToString();

            //Buy button
            GameObject buyButton = currCard.transform.Find("BuyButton").gameObject;
            buyButton.GetComponent<ShopCard>().warrior=shopUnits.ElementAt(i);
            buyButton.GetComponent<ShopCard>().card=currCard;
            buyButton.GetComponent<ShopCard>().shop=shop;
            GameObject buyText = buyButton.transform.Find("BuyText").gameObject;
            buyText.GetComponent<TextMeshProUGUI>().text = shopUnits.ElementAt(i).price.ToString();
        }
    }

    public void showHideShop(){
        if(shopAvailable){
            if(shop.activeSelf==false){
                shop.SetActive(true);
            }else{
                shop.SetActive(false);
            }
        }
    }
}
