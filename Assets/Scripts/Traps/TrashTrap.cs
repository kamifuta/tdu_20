using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTrap : Trap
{
    [SerializeField] GameObject trashObj;
    private void Start()
    {
        Transform playertf = gameObject.transform;
        playertf.position = new Vector3(playertf.position.x, playertf.position.y+2.5f, playertf.position.z);
        Instantiate(trashObj, playertf);
        
    //Rigidbody rb = GetComponent<Rigidbody>();
    } 
   
}
