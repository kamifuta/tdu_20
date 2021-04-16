using Cysharp.Threading.Tasks;
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
    public Camera mainCamera;
    public Camera playerCamera;
    public GameObject panelParent;
    public GameObject panelPrefab;
    public GameObject fossilPrefab;
    public GameObject clickTrigger;
    public Button pickelButton;
    public Button hummerButton;
    public Sprite[] panelSprites = new Sprite[4];
    public Text fossilCountText;
    public int[,] panelCount = new int[Count_h, Count_v];

    private FossilInfo fossilInfo = new FossilInfo();
    private SpriteRenderer[,] panelSpriteRenderer = new SpriteRenderer[Count_h, Count_v];
    private List<int> generateFossilNumList = new List<int>();
    private List<int> getFossilNumList = new List<int>();
    private List<GameObject> generateFossilList = new List<GameObject>();
    private List<GameObject> getFossilList = new List<GameObject>();
    private List<GameObject> panelsList = new List<GameObject>();
    private List<GameObject> triggersList = new List<GameObject>();
    private Vector3 generatePos;
    private Vector3 clickPos;
    private int tryCount=0;
    private int hp = 30;
    private bool isPickel = false;
    private bool isHummer = true;
    private bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager.ObserveEveryValueChanged(x => x.isDigScene)
            .Where(x => x == true)
            .Subscribe(_ =>
            {
                var token = this.GetCancellationTokenOnDestroy();
                Initialization(token).Forget();
            })
            .AddTo(this);
    }

    private async UniTask Initialization(CancellationToken token = default)
    {
        hp = 30;
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
        await GeneratePanels(token);
        
        while (generateFossilList.Count < 2)
        {
            await GenerateFossil(token);
        }
        fossilCountText.text = generateFossilList.Count + "個";

        ClickAsync(token).Forget();
        first = false;
    }

    public async UniTask GeneratePanels(CancellationToken token = default)
    {
        for(int i = 0; i < Count_h; i++)
        {
            for(int j = 0; j < Count_v; j++)
            {
                int count = Random.Range(1, 4);
                panelCount[i, j] = count;
                if (first)
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
                foreach(var a in panelsList)
                {
                    a.SetActive(true);
                }
                panelSpriteRenderer[i, j].sprite = panelSprites[count];
                await UniTask.DelayFrame(1);
            }
        }
    }

    public async UniTask GenerateFossil(CancellationToken token = default)
    {
        int fossilCount = Random.Range(3, maxFossilCount);
        for(int i = 0; i < fossilCount; i++)
        {
            tryCount = 0;
            int fossilNum = Random.Range(0, fossilInfo.FossilInfoDic.Count);
            
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
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: token);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, 100f, clickTriggerLayermask))
            {
                clickPos = hit.collider.transform.localPosition;
                await Dig(token);
                await CheckGetFossil(token);
            }
            else
            {
                continue;
            }

            Debug.Log(hp);

            if (hp <= 0 || generateFossilList.Count <= 0)
            {
                break;
            }
        }
        BackMainScene();

    }

    private async UniTask Dig(CancellationToken token = default)
    {
        DecreaseHP();

        if (isPickel)
        {
            for (int i = -1; i <= 1; i++)
            {
                if (clickPos.x + i < 0 || 12 < clickPos.x + i)
                {
                    continue;
                }
                await ChangeCount((int)clickPos.x + i, (int)clickPos.y, token);
            }
            for (int i = -1; i <= 1; i++)
            {
                if (clickPos.y + i < 0 || 9 < clickPos.y + i)
                {
                    continue;
                }
                await ChangeCount((int)clickPos.x, (int)clickPos.y + i, token);
            }
        }
        else if (isHummer)
        {
            for (int i = -1; i <= 1; i++)
            {
                if (clickPos.x + i < 0 || 12 < clickPos.x + i)
                {
                    continue;
                }
                for (int j = -1; j <= 1; j++)
                {
                    if (clickPos.y + j < 0 || 9 < clickPos.y + j)
                    {
                        continue;
                    }
                    await ChangeCount((int)clickPos.x + i, (int)clickPos.y + j, token);
                }
            }
        }
    }

    private void DecreaseHP()
    {
        if (isPickel)
        {
            hp--;
        }
        else if (isHummer)
        {
            hp -= 2;
        }
    }

    private async UniTask ChangeCount(int x, int y, CancellationToken token = default)
    {
        if (isPickel)
        {
            panelCount[x, y]--;
        }
        else if(isHummer)
        {
            panelCount[x, y] -= 2;
        }

        if (panelCount[x,y] > 0)
        {
            panelSpriteRenderer[x,y].sprite = panelSprites[panelCount[x,y]];
        }
        else
        {
            //panelSpriteRenderer[x, y].sprite = null;
            panelSpriteRenderer[x, y].gameObject.SetActive(false);
        }

        await UniTask.DelayFrame(1);
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
                //Debug.Log(fossilInfo.FossilInfoDic[generateFossilNumList[i]].itemName + "を手に入れた");
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

    public void BackMainScene()
    {
        for(int i = 0; i < getFossilNumList.Count; i++)
        {
            Debug.Log(fossilInfo.FossilInfoDic[getFossilNumList[i]].itemName + "を手に入れた");
        }
        gameManager.isDigScene = false;
        playerCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }
}
