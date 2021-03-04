using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningPointGenerator : MonoBehaviour
{
    public GameObject miningPointObj;

    private GameObject pointObj;
    private Vector3 miningPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (pointObj != null)
            {
                Destroy(pointObj);
            }

            int a = Random.Range(0, 4);
            if (a == 0)
            {
                miningPos = new Vector3(1.5f, 0.5f, 0);
            }
            else if (a == 1)
            {
                miningPos = new Vector3(0, 0.5f, 1.5f);
            }
            else if (a == 2)
            {
                miningPos = new Vector3(-1.5f, 0.5f, 0);
            }
            else if (a == 3)
            {
                miningPos = new Vector3(0, 0.5f, -1.5f);
            }

            pointObj = Instantiate(miningPointObj, transform.position+miningPos, Quaternion.identity);
        }
    }
}
