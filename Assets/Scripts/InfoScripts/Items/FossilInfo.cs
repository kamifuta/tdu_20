﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FossilInfo
{
    public enum FossilSize
    {
        small,
        big,
        large,
    };

    /*public enum FossilColor
    {
        red,
        blue,
        yellow,
        green,
    };*/

    public class Fossil
    {
        public FossilSize fossileSize;
        public ItemInfo.pointType fossilColor;
        public string itemName;
        public int itemCount;
        public int point;
        public Fossil(FossilSize size, ItemInfo.pointType color, string name, int count, int _point)
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
        {0, new Fossil(FossilSize.small,ItemInfo.pointType.red,"小さい　赤色の　宝石",0,10)},
        {1, new Fossil(FossilSize.big,ItemInfo.pointType.red,"大きい　赤色の　宝石",0,30)},
        {2, new Fossil(FossilSize.large,ItemInfo.pointType.red,"特大の　赤色の　宝石",0,50)},
        {3, new Fossil(FossilSize.small,ItemInfo.pointType.blue,"小さい　青色の　宝石",0,10)},
        {4, new Fossil(FossilSize.big,ItemInfo.pointType.blue,"大きい　青色の　宝石",0,30)},
        {5, new Fossil(FossilSize.large,ItemInfo.pointType.blue,"特大の　青色の　宝石",0,50)},
        {6, new Fossil(FossilSize.small,ItemInfo.pointType.yellow,"小さい　黄色の　宝石",0,10)},
        {7, new Fossil(FossilSize.big,ItemInfo.pointType.yellow,"大きい　黄色の　宝石",0,30)},
        {8, new Fossil(FossilSize.large,ItemInfo.pointType.yellow,"特大の　黄色の　宝石",0,50)},
        {9, new Fossil(FossilSize.small,ItemInfo.pointType.green,"小さい　緑色の　宝石",0,10)},
        {10, new Fossil(FossilSize.big,ItemInfo.pointType.green,"大きい　緑色の　宝石",0,30)},
        {11, new Fossil(FossilSize.large,ItemInfo.pointType.green,"特大の　緑色の　宝石",0,50)},
    };
}