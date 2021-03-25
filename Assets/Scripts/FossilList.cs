using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  F = Fossil

public class FossilList
{
    string Fname;
    Sprite Fsprite;
    int[,] Fsize;
    
    public FossilList(string _name,Sprite _sprite,int[,] _size)
    {
        Fname = _name;
        Fsprite = _sprite;
        Fsize = _size;
    }

    /*
    public class SmallRedGem
    {
        string _SmallRedGem = "小さい　赤い　宝石";
        public Sprite SmallRedGemSprite;
        //大きさ
    }

    public class LargeBlueGem
    {
        string _LargeBlueGem = "大きい　青い　宝石";
        public Sprite LargeBlueGemSprite;
        //大きさ
    }
    */
}
