using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    private GameObject Fire_EF_Obj;
    private ParticleSystem Fire_EF;
    // Start is called before the first frame update
    void Start()
    {
        Fire_EF_Obj= Resources.Load<GameObject>("Fire");

        Fire_EF = Instantiate(Fire_EF_Obj, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        Fire_EF.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
