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
    public Text itemCountText;
    private Having having;
    private ShowHaveItem showHaveItem;

    private int itemCount=0;

    // Start is called before the first frame update
    void Start()
    {
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();
        showHaveItem = GameObject.FindObjectOfType<ShowHaveItem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickItemNode()
    {
        countUpButtonObj.SetActive(true);
    }

    public void OnClickCountUpButton()
    {
        itemCount++;
        string itemName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        //ItemInfo.Item key = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), new ItemInfo().ItemName.IndexOf(itemName));
        var pair = new ItemInfo().ItemInfoDic.FirstOrDefault(c => c.Value.itemName == itemName);
        ItemInfo.Item key = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), pair.Key);
        having.ThrowItem(key);
        showHaveItem.ShowItem();
    }
}
