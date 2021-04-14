using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMoveTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(Random.Range(-10.0f, -20.0f), 0.0f, 0.0f);
        rb.AddForce(force, ForceMode.Impulse);
    }


}
