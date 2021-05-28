using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShowHaveFossil : MonoBehaviour
{
    public GameObject nodePrefab;
    private List<GameObject> nodeList = new List<GameObject>();
    private List<Text> nodeNameTextList = new List<Text>();
    private List<Text> nodeCountTextList = new List<Text>();
    private Having having;
    private FossilInfo fossilInfo = new FossilInfo();
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

        for (int i = 0; i < fossilInfo.FossilInfoDic.Count; i++)
        {
            if (!having.CheckHadFossil((FossilInfo.FossilSize)Enum.ToObject(typeof(FossilInfo.FossilSize), i % 3), (ItemInfo.pointType)Enum.ToObject(typeof(ItemInfo.pointType), i / 3)) || having.HaveFossil[i].itemCount == 0)
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

            nodeNameTextList[listNum].text = fossilInfo.FossilInfoDic[i].itemName;
            nodeCountTextList[listNum].text = "x" + having.HaveFossil[i].itemCount;
            nodeList[listNum].SetActive(true);
            listNum++;
        }
    }
}
