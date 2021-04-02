using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FossilInfo : MonoBehaviour
{
    public enum FossilSize
    {
        small,
        medium,
        large,
    };

    public enum FossilColor
    {
        red,
        blue,
        yellow,
        green,
    };

    public class Fossil
    {
        public FossilSize fossileSize;
        public FossilColor fossilColor;
        public string itemName;
        public int itemCount;
        public int point;
        public Fossil(FossilSize size, FossilColor color, string name, int count, int _point)
        {
            fossileSize = size;
            fossilColor = color;
            itemName = name;
            itemCount = count;
            point = _point;
        }
    }

    public Dictionary<int, Fossil> FossilInfoDic = new Dictionary<int, Fossil>()
    {
        {0, new Fossil(FossilSize.small,FossilColor.red,"小さい　赤い　玉",0,80)},
        {1, new Fossil(FossilSize.medium,FossilColor.red,"普通の　赤い　玉",0,80)},
        {2, new Fossil(FossilSize.large,FossilColor.red,"大きい　赤い　玉",0,80)},
        {3, new Fossil(FossilSize.small,FossilColor.red,"小さい　赤い　玉",0,80)},
    };
}
