using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class DigSceneManager : MonoBehaviour
{
    private const int Count_h = 13;
    private const int Count_v = 10;
    private const int maxTryCount = 20;
    private const int maxFossilCount = 5;
    private const int panelLayermask = 1 << 13;
    private const int fossilLayermask = 1 << 14;
    private const int clickTriggerLayermask = 1 << 15;

    public GameManager gameManager;
    public RPCGroupSettings groupSettings;
    public SetCustomPropertiesManager propertiesManager;
    public Camera mainCamera;
    public Camera playerCamera;
    public GameObject panelParent;
    public GameObject panelPrefab;
    public GameObject fossilPrefab;
    public GameObject clickTrigger;
    public GameObject mainSceneCanvas;
    public GameObject digSceneCanvas;
    public GameObject getTextObj;
    public Button pickelButton;
    public Button hummerButton;
    public Sprite[] panelSprites = new Sprite[4];
    public Slider hpBar;
    public Text fossilCountText;
    public Text getText;
    public int[,] panelCount = new int[Count_h, Count_v];

    public PlayerAction playerAction;
    private Having having;
    private FossilInfo fossilInfo = new FossilInfo();
    private SpriteRenderer[,] panelSpriteRenderer = new SpriteRenderer[Count_h, Count_v];
    private List<int> generateFossilNumList = new List<int>();
    private List<int> getFossilNumList = new List<int>();
    private List<GameObject> generateFossilList = new List<GameObject>();
    private List<GameObject> getFossilList = new List<GameObject>();
    private List<GameObject> panelsList = new List<GameObject>();
    private List<GameObject> triggersList = new List<GameObject>();
    private PhotonView photonView;
    private Vector3 generatePos;
    private Vector2 clickPos;
    private int tryCount=0;
    private int hp = 30;
    private int fossilNum;
    private bool isPickel = false;
    private bool isHummer = true;
    private bool first = false;
    //private bool first = true;
    int[] translatedPanelCount = new int[Count_h* Count_v];

    private enum DigMode
    {
        pickel,
        hummer,
    }

    private void Awake()
    {

    }
    private void OnEnable()
    {
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();
        photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Count_h; i++)
        {
            for (int j = 0; j < Count_v; j++)
            {
                var panel = Instantiate(panelPrefab);
                var trigger = Instantiate(clickTrigger);
                panel.transform.SetParent(panelParent.transform);
                panel.transform.localPosition = new Vector3(i, j, 0);
                trigger.transform.SetParent(panelParent.transform);
                trigger.transform.localPosition = new Vector3(i, j, 0);
                panelsList.Add(panel);
                triggersList.Add(trigger);
                panelSpriteRenderer[i, j] = panel.GetComponent<SpriteRenderer>();
            }
        }

        gameManager.ObserveEveryValueChanged(x => x.isDigScene)
            .Where(x => x == true)
            .Subscribe(async _ =>
            {
                var token = this.GetCancellationTokenOnDestroy();
                await Initialization(token);
                //int a = PhotonNetwork.LocalPlayer.ActorNumber;
                groupSettings.AddGroup(PhotonNetwork.LocalPlayer.ActorNumber);
                photonView.Group = (byte)PhotonNetwork.LocalPlayer.ActorNumber;
                Debug.Log("playerAction.digMaster: "+playerAction.digMaster);

                if (!playerAction.digMaster)//最初の人
                {
                    first=true;
                    await Fossil(token);
                    ClickAsync(token).Forget();
                    propertiesManager.PlayerCustomPropertiesSettings(true,propertiesManager.digKey,PhotonNetwork.LocalPlayer);
                }
                else//後から入ってきた人
                {
                    ResieveFossilPosInfo(playerAction.talkToPlayer);//
                }

            })
            .AddTo(this);
    }

    private async UniTask Initialization(CancellationToken token = default)
    {
        mainSceneCanvas.SetActive(false);
        digSceneCanvas.SetActive(true);
        first = false;
        hp = 30;
        hpBar.value = 1;
        getTextObj.SetActive(false);
        generateFossilNumList.Clear();
        getFossilNumList.Clear();
        foreach(var x in generateFossilList)
        {
            Destroy(x);
        }
        foreach (var x in getFossilList)
        {
            Destroy(x);
        }
        generateFossilList.Clear();
        getFossilList.Clear();
        await UniTask.DelayFrame(1);
        //first = false;
    }


    /// <summary>
    /// 最初に掘り始めた人の処理
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask Fossil(CancellationToken token = default)
    {
        await GeneratePanels(token);
        while (generateFossilList.Count < 2)
        {
            await GetFossilGeneratePos(token);
            fossilCountText.text = generateFossilList.Count + "個";
        }
    }


    public void ResieveFossilPosInfo(Player player)
    {
        int hpRPC = (int)player.CustomProperties[propertiesManager.hpKey];
        int[] panelCountRPC=(int[])player.CustomProperties[propertiesManager.panelListKey];

        groupSettings.AddGroup((byte)player.ActorNumber);
        photonView.Group = (byte)player.ActorNumber;

        hp = hpRPC;
        hpBar.value = hp/30;
        int k = 0;
        for (int i = 0; i < Count_h; i++)
        {
            for (int j = 0; j < Count_v; j++)
            {
                panelCount[i, j] = panelCountRPC[k];

                foreach (var a in panelsList)
                {
                    a.SetActive(true);
                }
                if (panelCountRPC[k]<0)
                {
                    panelSpriteRenderer[i, j].sprite = panelSprites[0];
                }
                else
                {
                    panelSpriteRenderer[i, j].sprite = panelSprites[panelCountRPC[k]];
                }   
                k++;
            }
        }

        ClickAsync(default).Forget();
    }



    public async UniTask GeneratePanels(CancellationToken token = default)
    {
        int k = 0;
        for(int i = 0; i < Count_h; i++)
        {
            for(int j = 0; j < Count_v; j++)
            {
                int count = Random.Range(1, 4);
                panelCount[i, j] = count;
                translatedPanelCount[k] = count;
                /*if (first)
                {
                    var panel = Instantiate(panelPrefab);
                    var trigger = Instantiate(clickTrigger);
                    panel.transform.SetParent(panelParent.transform);
                    panel.transform.localPosition = new Vector3(i, j, 0);
                    trigger.transform.SetParent(panelParent.transform);
                    trigger.transform.localPosition = new Vector3(i, j, 0);
                    panelsList.Add(panel);
                    triggersList.Add(trigger);
                    panelSpriteRenderer[i, j] = panel.GetComponent<SpriteRenderer>();
                }*/
                foreach(var a in panelsList)//??????
                {
                    a.SetActive(true);
                }
                panelSpriteRenderer[i, j].sprite = panelSprites[count];
                k++;
                await UniTask.DelayFrame(1);
                
            }
        }
        propertiesManager.PlayerCustomPropertiesSettings(translatedPanelCount,propertiesManager.panelListKey, PhotonNetwork.LocalPlayer);
    }

    public async UniTask GetFossilGeneratePos(CancellationToken token = default)
    {
        int fossilCount = Random.Range(3, maxFossilCount);
        for(int i = 0; i < fossilCount; i++)
        {
            tryCount = 0;
            fossilNum = Random.Range(0, fossilInfo.FossilInfoDic.Count);
            
            switch (fossilNum)
            {
                case 0:
                case 3:
                case 6:
                case 9:
                    while (true)
                    {
                        if (tryCount > maxTryCount) break;
                        generatePos = new Vector3(Random.Range(0.5f, 11.5f), Random.Range(0.5f, 8.5f), 0.01f);
                        if (CheckGeneratePos(1)) break;
                        await UniTask.DelayFrame(1);
                    }
                    break;
                case 1:
                case 4:
                case 7:
                case 10:
                    while (true)
                    {
                        if (tryCount > maxTryCount) break;
                        generatePos = new Vector3(Random.Range(1f, 11f), Random.Range(1f, 8f), 0.01f);
                        if (CheckGeneratePos(1.5f)) break;
                        await UniTask.DelayFrame(1);
                    }
                    break;
                case 2:
                case 5:
                case 8:
                case 11:
                    while (true)
                    {
                        if (tryCount > maxTryCount) break;
                        generatePos = new Vector3(Random.Range(1.5f, 10.5f), Random.Range(1.5f, 7.5f), 0.01f);
                        if (CheckGeneratePos(2)) break;
                        await UniTask.DelayFrame(1);
                    }
                    break;
            }

            if (tryCount > maxTryCount) continue;
            GenerateFossil();
        }
    }
    
    private void GenerateFossil()
    {
        generateFossilNumList.Add(fossilNum);

        var fossil = Instantiate(fossilPrefab);
        float size = 0.25f * ((int)fossilInfo.FossilInfoDic[(int)fossilNum].fossileSize + 2);
        fossil.transform.SetParent(panelParent.transform);
        fossil.transform.localPosition = generatePos;
        fossil.transform.localScale = new Vector3(size, size, 1);
        Addressables.LoadAssetAsync<Sprite>(fossilInfo.FossilInfoDic[(int)fossilNum].prefabAddress).Completed += handle =>
        {
            // ロードに成功した場合の処理をここに
            fossil.GetComponent<SpriteRenderer>().sprite = handle.Result;
        };

        generateFossilList.Add(fossil);
    }
    
    public bool CheckGeneratePos(float halfFossilSize)
    {
        Debug.DrawRay(panelParent.transform.position + generatePos + Vector3.forward, Vector3.back,Color.green,10);
        if(Physics.BoxCast(panelParent.transform.position + generatePos + Vector3.forward, new Vector3(halfFossilSize, halfFossilSize, 0.05f), 
            Vector3.back, Quaternion.identity, 10.0f, fossilLayermask))
        {
            tryCount++;
            return false;
        }
        return true;
    }

    private async UniTask ClickAsync(CancellationToken token = default)
    {
        while (true)
        {
            var result = await UniTask.WhenAny(
                UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: token),
                UniTask.WaitUntil(() => hp <= 0 || generateFossilList.Count <= 0)
                );

            if (result == 0)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit, 100f, clickTriggerLayermask))
                {
                    clickPos = hit.collider.transform.localPosition;
                    if (isPickel)
                    {
                        photonView.RPC(nameof(Dig),RpcTarget.All,(int)DigMode.pickel, clickPos.x,clickPos.y);//123456
                    }
                    else if (isHummer)
                    {
                        photonView.RPC(nameof(Dig), RpcTarget.All,(int)DigMode.hummer, clickPos.x,clickPos.y);//123456
                    }
                    propertiesManager.PlayerCustomPropertiesSettings(translatedPanelCount, propertiesManager.panelListKey, PhotonNetwork.LocalPlayer);
                    propertiesManager.PlayerCustomPropertiesSettings(hp, propertiesManager.hpKey, PhotonNetwork.LocalPlayer);
                    
                    await UniTask.DelayFrame(1);
                    await CheckGetFossil(token);
                }
            }
            else if (result == 1)
            {
                Debug.Log(hp);

                if (hp <= 0 || generateFossilList.Count <= 0)
                {
                    break;
                }
            }
        }
        BackMainScene(token).Forget();

    }
    [PunRPC]
    private void Dig(int mode, float clickPosx, float clickPosy)
    {
        if (mode == (int)DigMode.pickel)
        {
            DecreaseHP(DigMode.pickel);
            for (int i = -1; i <= 1; i++)
            {
                if (clickPosx + i < 0 || 12 < clickPosx + i)
                {
                    continue;
                }
                ChangeCount(DigMode.pickel, (int)clickPosx + i, (int)clickPosy);
            }
            for (int i = -1; i <= 1; i++)
            {
                if (clickPosy + i < 0 || 9 < clickPosy + i)
                {
                    continue;
                }
                ChangeCount(DigMode.pickel, (int)clickPosx, (int)clickPosy + i);
            }
        }
        else if (mode == (int)DigMode.hummer)
        {
            DecreaseHP(DigMode.hummer);
            for (int i = -1; i <= 1; i++)
            {
                if (clickPosx + i < 0 || 12 < clickPosx + i)
                {
                    continue;
                }
                for (int j = -1; j <= 1; j++)
                {
                    if (clickPosy + j < 0 || 9 < clickPosy + j)
                    {
                        continue;
                    }
                    ChangeCount(DigMode.hummer, (int)clickPosx + i, (int)clickPosy + j);
                }
            }
        }
    }

    private void DecreaseHP(DigMode key)
    {
        if (key==DigMode.pickel)
        {
            hp--;
        }
        else if (key == DigMode.hummer)
        {
            hp -= 2;
        }

        hpBar.value = (float)hp / 30f;
    }

    private void ChangeCount(DigMode key, int x, int y)
    {
        int index = Count_v * x + y;
        if (key == DigMode.pickel)
        {
            if (0 <= index && index < 150)//???
            {
                panelCount[x, y]--;
                translatedPanelCount[Count_v * x + y]--;
            }
        }
        else if(key == DigMode.hummer)
        {
            if (0<=index&&index<150)//???
            {
                panelCount[x, y] -= 2;
                translatedPanelCount[Count_v * x + y] -= 2;
            }
            
        }

        if (panelCount[x,y] > 0)
        {
            panelSpriteRenderer[x,y].sprite = panelSprites[panelCount[x,y]];
        }
        else
        {
            panelSpriteRenderer[x, y].gameObject.SetActive(false);
        }
    }

    private async UniTask CheckGetFossil(CancellationToken token = default)
    {
        for (int i = 0; i < generateFossilList.Count; i++)
        {
            var halfFossilSize = generateFossilList[i].transform.localScale.x * 2;
            if (Physics.BoxCast(generateFossilList[i].transform.localPosition + panelParent.transform.position + Vector3.forward,
                new Vector3(halfFossilSize, halfFossilSize), Vector3.back, Quaternion.identity, 5.0f, panelLayermask))
            {
                continue;
            }
            else
            {
                getFossilList.Add(generateFossilList[i]);
                getFossilNumList.Add(generateFossilNumList[i]);
                generateFossilNumList.RemoveAt(i);
                generateFossilList.RemoveAt(i);
            }
            await UniTask.DelayFrame(1);
        }
    }

    public void OnPickelButton()
    {
        pickelButton.interactable = false;
        hummerButton.interactable = true;
        isHummer = false;
        isPickel = true;
    }

    public void OnHummerButton()
    {
        pickelButton.interactable = true;
        hummerButton.interactable = false;
        isHummer = true;
        isPickel = false;
    }

    public async UniTask BackMainScene(CancellationToken token = default)
    {
        if (first)
        {
            groupSettings.RemoveGroup(PhotonNetwork.LocalPlayer.ActorNumber);
        }
        getTextObj.SetActive(true);
        for(int i = 0; i < getFossilNumList.Count; i++)
        {
            getText.text=fossilInfo.FossilInfoDic[getFossilNumList[i]].itemName + "を手に入れた";
            having.GetFossil(fossilInfo.FossilInfoDic[getFossilNumList[i]].fossileSize, fossilInfo.FossilInfoDic[getFossilNumList[i]].fossilColor);
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: token);
        }

        if (getFossilNumList.Count == 0)
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: token);
        }
        
        gameManager.isDigScene = false;
        mainSceneCanvas.SetActive(true);
        digSceneCanvas.SetActive(false);
        playerCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }
}
