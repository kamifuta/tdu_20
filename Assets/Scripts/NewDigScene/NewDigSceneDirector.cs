using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewDigSceneDirector : MonoBehaviour
{
    public GameObject _Canvas;
    public GameObject[] BoardSprite = new GameObject[3];
    public Sprite[] _BoardSprites = new Sprite[3];
    private int[,] Board = new int[13, 10];
    //private int [,] FossilLocation = new int[13,10];
    private GameObject[,] BoardImage = new GameObject[13, 10];
    public SpriteRenderer[,] SpriteRenderer = new SpriteRenderer[13, 10];
    private int Strength = 0;
    private Vector3 mousePos;
    private Vector3 objPos;
    private int objPosX;
    private int objPosY;
    public Button HammerButton;
    public Button PickelButton;
    Slider _slider;     //CountBar HP
    float _hp = 0;
    //public bool HammerCheck = true;
    //public bool PickelCheck = true;
    private int[,] FossilLocation = new int[13, 10];
    int FlocationX = 0;
    int FlocationY = 0;
    bool CheckOverlap = true; //重なりあるか否か
    int[] MemorizeKey = new int[4];
    Vector3[] MemorizeLocation = new Vector3[4];
    int[] ExcavationCompleted = new int[5]; //SuccessCNT++で一つ多くても参照不可にならないように+1
    Dictionary<int, FossilList> FossilDic;
    private int _count = 0; //Generator何回ループしているか、memorizeに入れる際使用
    public GameObject Fprefab;
    int SuccessCNT = 0; //発掘成功した化石のキー配列（ExcavationCompleted[]）回すため
    int RandomGenerateNum;  //生成する化石の個数

    private IEnumerator FossilGenerator()
    {
        RandomGenerateNum = Random.Range(2, 5);
        //Debug.Log("個数＝" + RandomGenerateNum);
        int RandomKindNum;  //ディクショナリのキーの数字をランダムに生成
        Vector3 Flocation = new Vector3(0, 0, 0);
        for(int i = 0; i < RandomGenerateNum; i++)
        {
            RandomKindNum = Random.Range(0, 12/*13*/);
            MemorizeKey[_count] = RandomKindNum;
            
            //Debug.Log(FossilDic[RandomKindNum].Fname + RandomKindNum);

            //↓下記のwhileは大きなバグの原因含んでいるので注意！！終わらない可能性あり
            while (true)
            {
                if (RandomKindNum <= 3)
                {
                    Flocation = new Vector3(Random.Range(0, 11) + 0.5f, Random.Range(0, 9) + 0.5f, 0);
                }
                else if (RandomKindNum <= 7)
                {
                    Flocation = new Vector3(Random.Range(1, 11), Random.Range(1, 8), 0);
                }
                else if (RandomKindNum <= 11)
                {
                    Flocation = new Vector3(Random.Range(1, 10) + 0.5f, Random.Range(1, 7) + 0.5f, 0);
                }
                //Debug.Log("切り捨て" + Mathf.Floor(Flocation.x));
                FlocationX = (int)Mathf.Floor(Flocation.x); //Flocationのxの小数第一位を切り捨てたもの　配列の値に使う
                FlocationY = (int)Mathf.Floor(Flocation.y); //Flocationのyの小数第一位を切り捨てたもの　配列の値に使う

                yield return StartCoroutine(CheckLocation(RandomKindNum, FlocationX, FlocationY));
                if (CheckOverlap) break;
            }
            MemorizeLocation[_count] = new Vector3(FlocationX,FlocationY,0);//FlocateionXで管理したほうがいい？？？
            _count++;

            int testX = 0;
            int testY = 0;

            for (int k = 0; k < FossilDic[RandomKindNum].Fsize.GetLength(0); k++)
            {
                for (int j = 0; j < FossilDic[RandomKindNum].Fsize.GetLength(0); j++)
                {
                    if (RandomKindNum <= 3)
                    {
                        FossilLocation[k + FlocationX, j + FlocationY] = 1;
                        testX = k + FlocationX;
                        testY = j + FlocationY;
                        Debug.Log(RandomKindNum+"("+testX+","+testY+")");
                    }
                    else if (RandomKindNum <= 7)
                    {
                        FossilLocation[k + FlocationX - 1, j + FlocationY -1] = 1;
                        testX = k + FlocationX-1;
                        testY = j + FlocationY-1;
                        Debug.Log(RandomKindNum + "(" + testX + "," + testY + ")");
                    }
                    else if (RandomKindNum <= 11)
                    {
                        FossilLocation[k + FlocationX - 1, j + FlocationY - 1] = 1;
                        testX = k + FlocationX-1;
                        testY = j + FlocationY-1;
                        Debug.Log(RandomKindNum + "(" + testX + "," + testY + ")");
                    }
                }
            }
            var tmp = Instantiate(Fprefab,Flocation,Quaternion.identity);
            tmp.gameObject.transform.SetParent(_Canvas.transform);
            tmp.gameObject.transform.localPosition = Flocation;
            
            tmp.GetComponent<SpriteRenderer>().sprite = FossilDic[RandomKindNum].Fsprite;

            float foo = 0.25f * FossilDic[RandomKindNum].Fsize.GetLength(0);  //正方形化石のscale変えるため
            tmp.transform.localScale = new Vector3(foo,foo,1);

        }

        //Generateするのに必要なこと
        //todo:重ならないように　 →　完了
        //todo:生成する場所の制限　→　完了
        //todo:配列の連携　化石掘れたかどうか
        //todo:生成するオブジェクトの種類の比率
        //todo:各エフェクト
        //todo:終わった後の処理
        //todo:ハッシュセット　タップごとに全部掘れたか判定
        //todo:ローカルポジション
    }

    //事前に化石が置かれているか精査
    private IEnumerator CheckLocation(int RandomKindNum,int FlocationX,int FlocationY)
    {
        for(int k = 0; k < FossilDic[RandomKindNum].Fsize.GetLength(0); k++)
        {
            for (int j = 0; j < FossilDic[RandomKindNum].Fsize.GetLength(0); j++)
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
                    if (FossilLocation[k + FlocationX-1, j + FlocationY-1] == 1)
                    {
                        CheckOverlap = false;
                        yield break;
                    }
                }
                else if (RandomKindNum <= 11)
                {
                    if (FossilLocation[k + FlocationX-1, j + FlocationY-1] == 1)
                    {
                        CheckOverlap = false;
                        yield break;
                    }
                }
            }
        }
        CheckOverlap =  true;
    }
    private void DigResult()
    {
        for(int i = 0; i < RandomGenerateNum; i++)
        {
            CompareArray(i);
            //Debug.Log(SuccessCNT + "," + ExcavationCompleted[SuccessCNT]);
            //ここまできたらbool型配列ｉ番目をtrueにするとか？
        }
        //生成された化石のキーと生成場所をそれぞれ配列で保存
        //それぞれの配列０番目から見ていく
        //一回タップするごとに呼ばれる　または　壁ｈｐが30以上になったら呼ばれる
    }

    private void CompareArray(int i)
    {
        for (int k = 0; k < FossilDic[MemorizeKey[i]].Fsize.GetLength(0); k++)
        {
            for (int j = 0; j < FossilDic[MemorizeKey[i]].Fsize.GetLength(0); j++)
            {
                if (MemorizeKey[i] <= 3)
                {
                    if (Board[k + (int)MemorizeLocation[i].x, j + (int)MemorizeLocation[i].y] > 0)
                    {
                        //Debug.Log("<=3"+k + (int)MemorizeLocation[i].x+","+j + (int)MemorizeLocation[i].y);
                        return;
                    }
                }
                else if (MemorizeKey[i] <= 7)
                {
                    if (Board[k + (int)MemorizeLocation[i].x - 1, j + (int)MemorizeLocation[i].y - 1] > 0)
                    {
                        //Debug.Log("<=7" + k + (int)MemorizeLocation[i].x + "," + j + (int)MemorizeLocation[i].y);
                        return;
                    }
                }
                else if (MemorizeKey[i] <= 11)
                {
                    if (Board[k + (int)MemorizeLocation[i].x - 1, j + (int)MemorizeLocation[i].y - 1] > 0)
                    {
                        //Debug.Log("<=11" + k + (int)MemorizeLocation[i].x + "," + j + (int)MemorizeLocation[i].y);
                        return;
                    }
                }
            }
        }
        ExcavationCompleted[SuccessCNT] = MemorizeKey[i];
        Debug.Log("CNT&Key" + SuccessCNT + "," + ExcavationCompleted[SuccessCNT]);
        SuccessCNT++;
        
    }

    private void IntializeArray()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Strength = Random.Range(0, 3);
                Board[i, j] = Strength;
                BoardImage[i,j] = Instantiate(BoardSprite[Strength], new Vector3(i, j, 0), Quaternion.identity);//マスの生成
                BoardImage[i, j].gameObject.transform.SetParent(_Canvas.transform);
                BoardImage[i, j].gameObject.transform.localPosition = new Vector3(i, j, 0);
                BoardImage[i, j].gameObject.transform.localScale = new Vector3(1, 1, 1);
                SpriteRenderer[i, j] = BoardImage[i, j].GetComponent<SpriteRenderer>();

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IntializeArray();
        _slider = GameObject.Find("CountBar").GetComponent<Slider>();
        FossilDic = new Dictionary<int, FossilList>()
        {
            {0,new FossilList("小さい　赤色の　宝石",Resources.Load<Sprite>("RedGem"),new int[2,2])},
            {1,new FossilList("小さい　青色の　宝石",Resources.Load<Sprite>("BlueGem"),new int[2,2])},
            {2,new FossilList("小さい　緑色の　宝石",Resources.Load<Sprite>("GreenGem"),new int[2,2])},
            {3,new FossilList("小さい　黄色の　宝石",Resources.Load<Sprite>("YellowGem"),new int[2,2])},
            {4,new FossilList("大きい　赤色の　宝石",Resources.Load<Sprite>("RedGem"),new int[3,3])},
            {5,new FossilList("大きい　青色の　宝石",Resources.Load<Sprite>("BlueGem"),new int[3,3])},
            {6,new FossilList("大きい　緑色の　宝石",Resources.Load<Sprite>("GreenGem"),new int[3,3])},
            {7,new FossilList("大きい　黄色の　宝石",Resources.Load<Sprite>("YellowGem"),new int[3,3])},
            {8,new FossilList("特大の　赤色の　宝石",Resources.Load<Sprite>("RedGem"),new int[4,4])},
            {9,new FossilList("特大の　青色の　宝石",Resources.Load<Sprite>("BlueGem"),new int[4,4])},
            {10,new FossilList("特大の　緑色の　宝石",Resources.Load<Sprite>("GreenGem"),new int[4,4])},
            {11,new FossilList("特大の　黄色の　宝石",Resources.Load<Sprite>("YellowGem"),new int[4,4])},
            {12,new FossilList("珍しいコハク",Resources.Load<Sprite>("SampleGem"),new int[6,8])},
        };

        StartCoroutine(FossilGenerator());


    }
    bool aaa = true;
    // Update is called once per frame
    void Update()
    {
        if(_hp >= 30 && aaa)
        {
            DigResult();
            aaa = false;
            //Debug.Log("FINISH!!!");
        }
        if (Input.GetMouseButtonDown(0) && _hp < 30)//且つすべての化石が掘り出されていないとき
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
                
            }
        }
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
                //Destroy(BoardImage[objPosX + i, objPosY + j]);
                Board[objPosX + i, objPosY + j] -= 2;
                if (Board[objPosX+i, objPosY+j] > -1)
                {
                    //BoardImage[objPosX + i, objPosY +j] = Instantiate(BoardSprite[Board[objPosX + i, objPosY + j]], new Vector3(objPosX + i, objPosY + j, 0), Quaternion.identity);
                    SpriteRenderer[objPosX + i, objPosY + j].sprite = _BoardSprites[Board[objPosX + i, objPosY + j]];
                }
                else
                {
                    Destroy(BoardImage[objPosX + i, objPosY + j]);
                }
            }
        }
        _hp += 2;
        // HPゲージに値を設定
        _slider.value = _hp;
    }
    public void PickelMode()
    {
        for (int j = -1; j <= 1; j++)
        {
            if (objPosY + j == -1 || objPosY + j == 10)
            {
                continue;
            }
            //Destroy(BoardImage[objPosX, objPosY + j]);
            Board[objPosX, objPosY + j]--;
            if (Board[objPosX , objPosY + j] > -1)
            {
                //BoardImage[objPosX, objPosY + j] = Instantiate(BoardSprite[Board[objPosX, objPosY + j]], new Vector3(objPosX, objPosY + j, 0), Quaternion.identity);
                SpriteRenderer[objPosX, objPosY + j].sprite = _BoardSprites[Board[objPosX, objPosY + j]];
            }
            else
            {
                Destroy(BoardImage[objPosX, objPosY + j]);
            }
        }
        for (int j = -1; j <= 1; j++)
        {
            if (objPosX + j == -1 || objPosX + j == 13)
            {
                continue;
            }
            //Destroy(BoardImage[objPosX + j, objPosY]);
            Board[objPosX + j, objPosY]--;
            if (Board[objPosX + j, objPosY ] > -1)
            {
                //BoardImage[objPosX + j, objPosY] = Instantiate(BoardSprite[Board[objPosX + j, objPosY ]], new Vector3(objPosX + j, objPosY, 0), Quaternion.identity);
                SpriteRenderer[objPosX + j, objPosY].sprite = _BoardSprites[Board[objPosX + j, objPosY]];
            }
            else
            {
                Destroy(BoardImage[objPosX + j, objPosY]);
            }
        }
        _hp++;
        // HPゲージに値を設定
        _slider.value = _hp;

        //試行回数hp
    }
}
