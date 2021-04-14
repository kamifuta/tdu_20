using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMoveTrap : Trap
{
    protected override void OnAwake()
    {
        Vector3 force = new Vector3(Random.Range(-10.0f, -20.0f), 0.0f, 0.0f);
        rb.AddForce(force, ForceMode.Impulse);
    }

}
