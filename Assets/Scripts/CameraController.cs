using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject target;
    private GameObject[] targets;
    // Start is called before the first frame update
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
        foreach(var a in targets)
        {
            if (a.GetComponent<PhotonView>().IsMine)
            {
                target = a;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + Vector3.up * 6-Vector3.forward*3;
        transform.LookAt(target.transform);
    }
}
