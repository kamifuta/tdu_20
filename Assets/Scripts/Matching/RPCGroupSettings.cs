using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCGroupSettings
{
    public void AddGroup(byte groupNum)
    {
        PhotonNetwork.SetInterestGroups(null, new byte[] { groupNum });
    }
    public byte RandomAddGroup()
    {
        byte newGroupNum=0;

        if (PhotonNetwork.CurrentRoom.CustomProperties["now"] is byte nowGroupNum)
        {
            newGroupNum = nowGroupNum ++;
            
            if (PhotonNetwork.CurrentRoom.CustomProperties[nameof(newGroupNum)] is byte)//255までいってたら
            {
                newGroupNum=SearchGroup();
            }
            
            var customRoomProperties = new ExitGames.Client.Photon.Hashtable();
            customRoomProperties["now"] = newGroupNum.ToString();
            PhotonNetwork.CurrentRoom.SetCustomProperties(customRoomProperties);

            //PhotonNetwork.SetInterestGroups(Array.Empty<byte>());
            PhotonNetwork.SetInterestGroups(null, new byte[] { newGroupNum });
        }
        return newGroupNum;
    }
    public byte SearchGroup()
    {
        for (int i = 1; i < 256; i++)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties[nameof(i)] is byte)
            {
                continue;
            }
            else
            {
                return (byte)i;
            }
        }
        return 0;
    }



    public void RemoveGroup(byte removeNum)
    {
        PhotonNetwork.SetInterestGroups(new byte[] { removeNum }, null);
        var customRoomProperties = new ExitGames.Client.Photon.Hashtable();

        customRoomProperties[removeNum.ToString()] = null;

        PhotonNetwork.CurrentRoom.SetCustomProperties(customRoomProperties);
    }

}
