using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Photonの設定関係まとめ
/// </summary>
public class PunSettings : MonoBehaviour
{
    public SetCustomPropertiesManager propertiesManager = new SetCustomPropertiesManager();
    public PropertiesKeyList propertiesKeyList = new PropertiesKeyList();
    public RPCGroupSettings rpcGroupSettings = new RPCGroupSettings();
}
