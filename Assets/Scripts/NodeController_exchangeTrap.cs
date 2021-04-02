using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NodeController_exchangeTrap : MonoBehaviour
{
    public GameObject countUpButtonObj;
    public GameObject countDownButtonObj;
    public Button countUpButton;
    public Button countDownButton;
    public Text itemCountText;
    private Having having;
    private TrapsInfo.Trap key;
    private KeyValuePair<int, TrapsInfo._trap> pair;
    private TrapsInfo trapsInfo = new TrapsInfo();

    private int itemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();

        string itemName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        pair = trapsInfo.trapInfoDic.FirstOrDefault(c => c.Value.itemName == itemName);
        key = (TrapsInfo.Trap)Enum.ToObject(typeof(TrapsInfo.Trap), pair.Key);

        countDownButton.interactable = false;

        int havePoint = 0;
        switch (trapsInfo.trapInfoDic[pair.Key].pointType)
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

        if (havePoint < trapsInfo.trapInfoDic[pair.Key].point)
        {
            countUpButton.interactable = false;
        }
    }

    public void OnClickCountUpButton()
    {
        int havePoint = 0;
        itemCount++;
        having.LosePoint(trapsInfo.trapInfoDic[pair.Key].pointType, trapsInfo.trapInfoDic[pair.Key].point);
        itemCountText.text = "x" + itemCount;

        switch (trapsInfo.trapInfoDic[pair.Key].pointType)
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

        if (havePoint < trapsInfo.trapInfoDic[pair.Key].point)
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
        having.GetPoint(trapsInfo.trapInfoDic[pair.Key].pointType, trapsInfo.trapInfoDic[pair.Key].point);
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
            having.GetTrap(key);
        }
        itemCountText.text = "x" + 0;
    }
}
