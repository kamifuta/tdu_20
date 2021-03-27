using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    [SerializeField] GameObject moveSensorObj = null;
    //MoveSensor moveSensor;

    private float clickposX;
    private float clickposZ;

    [System.NonSerialized]public GameObject selectObj;//MoveSensorからいじる
    private void Awake()
    {
        //moveSensor = moveSensorObj.GetComponent<MoveSensor>();
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.W))
        {

            selectObj.transform.position += new Vector3(0.0f, 0.0f, 1.5f);
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {

            selectObj.transform.position += new Vector3(0.0f, 0.0f, -1.5f);
            
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {

            selectObj.transform.position += new Vector3(-1.5f, 0.0f, 0.0f);
        }
        
        else if (Input.GetKeyDown(KeyCode.D))
        {
            selectObj.transform.position += new Vector3(1.5f, 0.0f, 0.0f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ClickFloor(ray);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            moveSensorObj.SetActive(true);
            selectObj.transform.position += new Vector3(0.0f,-1.0f,0.0f);
            moveSensorObj.transform.position = new Vector3(0, moveSensorObj.transform.position.y,0);//////////////////////////////////////////////////////////////////////////////////////////////////
            gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("オブジェクト削除!!!!!!!!!!");
            //Delete書く//////////////////////////////////////////////////////////////////////////////////////////////////////////
            moveSensorObj.SetActive(true);
            moveSensorObj.transform.position = new Vector3(0, moveSensorObj.transform.position.y,0);//////////////////////////////////////////////////////////////////////////////////////////////////
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
                //sensorObjPos.position 

                if (Physics.Raycast(ray, out hit[i]))
                {
                    Debug.Log("hit[i].point" + hit[i].point);
                    clickposX = ((int)(hit[i].point.x / 1.5)) * 1.5f;///////////////////////////////////////////////四捨五入がいい
                    clickposZ = ((int)(hit[i].point.z / 1.5)) * 1.5f;///////////////////////////////////////////////四捨五入がいい
                    Debug.Log("clickposX:" + clickposX);
                    Debug.Log("clickposY:" + clickposZ);

                    selectObj.transform.position = new Vector3(clickposX, selectObj.transform.position.y, clickposZ);
     
                }

            }
        }
    }

}
