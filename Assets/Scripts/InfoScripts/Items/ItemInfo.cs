using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{
    public enum Item
    {
        A,
        B,
        C,
    };

    public Dictionary<int,string> ItemName = new Dictionary<int,string>()
    {
        {0, "A"},
        {1, "B"},
        {2, "C"},
    };
}
