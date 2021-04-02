using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjTriggerStayJudge : MonoBehaviour
{
    [System.NonSerialized] public GameObject stayObj;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaEnter");
        stayObj = other.gameObject;
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaExit");
        stayObj = null;
    }
}
