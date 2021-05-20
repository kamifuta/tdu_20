using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCGroupSettings:MonoBehaviour
{
    public void AddGroup(int groupNum)
    {
        PhotonNetwork.SetInterestGroups(null, new byte[] { (byte)groupNum });
    }

    public void RemoveGroup(int removeNum)
    {
        PhotonNetwork.SetInterestGroups(new byte[] { (byte)removeNum }, null);
    }

}
