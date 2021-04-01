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
        D,
    };

    public enum pointType
    {
        red,
        blue,
        yellow,
        green,
    };

    public class _item
    {
        public Item item;
        public pointType pointType;
        public string itemName;
        public int itemCount;
        public int point;
        public _item(Item _item, pointType _pointType, string name, int count, int _point)
        {
            item = _item;
            pointType = _pointType;
            itemName = name;
            itemCount = count;
            point = _point;
        }
    }

    public Dictionary<int, _item> ItemInfoDic = new Dictionary<int, _item>()
    {
        {0, new _item(Item.A,pointType.red,"A",0,80)},
        {1, new _item(Item.B,pointType.blue,"B",0,50)},
        {2, new _item(Item.C, pointType.yellow,"C",0,30)},
        {3, new _item(Item.D, pointType.green,"D",0,10)},
    };
}
