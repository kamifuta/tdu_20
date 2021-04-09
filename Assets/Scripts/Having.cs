using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Having : MonoBehaviour
{
    private ItemInfo itemInfo = new ItemInfo();
    private TrapsInfo trapInfo = new TrapsInfo();
   
    public Dictionary<int, ItemInfo._item> HaveItem = new Dictionary<int, ItemInfo._item>();
    public Dictionary<int, TrapsInfo._trap> HaveTrap = new Dictionary<int, TrapsInfo._trap>();
    public Dictionary<int, FossilInfo.Fossil> HaveFossil = new Dictionary<int, FossilInfo.Fossil>();
    public int redPoint = 0;
    public int bluePoint = 0;
    public int yellowPoint = 0;
    public int greenPoint = 0;

    private PlayerAction playerAction;

    private void Awake()
    {
        playerAction = GetComponent<PlayerAction>();
    }

    //アイテム用------------------------------------------------

    public void GetItem(ItemInfo.Item key)
    {
        if (CheckHadItem(key))
        {
            HaveItem[(int)key].itemCount++;
        }
        else
        {
            ItemInfo._item item = new ItemInfo().ItemInfoDic[(int)key];
            item.itemCount = 1;
            HaveItem.Add((int)key, item);
        }
    }

    public void ThrowItem(ItemInfo.Item key)
    {
        if(HaveItem[(int)key].itemCount > 0)
        {
            HaveItem[(int)key].itemCount--;
        }
    }

    public bool CheckHadItem(ItemInfo.Item key)
    {
        if (HaveItem.ContainsKey((int)key))
        {
            return true;
        }

        return false;
    }
    //---------------------------------------------------------------------------

    //トラップ用-----------------------------------------------------------------

    public void GetTrap(TrapsInfo.Trap key)
    {
        if (CheckHadTrap(key))
        {
            HaveTrap[(int)key].itemCount++;
        }
        else
        {
            TrapsInfo._trap trap = new TrapsInfo().trapInfoDic[(int)key];
            trap.itemCount = 1;
            HaveTrap.Add((int)key, trap);
        }
    }

    public void PutTrap(TrapsInfo.Trap key)
    {
        if (HaveTrap[(int)key].itemCount > 0)
        {
            HaveTrap[(int)key].itemCount--;
        }

        playerAction.Put(key);
        //Instantiate(trapPrefab, transform.position + Vector3.forward, Quaternion.identity);
    }

    public void ThrowTrap(TrapsInfo.Trap key)
    {
        if (HaveTrap[(int)key].itemCount > 0)
        {
            HaveTrap[(int)key].itemCount--;
        }
    }

    public bool CheckHadTrap(TrapsInfo.Trap key)
    {
        if (HaveTrap.ContainsKey((int)key))
        {
            return true;
        }

        return false;
    }

    //----------------------------------------------------------------

    //化石用----------------------------------------------------------

    public void GetFossil(FossilInfo.FossilSize size, ItemInfo.pointType color)
    {
        if (CheckHadFossil(size,color))
        {
            HaveFossil[(int)size + (int)color * 3].itemCount++;
        }
        else
        {
            FossilInfo.Fossil fossil = new FossilInfo().FossilInfoDic[(int)size * 3 + (int)color];
            fossil.itemCount = 1;
            HaveFossil.Add((int)size + (int)color * 3, fossil);
            /*HaveItem[key].itemName = new ItemInfo().ItemName[(int)key];
            HaveItem[key].itemCount = 1;*/
        }
    }

    public void ThrowFossil(FossilInfo.FossilSize size, ItemInfo.pointType color)
    {
        //ItemInfo.Item key = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), itemInfo.ItemName.IndexOf(itemName));
        if (HaveItem[(int)size + (int)color * 3].itemCount > 0)
        {
            HaveFossil[(int)size + (int)color * 3].itemCount--;
        }
    }

    public bool CheckHadFossil(FossilInfo.FossilSize size, ItemInfo.pointType color)
    {
        if (HaveFossil.ContainsKey((int)size + (int)color * 3))
        {
            return true;
        }
        return false;
    }

    //----------------------------------------------------------------

    //ポイント用-------------------------------------------------------

    public void GetPoint(ItemInfo.pointType color, int point)
    {
        switch (color)
        {
            case ItemInfo.pointType.red:
                redPoint += point;
                break;
            case ItemInfo.pointType.blue:
                bluePoint += point;
                break;
            case ItemInfo.pointType.yellow:
                yellowPoint += point;
                break;
            case ItemInfo.pointType.green:
                yellowPoint += point;
                break;
        }
    }

    public void LosePoint(ItemInfo.pointType color, int point)
    {
        switch (color)
        {
            case ItemInfo.pointType.red:
                redPoint -= point;
                break;
            case ItemInfo.pointType.blue:
                bluePoint -= point;
                break;
            case ItemInfo.pointType.yellow:
                yellowPoint -= point;
                break;
            case ItemInfo.pointType.green:
                yellowPoint -= point;
                break;
        }
    }
}
