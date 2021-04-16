using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowExchangeItem : MonoBehaviour
{
    public GameObject nodePrefab;
    public Text havePointText;
    private List<GameObject> nodeList = new List<GameObject>();
    private List<Text> nodeNameTextList = new List<Text>();
    private List<Text> nodeCountTextList = new List<Text>();
    private List<Text> nodePointTextList = new List<Text>();
    private Having having;
    private ItemInfo itemInfo = new ItemInfo();
    private int listNum = 0;

    private void Awake()
    {
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();
    }

    // Update is called once per frame
    void Update()
    {
        havePointText.text = "赤:" + having.redPoint + "青:" + having.bluePoint + "黄:" + having.yellowPoint + "緑:" + having.greenPoint;
    }

    public void ShowItem()
    {
        listNum = 0;
        foreach (var obj in nodeList)
        {
            obj.SetActive(false);
        }

        for (int i = 0; i < itemInfo.ItemInfoDic.Count; i++)
        {
            if (listNum >= nodeList.Count - 1)
            {
                var node = Instantiate(nodePrefab);
                node.SetActive(false);
                node.transform.SetParent(this.gameObject.transform);
                nodeList.Add(node);
                nodeNameTextList.Add(node.transform.GetChild(0).GetComponent<Text>());
                nodeCountTextList.Add(node.transform.GetChild(1).GetComponent<Text>());
                nodePointTextList.Add(node.transform.GetChild(2).GetComponent<Text>());
            }

            nodeNameTextList[listNum].text = itemInfo.ItemInfoDic[i].itemName;
            nodeCountTextList[listNum].text = "x" + 0;
            if (having.HaveItem.ContainsKey(i))
            {
                nodeCountTextList[listNum].text = "x" + having.HaveItem[i].itemCount;
            }
           
            nodePointTextList[listNum].text = itemInfo.ItemInfoDic[i].point.ToString();
            nodeList[listNum].SetActive(true);
            listNum++;
        }
    }

    public void DesideItem()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<NodeController_item_exchange>().Exchange();
        }
        ShowItem();
    }
}
