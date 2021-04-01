using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Having : MonoBehaviour
{
    

    public class _trap
    {
        public string itemName;
        public int itemCount;
        public _trap(string name, int count)
        {
            itemName = name;
            itemCount = count;
        }
    }

    

    private ItemInfo itemInfo = new ItemInfo();
    private TrapsInfo trapInfo = new TrapsInfo();
   
    public Dictionary<int, ItemInfo._item> HaveItem = new Dictionary<int, ItemInfo._item>();
    public Dictionary<int, _trap> HaveTrap = new Dictionary<int, _trap>();
    public int redPoint = 0;
    public int bluePoint = 0;
    public int yellowPoint = 0;

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
            /*HaveItem[key].itemName = new ItemInfo().ItemName[(int)key];
            HaveItem[key].itemCount = 1;*/
        }
    }

    public void ThrowItem(ItemInfo.Item key)
    {
        //ItemInfo.Item key = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), itemInfo.ItemName.IndexOf(itemName));
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

    public void GetTrap(TrapsInfo.TrapEnum key)
    {
        if (CheckHadTrap(key))
        {
            HaveTrap[(int)key].itemCount++;
        }
        else
        {
            _trap trap = new _trap(trapInfo.trapName[(int)key], 1);
            HaveTrap.Add((int)key, trap);
        }
    }

    public void PutTrap(TrapsInfo.TrapEnum key)
    {
        if (HaveTrap[(int)key].itemCount > 0)
        {
            HaveTrap[(int)key].itemCount--;
        }

        playerAction.Put(key);
        //Instantiate(trapPrefab, transform.position + Vector3.forward, Quaternion.identity);
    }

    public void ThrowTrap(TrapsInfo.TrapEnum key)
    {
        if (HaveTrap[(int)key].itemCount > 0)
        {
            HaveTrap[(int)key].itemCount--;
        }
    }

    public bool CheckHadTrap(TrapsInfo.TrapEnum key)
    {
        if (HaveTrap.ContainsKey((int)key))
        {
            return true;
        }

        return false;
    }

    //----------------------------------------------------------------

    //ポイント用-------------------------------------------------------

    public void GetPoint(ItemInfo.pointType pointType, int point)
    {
        switch (pointType)
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
        }
    }

    public void losePoint(ItemInfo.pointType pointType, int point)
    {

    }
}
