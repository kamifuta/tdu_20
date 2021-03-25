using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSensor : MonoBehaviour
{
    Transform sensorObjPos;
    [SerializeField]Animator sensorAnimator=null;
    [System.NonSerialized] public GameObject selectObj;

    private List<float> objRendererList = new List<float>();
    //private int enterRendererCount=0;
    private int exitRendererCount=0;
    private float clickposX;
    private float clickposZ;


    private void Awake()
    {
        sensorObjPos = gameObject.GetComponent<Transform>();
    }

    private void OnDisable()
    {
        objRendererList.Clear();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            sensorObjPos.position += new Vector3(0.0f, 0.0f, 1.5f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            sensorObjPos.position += new Vector3(0.0f, 0.0f, -1.5f);
        }
        if (Input.GetKeyDown(KeyCode.A))
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
                //sensorObjPos.position 

                if (Physics.Raycast(ray, out hit[i]))
                {
                    Debug.Log("hit[i].point" + hit[i].point);
                    clickposX = ((int)(hit[i].point.x / 1.5)) * 1.5f;
                    clickposZ = ((int)(hit[i].point.z / 1.5)) * 1.5f;
                    Debug.Log("clickposX:" + clickposX);
                    Debug.Log("clickposY:" + clickposZ);

                    sensorObjPos.position = new Vector3(clickposX, 0.1f, clickposZ);

                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        sensorAnimator.SetTrigger("TouchObj");

        Debug.Log("EnterJewel");
        var childRenderer = other.gameObject.GetComponentsInChildren<Renderer>();
        int childRendererLen = childRenderer.Length;
        foreach (var colorchangeRenderer in childRenderer)
        {
            objRendererList.Add(colorchangeRenderer.material.color.a);
            ObjColorChange(colorchangeRenderer, 80f / 255f);
            Debug.Log("childRenderer[i]" + colorchangeRenderer);
        }

        selectObj = other.gameObject;
    }


    private void OnTriggerExit(Collider other)
    {
        sensorAnimator.SetTrigger("IdleObj");

        Debug.Log("ExitJewel");
        var childRenderer = other.gameObject.GetComponentsInChildren<Renderer>();//とりあえず子オブジェクトまで（孫は書いてない）
        foreach (var colorchangeRenderer in childRenderer)
        {
            ObjColorChange(colorchangeRenderer, objRendererList[exitRendererCount]);
            exitRendererCount++;
            Debug.Log("colorchangeRenderer" + colorchangeRenderer);
        }

        selectObj = null;
    }

    private void ObjColorChange(Renderer colorChangeRenderer,float alphaColor)
    {
        Color colorChangeObjColor = colorChangeRenderer.material.color;
        colorChangeObjColor.a = alphaColor;
        colorChangeRenderer.material.color = colorChangeObjColor; 
    }
}

