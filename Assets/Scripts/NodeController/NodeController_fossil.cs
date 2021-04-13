using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NodeController_fossil : MonoBehaviour
{
    public GameObject throwButtonObj;
    private Having having;
    private ShowHaveFossil showHaveFossil;

    private FossilInfo fossilinfo = new FossilInfo();

    // Start is called before the first frame update
    void Start()
    {
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();
        showHaveFossil = GameObject.FindObjectOfType<ShowHaveFossil>();
        throwButtonObj.SetActive(false);
    }

    public void OnClickItemNode()
    {
        throwButtonObj.SetActive(true);
    }

    public void OnClickThrowButton()
    {
        string itemName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        var pair = fossilinfo.FossilInfoDic.FirstOrDefault(c => c.Value.itemName == itemName);
        FossilInfo.FossilSize key = (FossilInfo.FossilSize)Enum.ToObject(typeof(FossilInfo.FossilSize), pair.Key);
        throwButtonObj.SetActive(false);
        having.ThrowFossil(key,pair.Value.fossilColor);
        showHaveFossil.ShowItem();
    }
}
