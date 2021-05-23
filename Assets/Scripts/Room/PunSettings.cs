using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Photonの設定関係まとめ
/// </summary>
public class PunSettings : MonoBehaviour
{
    //public GameObject a;
    public SetCustomPropertiesManager propertiesManager;
    public PropertiesKeyList propertiesKeyList;
    public RPCGroupSettings rpcGroupSettings;
    public void Start()
    {
        propertiesManager = new SetCustomPropertiesManager();
        propertiesKeyList = new PropertiesKeyList();
        rpcGroupSettings = new RPCGroupSettings();
        Debug.Log("propertiesManager:"+propertiesManager);
    }
}
