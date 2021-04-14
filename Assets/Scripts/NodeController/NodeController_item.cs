using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NodeController_item : MonoBehaviour
{
    public GameObject throwButtonObj;
    private Having having;
    private ShowHaveItem showHaveItem;

    private ItemInfo itemInfo = new ItemInfo();

    // Start is called before the first frame update
    void Start()
    {
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();
        showHaveItem = GameObject.FindObjectOfType<ShowHaveItem>();
        throwButtonObj.SetActive(false);
    }

    public void OnClickItemNode()
    {
        throwButtonObj.SetActive(true);
    }

    public void OnClickThrowButton()
    {
        string itemName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        var pair =itemInfo.ItemInfoDic.FirstOrDefault(c => c.Value.itemName == itemName);
        ItemInfo.Item key = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), pair.Key);
        throwButtonObj.SetActive(false);
        having.ThrowItem(key);
        showHaveItem.ShowItem();
    }
}
