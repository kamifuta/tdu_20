using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleTrap : Trap
{
    public Image BubbleImage;
    float BubbleHp = 0;
    // Start is called before the first frame update
    void Start()
    {
        BubbleHp = Random.Range(6, 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void OnAwake()
    {
        for (int i = 0; i < BubbleHp; i++)
        {
            Instantiate(BubbleImage);
        }

        
        
    }

  
}
