using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class WaveInfo : MonoBehaviour
{
    public GameObject openIcon;
    public GameObject closeIcon;
    bool open = true;
    RectTransform rt;
    bool playingAnimation;
    public GameObject enemyInfoPrefab;
    List<GameObject> enemyInfos;
    public GameObject enemiesContainer;
    public TextMeshProUGUI waveInfoTitle;
    public Scrollbar scrollBar;
    public static WaveInfo curr;
    // Start is called before the first frame update
    void Awake(){
        curr=this;
    }
    
    void Start()
    {
        rt = GetComponent<RectTransform>();
        playingAnimation=false;
        enemyInfos = new List<GameObject>();
        Events.curr.onSetWaves += drawWaveInfo;
        Events.curr.onGameOver += drawWaveInfo;
        Events.curr.onWinGame += drawWaveInfo;
        Events.curr.onWaveComplete += drawWaveInfo;
        Events.curr.onWaveComplete += openInfo;
        Events.curr.onWaveStart += closeInfo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void drawWaveInfo(){
        waveInfoTitle.text = "WAVE "+Global.curr.waveNum+":";
        int groupsCount = Waves.curr.waves[Global.curr.waveNum-1].enemyGroups.Count;
        enemiesContainer.GetComponent<RectTransform>().SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, 430*groupsCount);
        scrollBar.value=1;
        int i=0;
        foreach(GameObject go in enemyInfos){
            Destroy(go);
        }
        enemyInfos.Clear();
        foreach(EnemyGroup eg in Waves.curr.waves[Global.curr.waveNum-1].enemyGroups){
            enemyInfos.Add( Instantiate(enemyInfoPrefab, new Vector3(0, 0, 0), Quaternion.identity) );
            GameObject curr = enemyInfos.Last();
            enemyInfos.Last().transform.SetParent( enemiesContainer.transform );
            RectTransform ert = curr.GetComponent<RectTransform>();
            ert.localScale = new Vector3(1,1,1); 
            ert.localPosition = new Vector3(0,0,0);
            ert.anchoredPosition = new Vector3(0, -215+i*-430, 0);

            EnemyInfo ei = curr.GetComponent<EnemyInfo>();
            //Name:
            ei.title.text = eg.name;
            //Preview image:
            string path="Art/Warriors/preview"+eg.name;
            Sprite mySprite = Resources.Load<Sprite>(path);
            ei.previewImage.sprite = mySprite;
            //Multiple:
            ei.multiple.text = "X" + eg.count.ToString();
            //Description:
            ei.description.text = WarriorTypes.curr.find(eg.name).descriptions[0];

            i++;
        }
    }

    public void toggleWaveInfo(){
        if(open){
            open=false;
            openIcon.SetActive(true);
            closeIcon.SetActive(false);
            //rt.anchoredPosition = new Vector2(rt.sizeDelta.x/2, rt.anchoredPosition.y);
            StartCoroutine( animateClose() );
        }else{
            open=true;
            closeIcon.SetActive(true);
            openIcon.SetActive(false);
            //rt.anchoredPosition = new Vector2(-rt.sizeDelta.x/2, rt.anchoredPosition.y);
            StartCoroutine( animateOpen() );
        }
    }

    IEnumerator animateClose(){
        while(playingAnimation){}
        playingAnimation=true;
        while(rt.anchoredPosition.x < rt.sizeDelta.x/2){
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x+20, rt.anchoredPosition.y);
            yield return new WaitForSeconds(.01f);
        }
        rt.anchoredPosition = new Vector2(rt.sizeDelta.x/2, rt.anchoredPosition.y);
        playingAnimation=false;
    }

    IEnumerator animateOpen(){
        while(playingAnimation){}
        playingAnimation=true;
        while(rt.anchoredPosition.x > -rt.sizeDelta.x/2){
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x-20, rt.anchoredPosition.y);
            yield return new WaitForSeconds(.01f);
        }
        rt.anchoredPosition = new Vector2(-rt.sizeDelta.x/2, rt.anchoredPosition.y);
        playingAnimation=false;
    }

    void openInfo(){
        if(!open){
            toggleWaveInfo();
        }
    }

    void closeInfo(){
        if(open){
            toggleWaveInfo();
        }
    }
}
