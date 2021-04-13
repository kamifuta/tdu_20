using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon.StructWrapping;

public class MoveObj : MonoBehaviour
{
    public GameObject moveSensorObj = null;
    //MoveSensor moveSensor;

    private float clickposX;
    private float clickposZ;

    private MoveObjTriggerStayJudge stayJudge;
    private Collider moveObjCollider;
    private Collider moveObjCollidersub;
    Rigidbody moveObjRigidBody;
    PhotonView photonView;
    //private GameObject moveObj;//MoveSensorからいじる

    public void MoveObjSet(GameObject moveSensor)
    {
        for (int i=0; i<3;i++)//読み取り専用…
        {
            if (i==0)
            {
                moveObjCollidersub = GetComponent<BoxCollider>();
            }
            else if (i==1)
            {
                moveObjCollidersub = GetComponent<SphereCollider>();
            }
            else if (i==2)
            {
                moveObjCollidersub = GetComponent<CapsuleCollider>();
            }
            if (moveObjCollidersub != null)
            {
                moveObjCollider = moveObjCollidersub;
                break;
            }
        }
        //moveObjCollidersub = moveObj.GetComponent<Collider>();/////////////////////////////
        moveObjCollider.isTrigger = true;
        stayJudge = gameObject.AddComponent<MoveObjTriggerStayJudge>();
        moveObjRigidBody=gameObject.AddComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
        moveSensorObj = moveSensor;
        moveObjRigidBody.useGravity = false;
    }

    private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, 1.5f);   
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, 0.0f, -1.5f);   
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1.5f, 0.0f, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1.5f, 0.0f, 0.0f);
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
                transform.position += new Vector3(0.0f, -1.0f, 0.0f);
                float posx = transform.position.x;
                float posz = transform.position.z;
                Debug.Log(photonView);
                photonView.RPC(nameof(ObjPosSend), RpcTarget.Others ,transform.position);
                moveSensorObj.transform.position = new Vector3(posx, moveSensorObj.transform.position.y, posz);////
                moveObjCollider.isTrigger = false;
                Destroy(stayJudge); 
                Destroy(moveObjRigidBody);
                Destroy(gameObject.GetComponent<MoveObj>());
            }
            else
            {
                //置けないよーっていうなんか。
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("オブジェクト削除!!!!!!!!!!");         
            moveSensorObj.SetActive(true);
            moveSensorObj.transform.position = new Vector3(transform.position.x, moveSensorObj.transform.position.y, transform.position.z);
            photonView.RPC(nameof(DestroyObj), RpcTarget.AllViaServer);//Photon適応
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

                    transform.position = new Vector3(clickposX, transform.position.y, clickposZ);
     
                }

            }
        }
    }
    //できるかは分かんない
    [PunRPC]
    public void DestroyObj(PhotonMessageInfo info)
    {
        Destroy(info.photonView.gameObject);
    }
    [PunRPC]
    public void ObjPosSend(Vector3 pos, PhotonMessageInfo Info)
    {
        Info.photonView.gameObject.transform.position = new Vector3(pos.x,pos.y,pos.z);
    }
}
