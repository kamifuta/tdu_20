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
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10.0f)) 
            {
               Debug.Log(hit.collider.gameObject.name);
                
            }
        
        
     
        }
    }
    protected override void OnAwake()
    {
        GameObject prefab = (GameObject)Instantiate(IceImage,new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
        prefab.transform.SetParent(canvas.transform, false);


    }
}
