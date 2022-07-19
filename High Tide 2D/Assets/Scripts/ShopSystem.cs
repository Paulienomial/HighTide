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
    public LinkedList<WarriorAttributes.attr> shopUnits;
    public ArrayList cards;//will be type GameObject

    // Start is called before the first frame update
    void Start()
    {
        amountOfUnits=5;

        shopSelection = new ArrayList();
        shopUnits = new LinkedList<WarriorAttributes.attr>();
        cards = new ArrayList();

        createShop();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        foreach(WarriorAttributes.attr warrior in shopUnits){
            //Debug.Log(warrior.name);
        }
    }

    public void fillShopSelection(int shopTier){
        foreach(WarriorAttributes.attr warrior in WarriorTypes.curr.wList.warriors){
            if(warrior.tier<=shopTier){
                shopSelection.Add(warrior);
            }
        }
    }

    public void createCards(){
        for(int i=0; i<amountOfUnits; i++){
            //instantiate a unitCard as a child of the shop
            cards.Add( Instantiate(unitCard, shop.transform) );
            //change card position
            GameObject currCard = (GameObject)cards[i];
            currCard.GetComponent<RectTransform>().anchoredPosition= new Vector3(-800f+300f*i,0f,0f);


            //Preview image
            GameObject preview = currCard.transform.Find("Preview").gameObject;
            string path="Art/Warriors/"+shopUnits.ElementAt(i).name;
            Sprite mySprite = Resources.Load<Sprite>(path);
            preview.GetComponent<Image>().sprite=mySprite;

            //Name
            GameObject nameText = currCard.transform.Find("Name").gameObject;
            nameText.GetComponent<TextMeshProUGUI>().text = shopUnits.ElementAt(i).name;

            //Damage
            GameObject damageText = currCard.transform.Find("Damage").gameObject;
            damageText.GetComponent<TextMeshProUGUI>().text = "Damage: " + shopUnits.ElementAt(i).damage.ToString();

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
            buyText.GetComponent<TextMeshProUGUI>().text = "$ " + shopUnits.ElementAt(i).price.ToString();
        }
    }

    public void showHideShop(){
        if(shop.activeSelf==false){
            shop.SetActive(true);
        }else{
            shop.SetActive(false);
        }
    }
}
