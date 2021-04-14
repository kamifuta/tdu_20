using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//基地に設置可能なオブジェクトの情報
public class ItemInfo
{
    public enum Item
    {
        RoomMaker,
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
        public string prefabAddress;
        public string itemName;
        public int itemCount;
        public int point;
        public _item(Item _item, pointType _pointType,　string address, string name, int count, int _point)
        {
            item = _item;
            pointType = _pointType;
            prefabAddress = address;
            itemName = name;
            itemCount = count;
            point = _point;
        }
    }

    public Dictionary<int, _item> ItemInfoDic = new Dictionary<int, _item>()
    {
        {0, new _item(Item.RoomMaker, pointType.green,"RoomGate", "あなほりドリル",0,10)},
    };
}
