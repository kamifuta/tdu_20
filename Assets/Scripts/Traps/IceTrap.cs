using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceTrap : Trap
{
    public GameObject canvas;
    public GameObject IceImage;
    private int IceHp;
    // Start is called before the first frame update
    void Start()
    {
        IceHp = Random.Range(7, 10);
        OnAwake();
    }

    // Update is called once per frame
    void Update()
    {

        GameObject obj = GameObject.Find("IceImage(Clone)");
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
        GameObject prefab = (GameObject)Instantiate(IceImage,new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
        prefab.transform.SetParent(canvas.transform, false);


    }
}
