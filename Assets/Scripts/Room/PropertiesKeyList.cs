using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カスタムプロパティで使うKey(string)一覧
/// </summary>
public class PropertiesKeyList 
{
    /// <summary>
    /// Panel情報(int[])
    /// </summary>
    public string panelListKey { get => "panel"; }

    /// <summary>
    /// 化石掘りの最中かどうか(bool)
    /// </summary>
    public string digKey { get => "dig"; }

    /// <summary>
    /// 今もっている旗の数(int)
    /// </summary>
    public string nowFlagKey { get => "nflag"; }

    /// <summary>
    /// 基地に持ち帰った旗の数
    /// </summary>
    public string flagKey { get => "flag"; }

    /// <summary>
    /// 化石堀のときのHP
    /// </summary>
   public string hpKey { get => "hp"; }

    /// <summary>
    /// 化石の位置情報
    /// </summary>
    public string fossilKey { get => "fossil"; }
    public PropertiesKeyList()
    {
        ExitGames.Client.Photon.Hashtable customPlayerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        customPlayerProperties[panelListKey] = new int[1];
        customPlayerProperties[digKey] = false;
        customPlayerProperties[nowFlagKey] = 0;
        customPlayerProperties[flagKey] = 0;
        customPlayerProperties[hpKey] = 30;
        customPlayerProperties[fossilKey] = new float[1];
        PhotonNetwork.LocalPlayer.SetCustomProperties(customPlayerProperties);
        Debug.Log("StartSetPlayerCustomProperties");
    }
}
