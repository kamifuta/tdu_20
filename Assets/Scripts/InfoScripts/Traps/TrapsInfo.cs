using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TrapsInfo
{
    public enum Trap
    {
        fireTrap,
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
        {0, new _trap(Trap.fireTrap,ItemInfo.pointType.red,"trap_hole","ほのおトラップ",0,20)},
    };
}
