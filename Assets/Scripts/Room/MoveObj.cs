using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveObj : MonoBehaviour
{
    [SerializeField] GameObject moveSensorObj = null;
    //MoveSensor moveSensor;

    private float clickposX;
    private float clickposZ;

    private MoveObjTriggerStayJudge stayJudge;
    private Collider moveObjCollider;
    private Collider moveObjCollidersub;
    Rigidbody moveObjRigidBody;

    private GameObject moveObj;//MoveSensorからいじる
    private void Awake()
    {
        //moveSensor = moveSensorObj.GetComponent<MoveSensor>();
        gameObject.SetActive(false);
    }
    public void MoveObjSet(GameObject selectObj)
    {
        moveObj = selectObj;
        for (int i=0; i<3;i++)//読み取り専用…
        {
            if (i==0)
            {
                moveObjCollidersub = moveObj.GetComponent<BoxCollider>();
            }
            else if (i==1)
            {
                moveObjCollidersub = moveObj.GetComponent<SphereCollider>();
            }
            else if (i==2)
            {
                moveObjCollidersub = moveObj.GetComponent<CapsuleCollider>();
            }
            if (moveObjCollidersub != null)
            {
                moveObjCollider = moveObjCollidersub;
                break;
            }
        }
        //moveObjCollidersub = moveObj.GetComponent<Collider>();/////////////////////////////
        moveObjCollider.isTrigger = true;
        stayJudge = moveObj.AddComponent<MoveObjTriggerStayJudge>();
        moveObjRigidBody=moveObj.AddComponent<Rigidbody>();
        moveObjRigidBody.useGravity = false;
    }

    private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveObj.transform.position += new Vector3(0.0f, 0.0f, 1.5f);   
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveObj.transform.position += new Vector3(0.0f, 0.0f, -1.5f);   
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveObj.transform.position += new Vector3(-1.5f, 0.0f, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveObj.transform.position += new Vector3(1.5f, 0.0f, 0.0f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ClickFloor(ray);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stayJudge.stayObj == null)
            {
                moveSensorObj.SetActive(true);
                moveObj.transform.position += new Vector3(0.0f, -1.0f, 0.0f);
                moveSensorObj.transform.position = new Vector3(moveObj.transform.position.x, moveSensorObj.transform.position.y, moveObj.transform.position.z);////
                moveObjCollider.isTrigger = false;
                Destroy(stayJudge); 
                Destroy(moveObjRigidBody);
                gameObject.SetActive(false);
            }
            else
            {
                //置けないよーっていうなんか。
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("オブジェクト削除!!!!!!!!!!");
            DestroyObj((Object)moveObj);
            moveSensorObj.SetActive(true);
            moveSensorObj.transform.position = new Vector3(moveObj.transform.position.x, moveSensorObj.transform.position.y, moveObj.transform.position.z);
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("stayJudge:"+ stayJudge);
            Debug.Log("moveObjCollider:" + moveObjCollider);
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

                    moveObj.transform.position = new Vector3(clickposX, moveObj.transform.position.y, clickposZ);
     
                }

            }
        }
    }
    //できるかは分かんない
    [PunRPC]
    private void DestroyObj(Object destroyObj)
    {
        Destroy(destroyObj);
    }

}
