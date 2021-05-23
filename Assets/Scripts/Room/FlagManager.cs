using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FlagManager : PunSettings
{
    //[SerializeField]PunSettings punSettings;

    public void GetFlag()
    {
        int myNowFlagNum = (int)PhotonNetwork.LocalPlayer.CustomProperties[propertiesKeyList.nowFlagKey];
        propertiesManager.PlayerCustomPropertiesSettings(myNowFlagNum+1, propertiesKeyList.nowFlagKey, PhotonNetwork.LocalPlayer) ;
    }
    public void SetFlag()
    {
        int myNowFlagNum= (int)PhotonNetwork.LocalPlayer.CustomProperties[propertiesKeyList.nowFlagKey];
        int mysetFlagNum= (int)PhotonNetwork.LocalPlayer.CustomProperties[propertiesKeyList.flagKey];

        propertiesManager.PlayerCustomPropertiesSettings(mysetFlagNum+myNowFlagNum, propertiesKeyList.flagKey, PhotonNetwork.LocalPlayer);
        propertiesManager.PlayerCustomPropertiesSettings(0, propertiesKeyList.nowFlagKey, PhotonNetwork.LocalPlayer);
    }


    public void SteelFlag(Player talkToPlayer, int talktoPlayerNowFlag)
    {
        //相手側の設定
        propertiesManager.PlayerCustomPropertiesSettings(0, propertiesKeyList.nowFlagKey, talkToPlayer);

        //自分側の設定
        int myNowFlagNum = (int)PhotonNetwork.LocalPlayer.CustomProperties[propertiesKeyList.nowFlagKey];
        propertiesManager.PlayerCustomPropertiesSettings(talktoPlayerNowFlag + myNowFlagNum, propertiesKeyList.nowFlagKey, PhotonNetwork.LocalPlayer);
    }

}
