using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TrapsInfo
{
    public enum Trap
    {
        LeftMoveTrap,
        RightMoveTrap,
        UpMoveTrap,
        DownMoveTrap,
        IceTrap,
        BubbleTrap,
    };

    public class _trap
    {
        public Trap trap;
        public ItemInfo.pointType pointType;
        public string prefabAddress;
        public string itemName;
        public int itemCount;
        public int point;
        public _trap(Trap _trap, ItemInfo.pointType _pointType, string address, string name, int count, int _point)
        {
            trap = _trap;
            pointType = _pointType;
            prefabAddress = address;
            itemName = name;
            itemCount = count;
            point = _point;
        }
    }

    public Dictionary<int, _trap> trapInfoDic = new Dictionary<int, _trap>(){
        {0, new _trap(Trap.LeftMoveTrap,ItemInfo.pointType.red,"LeftMoveTrap","移動←トラップ",0,20)},
        {1, new _trap(Trap.RightMoveTrap,ItemInfo.pointType.red,"RightMoveTrap","移動→トラップ",0,20)},
        {2, new _trap(Trap.UpMoveTrap,ItemInfo.pointType.red,"UpMoveTrap","移動↑トラップ",0,20)},
        {3, new _trap(Trap.DownMoveTrap,ItemInfo.pointType.red,"DownMoveTrap","移動↓トラップ",0,20)},
        {4, new _trap(Trap.IceTrap,ItemInfo.pointType.red,"IceTrap","氷塊トラップ",0,20) },
        {5, new _trap(Trap.BubbleTrap,ItemInfo.pointType.red,"BubbleTrap","泡トラップ",0,20) },
    };
}
