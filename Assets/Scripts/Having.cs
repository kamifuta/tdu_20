using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Having : MonoBehaviour
{
    public class _item
    {
        public string itemName;
        public int itemCount;
        public _item(string name,int count)
        {
            itemName = name;
            itemCount = count;
        }
    }

    private ItemInfo itemInfo = new ItemInfo();
    public Dictionary<int, _item> HaveItem = new Dictionary<int, _item>();

    public void GetItem(ItemInfo.Item key)
    {
        if (CheckHadItem(key))
        {
            HaveItem[(int)key].itemCount++;
        }
        else
        {
            _item item = new _item(itemInfo.ItemName[(int)key], 1);
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
}
