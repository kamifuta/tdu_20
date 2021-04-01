using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NodeController_point : MonoBehaviour
{
    public GameObject countUpButtonObj;
    public GameObject countDownButtonObj;
    public Button countUpButton;
    public Button countDownButton;
    public Text itemCountText;
    private Having having;
    private ItemInfo.Item key;

    private int itemCount=0;

    // Start is called before the first frame update
    void Start()
    {
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();

        countDownButton.interactable = false;

        string itemName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        var pair = new ItemInfo().ItemInfoDic.FirstOrDefault(c => c.Value.itemName == itemName);
        key = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), pair.Key);
    }

    public void OnClickCountUpButton()
    {
        itemCount++;
        having.GetPoint(having.HaveItem[(int)key].pointType, having.HaveItem[(int)key].point);
        itemCountText.text = "x" + itemCount;
        if(itemCount== having.HaveItem[(int)key].itemCount)
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
        having.LosePoint(having.HaveItem[(int)key].pointType, having.HaveItem[(int)key].point);
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
}
