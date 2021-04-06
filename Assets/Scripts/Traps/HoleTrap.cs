using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrap : Trap
{
    private bool HTrue = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HTrue==true)
        {
            float x = 0, z = 0;
            if (Input.GetButton("Horizontal"))
            {
                x = Input.GetAxis("Horizontal");
            }

            if (Input.GetButton("Vertical"))
            {
                z = Input.GetAxis("Vertical");
            }

            transform.Translate(new Vector3(-x, 0, -z) * Time.deltaTime);
        }
    }

    protected override void OnAwake()
    {
        HTrue = true;
        //後キャラの向きだけかえる
    }
}
