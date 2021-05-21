using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SetCustomPropertiesManager : MonoBehaviour
{
    public void RoomCustomPropertiesSettings<T>(T properties,string name)
    {
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        customRoomProperties[name] = properties;
        PhotonNetwork.CurrentRoom.SetCustomProperties(customRoomProperties);
        Debug.Log("SetRoomCustomProperties");
    }

    public void PlayerCustomPropertiesSettings<T>(T properties, string name,Player player)
    {
        ExitGames.Client.Photon.Hashtable customPlayerProperties = player.CustomProperties;
        customPlayerProperties[name] = properties;
        player.SetCustomProperties(customPlayerProperties);
        Debug.Log("SetPlayerCustomProperties");
    }

}
