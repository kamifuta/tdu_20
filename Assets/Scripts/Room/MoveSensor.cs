using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSensor : MonoBehaviour
{
    Transform sensorObjPos;
    MoveObj moveObj;
    [SerializeField]Animator sensorAnimator=null;

    private List<GameObject> selectObjList=new List<GameObject>();

    private List<float> objRendererList = new List<float>();
    //private int enterRendererCount=0;
    private int exitRendererCount=0;
    private int exitObjCount=0;
    private float clickposX;
    private float clickposZ;


    private void Awake()
    {
        sensorObjPos = gameObject.GetComponent<Transform>();
        gameObject.SetActive(false);
    }

    /*private void OnEnable()
    {
        //アニメ―ション開始（AnimationAwake切る）
    }*/
    private void OnDisable()
    {
        selectObjList.Clear();
        objRendererList.Clear();
        exitRendererCount = 0;
        exitObjCount = 0;
        Debug.Log("exitRendererCount" + exitRendererCount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            sensorObjPos.position += new Vector3(0.0f, 0.0f, 1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            sensorObjPos.position += new Vector3(0.0f, 0.0f, -1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            sensorObjPos.position += new Vector3(-1.5f, 0.0f, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            sensorObjPos.position += new Vector3(1.5f, 0.0f, 0.0f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ClickFloor(ray);
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selectObjList.Count-1== exitObjCount) //nullじゃなかったら
            {
                Debug.Log("オブジェクト移動開始!!!!!!!");
                moveObj=selectObjList[exitObjCount].AddComponent<MoveObj>();
                moveObj.MoveObjSet(gameObject);
                selectObjList[exitObjCount].transform.position += new Vector3(0.0f,1.0f,0.0f);
                OnTriggerExit(selectObjList[exitObjCount].GetComponent<Collider>());
                gameObject.SetActive(false);
            }
            else if(selectObjList.Count - 1 < exitObjCount)
            {
                gameObject.SetActive(false);
                Debug.Log("オブジェクト配置開始!!!!!!!");

                //別スクリプトでオブジェクトの情報引っ張ってきて直でMoveObjに渡す
                /* moveObjObj.SetActive(true);
                moveObj.MoveObjSet("置くオブジェクト");
                selectObjList."置くオブジェクト".transform.position += new Vector3(0.0f, 1.0f, 0.0f);*/

            }
            else if (selectObjList.Count - 1 > exitObjCount)
            {
                Debug.Log("2個以上選択しています。");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            sensorAnimator.SetTrigger("FinishSensor");
            gameObject.SetActive(false);
        }


    }

    private void ClickFloor(Ray ray)
    {
        RaycastHit[] hit = Physics.RaycastAll(ray.origin, ray.direction);
        int hitlen = hit.Length;
        Debug.Log("hit2Dlen:" + hitlen);

        for (int i = 0; i < hitlen; i++)
        {
            Debug.Log("hit2d:" + hit[i].transform.gameObject);
            if (hit[i].transform != null && hit[i].transform.gameObject.CompareTag("Floor"))
            {
                Debug.Log("!RoomObj,hit2d:" + hit[i].transform.gameObject);

                if (Physics.Raycast(ray, out hit[i]))
                {
                    Debug.Log("hit[i].point" + hit[i].point);
                    clickposX = Mathf.RoundToInt(hit[i].point.x / 1.5f) * 1.5f;//clickposxはfloat(.5にはなる)
                    clickposZ = Mathf.RoundToInt(hit[i].point.z / 1.5f) * 1.5f;
                    Debug.Log("clickposX:" + clickposX);
                    Debug.Log("clickposY:" + clickposZ);

                    sensorObjPos.position = new Vector3(clickposX, sensorObjPos.position.y, clickposZ);
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        sensorAnimator.SetTrigger("TouchObj");

        Debug.Log("EnterJewel");
        var childRenderer = other.gameObject.GetComponentsInChildren<Renderer>();
        foreach (var colorchangeRenderer in childRenderer)
        {
            objRendererList.Add(colorchangeRenderer.material.color.a);
            ObjColorChange(colorchangeRenderer, 80f / 255f);
            Debug.Log("childRenderer[i]" + colorchangeRenderer);
        }
        selectObjList.Add(other.gameObject);
      
        Debug.Log("other.gameObject");
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("selectObjList[exitObjCount] ="+ other);
        sensorAnimator.SetTrigger("IdleObj");

        Debug.Log("ExitJewel");
        var childRenderer = other.gameObject.GetComponentsInChildren<Renderer>();//とりあえず子オブジェクトまで（孫は書いてない）
        foreach (var colorchangeRenderer in childRenderer)
        {
            ObjColorChange(colorchangeRenderer, objRendererList[exitRendererCount]);
            exitRendererCount++;
            Debug.Log("colorchangeRenderer" + colorchangeRenderer);
        }

        selectObjList[exitObjCount] = null;
        exitObjCount++;
    }

    private void ObjColorChange(Renderer colorChangeRenderer,float alphaColor)
    {
        Color colorChangeObjColor = colorChangeRenderer.material.color;
        colorChangeObjColor.a = alphaColor;
        colorChangeRenderer.material.color = colorChangeObjColor; 
    }
}

