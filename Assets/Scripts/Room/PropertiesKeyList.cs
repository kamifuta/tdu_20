using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertiesKeyList : MonoBehaviour
{
    /// <summary>
    /// Panel情報(int[])
    /// </summary>
    public string panelListKey { get => "panel"; }

    /// <summary>
    /// 化石掘りの最中かどうか(bool)
    /// </summary>
    public string digKey { get => "dig"; }//初回追加

    /// <summary>
    /// 今旗をもっているかどうか
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

}
