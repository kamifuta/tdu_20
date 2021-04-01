using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsInfo
{
    public enum TrapEnum
    {
        fireTrap,
    };

    public Dictionary<int, string> trapName=new Dictionary<int, string>(){
        {0, "ほのおトラップ"},
    };
}
