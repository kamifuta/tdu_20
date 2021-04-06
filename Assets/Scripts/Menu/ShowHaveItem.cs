﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHaveItem : MonoBehaviour
{
    public GameObject nodePrefab;
    private List<GameObject> nodeList = new List<GameObject>();
    private List<Text> nodeNameTextList = new List<Text>();
    private List<Text> nodeCountTextList = new List<Text>();
    private Having having;
    private int listNum = 0;

    private void Awake()
    {
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowItem()
    {
        listNum = 0;
        foreach(var obj in nodeList)
        {
            obj.SetActive(false);
        }

        for(int i = 0; i < new ItemInfo().ItemInfoDic.Count; i++)
        {
            //ItemInfo.Item n = (ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), i);
            if (!having.CheckHadItem((ItemInfo.Item)Enum.ToObject(typeof(ItemInfo.Item), i)) || having.HaveItem[i].itemCount == 0)
            {
                continue;
            }

            if (listNum >= nodeList.Count - 1)
            {
                var node = Instantiate(nodePrefab);
                node.SetActive(false);
                node.transform.SetParent(this.gameObject.transform);
                nodeList.Add(node);
                nodeNameTextList.Add(node.transform.GetChild(0).GetComponent<Text>());
                nodeCountTextList.Add(node.transform.GetChild(1).GetComponent<Text>());
            }

            nodeNameTextList[listNum].text = new ItemInfo().ItemInfoDic[i].itemName;
            nodeCountTextList[listNum].text = "x" + having.HaveItem[i].itemCount;
            nodeList[listNum].SetActive(true);
            listNum++;
        }
    }
}
