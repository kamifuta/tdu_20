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

    public Dictionary<ItemInfo.Item, _item> HaveItem = new Dictionary<ItemInfo.Item, _item>();

    public void GetItem(ItemInfo.Item key)
    {
        if (CheckHadItem(key))
        {
            HaveItem[key].itemCount++;
        }
        else
        {
            _item item = new _item(new ItemInfo().ItemName[(int)key], 1);
            HaveItem.Add(key, item);
            /*HaveItem[key].itemName = new ItemInfo().ItemName[(int)key];
            HaveItem[key].itemCount = 1;*/
        }
    }

    public void ThrowItem(string itemName)
    {
        ItemInfo.Item key = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), new ItemInfo().ItemName.IndexOf(itemName));
        if(HaveItem[key].itemCount > 0)
        {
            HaveItem[key].itemCount--;
        }
    }

    public bool CheckHadItem(ItemInfo.Item key)
    {
        if (HaveItem.ContainsKey(key))
        {
            return true;
        }

        return false;
    }
}
