using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectPosition : MonoBehaviour
{
    PhotonView photonView;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    public void ObjPosMove()
    {
        photonView.RPC(nameof(ObjPosMoveSend),RpcTarget.Others,transform.position);
    }

    public void ObjDestroy()
    {
        PhotonNetwork.Destroy(gameObject);
        //photonView.RPC(nameof(ObjDestroySend), RpcTarget.AllViaServer);
    }

    [PunRPC]
    public void ObjPosMoveSend(Vector3 pos,PhotonMessageInfo info)
    {
        info.photonView.transform.position = pos;
    }

    /*[PunRPC]
    public void ObjDestroySend(PhotonMessageInfo info)
    {
        Destroy(info.photonView.gameObject);
    }*/
}
