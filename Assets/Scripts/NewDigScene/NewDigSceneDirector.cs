using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System.Threading;

public class NewDigSceneDirector : MonoBehaviour
{
    public GameObject ResultTextController;
    public GameObject PanelGenerator;

    private Sprite[] BoardSprite = new Sprite[3];
    public GameObject[] boardSpritePrefab = new GameObject[3];
    private int[,] Board = new int[13, 10];
    private GameObject[,] BoardImage = new GameObject[13, 10];
    public SpriteRenderer[,] SpriteRenderer = new SpriteRenderer[13, 10];
    private int Strength = 0;
    private Vector3 mousePos;
    private Vector3 objPos;
    private int objPosX;
    private int objPosY;
    public Button HammerButton;
    public Button PickelButton;
    private Slider _slider;     //CountBar HP
    private float _hp = 0;
    private int[,] FossilLocation = new int[13, 10];
    private int FlocationX = 0;
    private int FlocationY = 0;
    private bool CheckOverlap = true; //重なりあるか否か
    public int[] MemorizeKey = new int[4];
    private Vector3[] MemorizeLocation = new Vector3[4];
    public HashSet<int> ExcavationCompletedhs = new HashSet<int>();
    //public Dictionary<int, FossilList> FossilDic;
    private int _count = 0; //Generator何回ループしているか、memorizeに入れる際使用
    public GameObject Fprefab;
    private int RandomGenerateNum;  //生成する化石の個数
    private int RandomKindNum;  //ディクショナリのキーの数字をランダムに生成
    public Text FossilKosu;
    private FossilInfo fossilInfo = new FossilInfo();
    private bool aaa = true;
    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        for (int i = 0; i < 3; i++)
        {
            BoardSprite[i] = boardSpritePrefab[i].GetComponent<SpriteRenderer>().sprite;
        }
        _slider = GameObject.Find("CountBar").GetComponent<Slider>();
    }

    private void Start()
    {
        GenerateBoard();
        IntializeArray();
        var token = this.GetCancellationTokenOnDestroy();
        FossilGenerator(token).Forget();
    }

    void Update()
    {
        if (_hp >= 30 && aaa)
        {
            DigResult();
            aaa = false;
            ResultTextController.SetActive(true);
            Debug.Log("FINISH!!!");
        }

        if (Input.GetMouseButtonDown(0) && _hp < 30 && ExcavationCompletedhs.Count != RandomGenerateNum)//且つすべての化石が掘り出されていないとき
        {
            SearchMousePosition();

            if (objPos.y > -0.5 && objPos.y < 9.5 && objPos.x > -0.5 && objPos.x < 12.5)
            {
                objPosX = (int)Mathf.Round(objPos.x);
                objPosY = (int)Mathf.Round(objPos.y);

                if (HammerButton.interactable == false)
                {
                    HammerMode();
                }
                else if (PickelButton.interactable == false)
                {
                    PickelMode();
                }
                Debug.Log(ExcavationCompletedhs.Count);
                DigResult();
                if (ExcavationCompletedhs.Count == RandomGenerateNum)
                {
                    Debug.Log("Completed!!");
                    ResultTextController.SetActive(true);
                }
            }
        }
    }

    private void GenerateBoard()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                BoardImage[i, j] = Instantiate(boardSpritePrefab[0], new Vector3(i, j, 0), Quaternion.identity);
                BoardImage[i, j].gameObject.transform.SetParent(PanelGenerator.transform);
                BoardImage[i, j].gameObject.transform.localPosition = new Vector3(i, j, 0);
                BoardImage[i, j].gameObject.transform.localScale = new Vector3(1, 1, 1);
                SpriteRenderer[i, j] = BoardImage[i, j].GetComponent<SpriteRenderer>();
            }
        }
    }

    private void IntializeArray()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Strength = Random.Range(0, 3);
                Board[i, j] = Strength;
                //BoardImage[i,j] = Instantiate(BoardSprite[Strength], new Vector3(i, j, 0), Quaternion.identity);//マスの生成
                //BoardImage[i, j].gameObject.transform.SetParent(PanelGenerator.transform);
                //BoardImage[i, j].gameObject.transform.localPosition = new Vector3(i, j, 0);
                //BoardImage[i, j].gameObject.transform.localScale = new Vector3(1, 1, 1);
                //SpriteRenderer[i, j] = BoardImage[i, j].GetComponent<SpriteRenderer>();
                SpriteRenderer[i, j].sprite = BoardSprite[Strength];
            }
        }
    }

    private async UniTask FossilGenerator(CancellationToken token = default)
    {
        RandomGenerateNum = Random.Range(2, 5);
        FossilKosu.text = RandomGenerateNum + "コ";
        Debug.Log("個数＝" + RandomGenerateNum);
        
        Vector3 Flocation = new Vector3(0, 0, 0);
        for(int i = 0; i < RandomGenerateNum; i++)
        {
            //RandomKindNum =0;
            RandomKindNum = Random.Range(0, 12/*13*/);
            MemorizeKey[_count] = RandomKindNum;
            
            //Debug.Log(FossilDic[RandomKindNum].Fname + RandomKindNum);

            //↓↓↓下記のwhileは大きなバグの原因含んでいるので注意！！終わらない可能性あり
            while (true)
            {
                if (RandomKindNum <= 3)
                {
                    Flocation = new Vector3(Random.Range(0, 11) + 0.5f, Random.Range(0, 9) + 0.5f, 0);//基準左下
                }
                else if (RandomKindNum <= 7)
                {
                    Flocation = new Vector3(Random.Range(1, 11), Random.Range(1, 8), 0);
                }
                else if (RandomKindNum <= 11)
                {
                    Flocation = new Vector3(Random.Range(1, 10) + 0.5f, Random.Range(1, 7) + 0.5f, 0);//基準（２，２）
                }
                //Debug.Log("切り捨て" + Mathf.Floor(Flocation.x));
                FlocationX = (int)Mathf.Floor(Flocation.x); //Flocationのxの小数第一位を切り捨てたもの　配列の値に使う
                FlocationY = (int)Mathf.Floor(Flocation.y); //Flocationのyの小数第一位を切り捨てたもの　配列の値に使う

                await CheckLocation(RandomKindNum, FlocationX, FlocationY);
                if (CheckOverlap) break;
            }
            MemorizeLocation[_count] = new Vector3(FlocationX,FlocationY,0);
            _count++;

            FossilGeneratorSend(Flocation);
            FillFosssilPos();
            
            //123456
            //photonView.RPC(nameof(FossilGeneratorSend,));

        }
        for (int i=0;i<_count ;i++)
        {
            //Debug.Log("MemorizeLocation[i]" + MemorizeLocation[i]+":"+ MemorizeKey[i]);
        }
    }

    //事前に化石が置かれているか精査
    private IEnumerator CheckLocation(int RandomKindNum, int FlocationX, int FlocationY)
    {
        //Debug.Log("Flocation"+ FlocationX+":"+ FlocationY+":"+ RandomKindNum);
        for (int k = 0; k < (int)fossilInfo.FossilInfoDic[RandomKindNum].fossileSize + 2; k++)
        {
            for (int j = 0; j < (int)fossilInfo.FossilInfoDic[RandomKindNum].fossileSize + 2; j++)
            {
                if (RandomKindNum <= 3)
                {
                    if (FossilLocation[k + FlocationX, j + FlocationY] == 1)
                    {
                        CheckOverlap = false;
                        yield break;
                    }
                }
                else if (RandomKindNum <= 7)
                {
                    if (FossilLocation[k + FlocationX - 1, j + FlocationY - 1] == 1)
                    {
                        CheckOverlap = false;
                        yield break;
                    }
                }
                else if (RandomKindNum <= 11)
                {
                    if (FossilLocation[k + FlocationX - 1, j + FlocationY - 1] == 1)
                    {
                        CheckOverlap = false;
                        yield break;
                    }
                }
            }
        }
        CheckOverlap = true;
    }

    [PunRPC]
    public void FossilGeneratorSend(Vector3 FlocationS)
    {
        var tmp = Instantiate(Fprefab, FlocationS, Quaternion.identity);
        tmp.gameObject.transform.SetParent(PanelGenerator.transform);
        tmp.gameObject.transform.localPosition = FlocationS;
        Addressables.LoadAssetAsync<Sprite>(fossilInfo.FossilInfoDic[(int)RandomKindNum].prefabAddress).Completed += handle =>
        {
            // ロードに成功した場合の処理をここに
            tmp.GetComponent<SpriteRenderer>().sprite = handle.Result;
        };
        float foo = 0.25f * ((int)fossilInfo.FossilInfoDic[(int)RandomKindNum].fossileSize + 2);   //正方形化石のscale変えるため
        tmp.transform.localScale = new Vector3(foo, foo, 1);
    }

    public void FillFosssilPos()
    {
        int testX;
        int testY;
        for (int k = 0; k < (int)fossilInfo.FossilInfoDic[RandomKindNum].fossileSize + 2; k++)
        {
            for (int j = 0; j < (int)fossilInfo.FossilInfoDic[RandomKindNum].fossileSize + 2; j++)
            {
                if (RandomKindNum <= 3)
                {
                    FossilLocation[k + FlocationX, j + FlocationY] = 1;
                    testX = k + FlocationX;
                    testY = j + FlocationY;
                    //Debug.Log("RandomKindNum"+RandomKindNum + "(" + testX + ","+ testY +")");
                }
                else if (RandomKindNum <= 7)
                {
                    testX = k + FlocationX - 1;
                    testY = j + FlocationY - 1;
                    FossilLocation[k + FlocationX - 1, j + FlocationY - 1] = 1;
                    //Debug.Log("RandomKindNum"+RandomKindNum + "(" + testX + "," + testY + ")");
                }
                else if (RandomKindNum <= 11)
                {
                    testX = k + FlocationX - 1;
                    testY = j + FlocationY - 1;
                    FossilLocation[k + FlocationX - 1, j + FlocationY - 1] = 1;
                    //Debug.Log("RandomKindNum" + RandomKindNum+"(" + testX + "," + testY + ")");
                }
            }
        }
    }

    //化石が掘れたか判定、すべて掘れた時。。。
    private void DigResult()
    {
        for(int i = 0; i < RandomGenerateNum; i++)
        {
            CompareArray(i);
            //Debug.Log(SuccessCNT + "," + ExcavationCompleted[SuccessCNT]);
        }
        
        //生成された化石のキーと生成場所をそれぞれ配列で保存
        //それぞれの配列０番目から見ていく
        //一回タップするごとに呼ばれる　または　壁ｈｐが30以上になったら呼ばれる
    }

    

    private void CompareArray(int i)
    {
        //Debug.Log("CompareAray");
        for (int k = 0; k < (int)fossilInfo.FossilInfoDic[MemorizeKey[i]].fossileSize+2; k++)
        {      
            for (int j = 0; j < (int)fossilInfo.FossilInfoDic[MemorizeKey[i]].fossileSize + 2; j++)
            {
                if (MemorizeKey[i] <= 3)
                {
                    if (Board[k + (int)MemorizeLocation[i].x, j + (int)MemorizeLocation[i].y] >= 0)
                    {
                        return;
                    }
                }
                else if (MemorizeKey[i] <= 7)
                {
                    if (Board[k + (int)MemorizeLocation[i].x - 1, j + (int)MemorizeLocation[i].y - 1] >= 0)
                    {
                        return;
                    }
                }
                else if (MemorizeKey[i] <= 11)
                {
                    if (Board[k + (int)MemorizeLocation[i].x - 1, j + (int)MemorizeLocation[i].y - 1] >= 0)
                    {
                        return;
                    }
                }
            }
        }
        ExcavationCompletedhs.Add(i);
        //Debug.Log("MemorizeKey[i]"+ MemorizeKey[i]);
        //Debug.Log("ExcavationCompletedhs.Count" + ExcavationCompletedhs.Count);
    }

    private void SearchMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10.0f;                   //役割何？
        objPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Debug.Log(objPos);
    }

    public void HammerORPickelButton()
    {
        if (HammerButton.interactable == false)
        {
            HammerButton.interactable = true;
            PickelButton.interactable = false;
        }
        else if (PickelButton.interactable == false)
        {
            
            PickelButton.interactable = true;
            HammerButton.interactable = false;
        }
    }
    
    public void HammerMode()
    {
        for(int i = -1; i <= 1; i++)
        {
            if (objPosX + i == -1 || objPosX + i == 13)
            {
                continue;
            }
            for(int j=-1;j<= 1; j++)
            {
                if(objPosY + j == -1 || objPosY + j == 10)
                {
                    continue;
                }
                //123456
                HammerModeSend(i,j);
            }
        }
        _hp += 2;
        // HPゲージに値を設定
        _slider.value = _hp;
    }

    public void HammerModeSend(int i,int j)
    {
        Board[objPosX + i, objPosY + j] -= 2;
        if (Board[objPosX + i, objPosY + j] > -1)
        {
            SpriteRenderer[objPosX + i, objPosY + j].sprite = BoardSprite[Board[objPosX + i, objPosY + j]];
        }
        else
        {
            BoardImage[objPosX + i, objPosY + j].SetActive(false);
        }
    }

    public void PickelMode()
    {
        for (int j = -1; j <= 1; j++)
        {
            if (objPosY + j == -1 || objPosY + j == 10)
            {
                continue;
            }
            //123456
            PickelMode1Send(j);
        }
        for (int j = -1; j <= 1; j++)
        {
            if (objPosX + j == -1 || objPosX + j == 13)
            {
                continue;
            }
            //123456
            PickelMode2Send(j);
        }
        _hp++;
        // HPゲージに値を設定
        _slider.value = _hp;

        //試行回数hp
    }

    [PunRPC]
    public void PickelMode1Send(int j)
    {
        Board[objPosX, objPosY + j]--;
        if (Board[objPosX, objPosY + j] > -1)
        {
            SpriteRenderer[objPosX, objPosY + j].sprite = BoardSprite[Board[objPosX, objPosY + j]];
        }
        else
        {
            BoardImage[objPosX, objPosY + j].SetActive(false);
        }
    }

    [PunRPC]
    public void PickelMode2Send(int j)
    {
        Board[objPosX + j, objPosY]--;
        if (Board[objPosX + j, objPosY] > -1)
        {
            SpriteRenderer[objPosX + j, objPosY].sprite = BoardSprite[Board[objPosX + j, objPosY]];
        }
        else
        {
            BoardImage[objPosX + j, objPosY].SetActive(false);
        }
    }
}
