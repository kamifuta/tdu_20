using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;

public class ShowHaveTrap : MonoBehaviour
{
    public GameObject nodePrefab;
    private List<GameObject> nodeList = new List<GameObject>();
    private List<Text> nodeNameTextList = new List<Text>();
    private List<Text> nodeCountTextList = new List<Text>();
    private Having having;
    private TrapsInfo trapsInfo = new TrapsInfo();
    private int listNum = 0;
    private GameObject[] havings;

    private void Awake()
    {
        havings = GameObject.FindGameObjectsWithTag("Player");
        foreach (var a in havings)
        {
            if (a.GetComponent<PhotonView>().IsMine)
            {
                having = a.GetComponent<Having>();
            }
        }
    }

    public void ShowItem()
    {
        listNum = 0;
        foreach (var obj in nodeList)
        {
            obj.SetActive(false);
        }

        for (int i = 0; i < trapsInfo.trapInfoDic.Count; i++)
        {
            if (!having.CheckHadTrap((TrapsInfo.Trap)Enum.ToObject(typeof(TrapsInfo.Trap), i)) || having.HaveTrap[i].itemCount == 0)
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

            nodeNameTextList[listNum].text = trapsInfo.trapInfoDic[i].itemName;
            nodeCountTextList[listNum].text = "x" + having.HaveTrap[i].itemCount;
            nodeList[listNum].SetActive(true);
            listNum++;
        }
    }
}
