using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSensor : MonoBehaviour
{
    Transform sensorObjPos;
    [SerializeField]Animator sensorAnimator=null;

    private void Awake()
    {
        sensorObjPos = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        { 
            sensorObjPos.position += new Vector3(0.0f, 0.0f, 3.0f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            sensorObjPos.position += new Vector3(0.0f, 0.0f, -3.0f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            sensorObjPos.position += new Vector3(-3.0f, 0.0f, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            sensorObjPos.position += new Vector3(3.0f, 0.0f, 0.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        sensorAnimator.SetTrigger("TouchObj");
    }
    private void OnTriggerExit(Collider other)
    {
        sensorAnimator.SetTrigger("IdleObj");
    }
}

