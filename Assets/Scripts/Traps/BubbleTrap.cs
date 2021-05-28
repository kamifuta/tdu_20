using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleTrap : Trap
{
    
    public GameObject BubbleT;
  
    private GameObject prefab;
    
    float BubbleHp = 10;
    public Camera mainCamera;




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
            Vector3 p = new Vector3(Random.value, Random.value, 15.0f);
            p = mainCamera.ViewportToWorldPoint(p);
            prefab = Instantiate(BubbleT, p, Quaternion.identity);
            //prefab.transform.SetParent(canvas.transform, false);
           
        }

        
        
    }

  
}
