using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap:MonoBehaviour
{
    protected Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            OnAwake();
        }
    }

    protected virtual void OnAwake()
    {

    }
}
