using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public GameObject shop;
    public GameObject cardsContainer;
    public GameObject unitCard;
    int amountOfUnits;
    ArrayList shopSelection;//stores the selection of possible units from curr shop tier
    public LinkedList<WarriorAttributes.attr> shopUnits;//the 5 randomm units in the shop
    public ArrayList cards;//5 cards that display shopUnits
    public static ShopSystem curr;
    public bool shopAvailable;
    public bool shopOpen=false;
    public List<GameObject> lockedCards;
    bool rerolledThisWave;

    void Awake(){
        curr=this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rerolledThisWave=false;
        Events.curr.onWaveStart += ()=>{
            rerolledThisWave=false;
        };
        Events.curr.onReroll += ()=>{
            rerolledThisWave=true;
        };

        amountOfUnits=5;
        shopAvailable=true;

        shopSelection = new ArrayList();
        shopUnits = new LinkedList<WarriorAttributes.attr>();
        cards = new ArrayList();
        lockedCards = new List<GameObject>();

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
            if(i==0 && Global.curr.waveNum==2 && Global.curr.defenders.Count>=1 && Global.curr.defenders.ElementAt(i).GetComponent<Warrior>().attributes.name!="Farmer" && !rerolledThisWave){
                //for the tutorial level, we make it so the first unit in the shop on wave 2 will be the one that was placed in the tutorial
                WarriorAttributes.attr a = WarriorTypes.curr.find(Global.curr.defenders.ElementAt(i).GetComponent<Warrior>().attributes.name);
                shopUnits.AddLast(a);
            }else{
                int unitIndex = Random.Range(0,shopSelection.Count);//select a random unit from the shop selection
                shopUnits.AddLast( (WarriorAttributes.attr)shopSelection[unitIndex] );//add that unit to the shopUnits list
            }
        }
    }

    public void fillShopSelection(int shopTier){
        shopSelection.Clear();
        foreach(WarriorAttributes.attr warrior in WarriorTypes.curr.wList.warriors){
            if(warrior.tier<=shopTier && warrior.isFriendly && warrior.name!="Farmer"){
                shopSelection.Add(warrior);
            }
        }
    }

    public void createCards(){
        List<int> lockedIndexes = new List<int>();
        List<GameObject> lockedCards = new List<GameObject>();
        int idx=0;
        foreach(GameObject card in cards){
            if( card.GetComponent<CardLock>().locked && card.activeSelf){//if the card is locked, and visible
                lockedIndexes.Add(idx);
                lockedCards.Add(card);
            }else{
                Destroy(card);
            }
            idx++;
        }
        cards.Clear();
        //create the cards from shopUnits

        int storedLocks = 0;
        for(int i=0; i<amountOfUnits; i++){
            if(lockedIndexes.Contains(i)){//if locked card
                //then just re add to list
                cards.Add( lockedCards.ElementAt(storedLocks++) );
            }else{
                //Add new card to list
                cards.Add( Instantiate(unitCard, cardsContainer.transform) );
                GameObject currCard = (GameObject)cards[i];
                currCard = (GameObject)cards[i];
                //change card position
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
                //currCard.GetComponent<RectTransform>().anchoredPosition= new Vector3(-125f+272.5f*((i+1)%2), 242f-244.5f*k,0f);
                currCard.GetComponent<RectTransform>().anchoredPosition= new Vector3(100+1720f/5f*(i+1f)-1720f/5f/2f, 0f,0f);

                //Preview image
                GameObject previewBorder = currCard.transform.Find("PreviewBorder").gameObject;
                GameObject previewBackground = previewBorder.transform.Find("PreviewBackground").gameObject;
                GameObject preview = previewBackground.transform.Find("Preview").gameObject;
                string path="Art/Warriors/preview"+shopUnits.ElementAt(i).name;
                Sprite mySprite = Resources.Load<Sprite>(path);
                preview.GetComponent<Image>().sprite=mySprite;

                //Name
                GameObject nameText = currCard.transform.Find("Name").gameObject;
                nameText.GetComponent<TextMeshProUGUI>().text = shopUnits.ElementAt(i).name;

                //Damage
                GameObject dmgIcon = currCard.transform.Find("dmgIcon").gameObject;
                GameObject damageText = dmgIcon.transform.Find("Damage").gameObject;
                damageText.GetComponent<TextMeshProUGUI>().text = shopUnits.ElementAt(i).damage.ToString();

                //HP
                GameObject hpIcon = currCard.transform.Find("hpIcon").gameObject;
                GameObject hpText = hpIcon.transform.Find("HP").gameObject;
                hpText.GetComponent<TextMeshProUGUI>().text = shopUnits.ElementAt(i).hp.ToString();

                //Tier
                GameObject tierBackground = currCard.transform.Find("TierBackground").gameObject;
                GameObject tierText = tierBackground.transform.Find("Tier").gameObject;
                tierText.GetComponent<TextMeshProUGUI>().text = "Tier: " + shopUnits.ElementAt(i).tier.ToString();

                //Description
                GameObject description = currCard.transform.Find("para").gameObject;
                description.GetComponent<TextMeshProUGUI>().text = shopUnits.ElementAt(i).descriptions[0];

                //Buy button
                GameObject buyButton = currCard.transform.Find("BuyButton").gameObject;
                buyButton.GetComponent<ShopCard>().warrior=shopUnits.ElementAt(i);
                buyButton.GetComponent<ShopCard>().card=currCard;
                buyButton.GetComponent<ShopCard>().shop=shop;

                GameObject goldBackground = currCard.transform.Find("GoldBackground").gameObject;
                GameObject buyText = goldBackground.transform.Find("BuyText").gameObject;
                buyText.GetComponent<TextMeshProUGUI>().text = shopUnits.ElementAt(i).price.ToString();
            }
        }
    }

    public void showHideShop(){
        AudioScript.curr.playButtonClickSound();
        if(shopAvailable){
            if(shop.activeSelf==false){
                shopOpen=true;
                shop.SetActive(true);
                Tutorial.curr.showMergeTutorialTip();
                Tutorial.curr.showFarmerTip();
                Tutorial.curr.showLockTip();
            }else{
                shopOpen=false;
                shop.SetActive(false);
            }
        }
    }

    
}
