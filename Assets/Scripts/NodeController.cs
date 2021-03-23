using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NodeController : MonoBehaviour
{
    private GameObject itemPanel;
    public GameObject throwButtonObj;
    private Having having;

    // Start is called before the first frame update
    void Start()
    {
        itemPanel = GameObject.Find("HaveItemPanel");
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();
        throwButtonObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickItemNode()
    {
        throwButtonObj.SetActive(true);
    }

    public void OnClickThrowButton()
    {
        string itemName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        //ItemInfo.Item key = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), new ItemInfo().ItemName.IndexOf(itemName));
        var pair = new TrapsInfo().trapName.FirstOrDefault(c => c.Value == itemName);
        ItemInfo.Item key = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), pair.Key);
        throwButtonObj.SetActive(false);
        having.ThrowItem(key);
        itemPanel.SetActive(false);
    }
}
