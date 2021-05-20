using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SetCustomPropertiesManager : MonoBehaviour
{
    public void SetPunCustomProperties<T>(T properties,string name)
    {
        ExitGames.Client.Photon.Hashtable customRoomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
        customRoomProperties[name] = properties;
        PhotonNetwork.CurrentRoom.SetCustomProperties(customRoomProperties);
        Debug.Log("set");
    }
    


}
