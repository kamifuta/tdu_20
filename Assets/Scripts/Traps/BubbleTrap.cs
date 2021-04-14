using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleTrap : Trap
{
    public GameObject canvas;
    public GameObject BubbleT;
    float BubbleHp = 0;
    // Start is called before the first frame update
    void Start()
    {
        BubbleHp = Random.Range(6, 8);
        OnAwake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnAwake()
    {
        for (int i = 0; i < BubbleHp; i++)
        {
            GameObject prefab = (GameObject)Instantiate(BubbleT, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            prefab.transform.SetParent(canvas.transform, false);

        }

        
        
    }

  
}
