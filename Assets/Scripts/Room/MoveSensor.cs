using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSensor : MonoBehaviour
{
    Transform sensorObjPos; 

    private void Awake()
    {
        sensorObjPos = gameObject.GetComponent<Transform>();

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.W)) //上キーが押されていれば
        {
            sensorObjPos.position += new Vector3(0.0f, 3.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.S)) //下キーが押されていれば
        {
            sensorObjPos.position += new Vector3(0.0f, -3.0f, 0.0f); 
        }
        if (Input.GetKey(KeyCode.A)) //左キーが押されていれば
        {
            sensorObjPos.position += new Vector3(-3.0f, 0.0f, 0.0f); 
        }
        else if (Input.GetKey(KeyCode.D)) //右キーが押されていれば
        {
            sensorObjPos.position += new Vector3(3.0f, 0.0f, 0.0f); 
        }
    }
}
