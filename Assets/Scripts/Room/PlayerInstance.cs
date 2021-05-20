using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInstance : MonoBehaviour
{
    string playerObj= "Player_variant Variant";
 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PhotonNetwork.Instantiate(playerObj,new Vector3(0,1.3f,0),new Quaternion(0,0,0,0));
        }


    }

}
