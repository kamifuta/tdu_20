using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMoveTrap : Trap
{
    protected override void OnAwake()
    {
        Vector3 force = new Vector3(0.0f, 0.0f, Random.Range(-10.0f, -20.0f));
        rb.AddForce(force, ForceMode.Impulse);
    }
}
