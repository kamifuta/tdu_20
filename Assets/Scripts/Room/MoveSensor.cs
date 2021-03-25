using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSensor : MonoBehaviour
{
    Transform sensorObjPos;
    [SerializeField]Animator sensorAnimator=null;
    [System.NonSerialized] public GameObject selectObj;
   
    private List<float> objColorListBackup=new List<float>();
    private List<GameObject> objListBackup = new List<GameObject>();

    private float clickposX;
    private float clickposZ;


    private void Awake()
    {
        sensorObjPos = gameObject.GetComponent<Transform>();
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
        float[] objColorAlpha;
        GameObject[] objInRenderer;
        if (other.gameObject.GetComponent<Renderer>())//とりあえず子オブジェクトまで（孫は書いてない）
        {
            objColorAlpha = new float[1];
            objInRenderer = new GameObject[1];
            objColorAlpha[0]=other.gameObject.GetComponent<Renderer>().material.color.a;//Add
            ObjColorChange(other.gameObject, 100f / 255f);
            Debug.Log("ObjColorChange");            
        }
        else
        {
            Debug.Log("EnterJewel");
            var childRenderer = other.gameObject.GetComponentsInChildren<Renderer>();
            int childTransformslen = childRenderer.Length;

            objColorAlpha = new float[childTransformslen];
            objInRenderer = new GameObject[childTransformslen];
            for (int i = 0; i < childTransformslen; i++)
            {
                objColorAlpha[i] = childRenderer[i].material.color.a;
                objInRenderer[i] = childRenderer[i].gameObject;
                ObjColorChange(objInRenderer[i], 100f / 255f);
            }
        }

        objColorListBackup.AddRange(objColorAlpha);
        objListBackup.AddRange(objInRenderer);

        Debug.Log(objColorAlpha);
        //objColorAlpha.Clear();
        selectObj = other.gameObject;
    }


    private void OnTriggerExit(Collider other)
    {
        List<float> enterObjColorAlpha=new List<float>();
        List<GameObject> enterObj=new List<GameObject>();
        enterObjColorAlpha.AddRange(objColorListBackup);
        enterObj.AddRange(objListBackup);
        objColorListBackup.Clear();
        int alphaCount = enterObjColorAlpha.Count;

        for (int i=0; enterObjColorAlpha[i] < alphaCount; i++)
        {
            ObjColorChange(other.gameObject, enterObjColorAlpha[i]);
        }
        //enterObjColorAlpha.Clear();
        //enterObjColorAlpha.Clear();
        selectObj = null;
        sensorAnimator.SetTrigger("IdleObj");
    }

    private void ObjColorChange(GameObject colorChangeObj,float alphaColor)
    {
        Color colorChangeObjColor = colorChangeObj.GetComponent<Renderer>().material.color;
        colorChangeObjColor.a = alphaColor;
        colorChangeObj.GetComponent<Renderer>().material.color = colorChangeObjColor; 
    }
}

