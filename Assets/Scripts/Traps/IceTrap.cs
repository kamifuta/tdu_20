using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceTrap : Trap
{

    public GameObject IceImage;
    private int IceHp;
    public GameObject obj;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        IceHp = Random.Range(7, 10);
        OnAwake();
    }

    // Update is called once per frame
    void Update()
    {

        obj = GameObject.Find("Bubble(Clone)");
        if (Input.GetMouseButtonDown(0)) 
        {
            IceHp -= 1;
            Debug.Log(IceHp);
        }
        if (IceHp == 0) 
        {
            Destroy(obj);
        }
    }
    protected override void OnAwake()
    {
        Vector3 p = new Vector3(0.5f, 0.5f, 5.0f);
        p = mainCamera.ViewportToWorldPoint(p);
        GameObject prefab = Instantiate(IceImage,p,Quaternion.identity);
        
        
        //キャラを動かせなくする
    }

}
