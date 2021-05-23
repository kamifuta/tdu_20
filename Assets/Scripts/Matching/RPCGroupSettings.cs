using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 化石堀りのときのグループ設定
/// </summary>
public class RPCGroupSettings
{
    public void AddGroup(int groupNum)
    {
        PhotonNetwork.SetInterestGroups(null, new byte[] { (byte)groupNum });
    }

    public void RemoveGroup(int removeNum)
    {
        Debug.Log("removeNum" + removeNum);
        PhotonNetwork.SetInterestGroups(new byte[] { (byte)removeNum }, null);
    }

}
