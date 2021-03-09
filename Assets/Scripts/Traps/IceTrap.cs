using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceTrap : Trap
{
    public Image IceImage;
    // Start is called before the first frame update
    void Start()
    {
        OnAwake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnAwake()
    {
        Instantiate(IceImage, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);


    }
}
