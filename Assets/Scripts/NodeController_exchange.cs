using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NodeController_exchange : MonoBehaviour
{
    public GameObject countUpButtonObj;
    public GameObject countDownButtonObj;
    public Button countUpButton;
    public Button countDownButton;
    public Text itemCountText;
    private Having having;
    private ItemInfo.Item key;
    private KeyValuePair<int, ItemInfo._item> pair;
    private ItemInfo itemInfo = new ItemInfo();

    private int itemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();

        string itemName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        pair = itemInfo.ItemInfoDic.FirstOrDefault(c => c.Value.itemName == itemName);
        key = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), pair.Key);

        countDownButton.interactable = false;

        int havePoint = 0;
        switch (itemInfo.ItemInfoDic[pair.Key].pointType)
        {
            case ItemInfo.pointType.red:
                havePoint = having.redPoint;
                break;
            case ItemInfo.pointType.blue:
                havePoint = having.bluePoint;
                break;
            case ItemInfo.pointType.yellow:
                havePoint = having.yellowPoint;
                break;
            case ItemInfo.pointType.green:
                havePoint = having.greenPoint;
                break;
        }

        if (havePoint < itemInfo.ItemInfoDic[pair.Key].point)
        {
            countUpButton.interactable = false;
        }
    }

    public void OnClickCountUpButton()
    {
        int havePoint = 0;
        itemCount++;
        having.LosePoint(itemInfo.ItemInfoDic[pair.Key].pointType, itemInfo.ItemInfoDic[pair.Key].point);
        itemCountText.text = "x" + itemCount;

        switch (itemInfo.ItemInfoDic[pair.Key].pointType)
        {
            case ItemInfo.pointType.red:
                havePoint = having.redPoint;
                break;
            case ItemInfo.pointType.blue:
                havePoint = having.bluePoint;
                break;
            case ItemInfo.pointType.yellow:
                havePoint = having.yellowPoint;
                break;
            case ItemInfo.pointType.green:
                havePoint = having.greenPoint;
                break;
        }

        if (havePoint < itemInfo.ItemInfoDic[pair.Key].point)
        {
            countUpButton.interactable = false;
        }

        if (countDownButton.interactable == false)
        {
            countDownButton.interactable = true;
        }
    }

    public void OnClickCountDownButton()
    {
        itemCount--;
        having.GetPoint(itemInfo.ItemInfoDic[pair.Key].pointType, itemInfo.ItemInfoDic[pair.Key].point);
        itemCountText.text = "x" + itemCount;

        if (itemCount == 0)
        {
            countDownButton.interactable = false;
        }

        if (countUpButton.interactable == false)
        {
            countUpButton.interactable = true;
        }
    }

    public void Exchange()
    {
        for (int i = 0; i < itemCount; i++)
        {
            having.GetItem(key);
        }
        itemCountText.text = "x" + 0;
    }
}
