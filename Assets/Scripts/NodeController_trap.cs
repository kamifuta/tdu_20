using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NodeController_trap : MonoBehaviour
{
    private GameObject trapPanel;
    public GameObject throwButtonObj;
    public GameObject putButtonObj;
    private Having having;
    private ShowHaveTrap showHaveTrap;
    private PlayerAction playerAction;

    // Start is called before the first frame update
    void Start()
    {
        trapPanel = GameObject.Find("HaveTrapPanel");
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();
        showHaveTrap = FindObjectOfType<ShowHaveTrap>();
        playerAction = FindObjectOfType<PlayerAction>();
        throwButtonObj.SetActive(false);
        putButtonObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickItemNode()
    {
        throwButtonObj.SetActive(true);
        putButtonObj.SetActive(true);
    }

    public void OnClickThrowButton()
    {
        string trapName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        var pair = new TrapsInfo().trapName.FirstOrDefault(c => c.Value == trapName);
        TrapsInfo.TrapEnum key = (TrapsInfo.TrapEnum)Enum.ToObject(typeof(TrapsInfo.TrapEnum), pair.Key);
        throwButtonObj.SetActive(false);
        putButtonObj.SetActive(false);
        having.ThrowTrap(key);
        // trapPanel.SetActive(false);
        showHaveTrap.ShowItem();
    }

    public void OnClickPutButton()
    {
        string trapName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        var pair = new TrapsInfo().trapName.FirstOrDefault(c => c.Value == trapName);
        TrapsInfo.TrapEnum key = (TrapsInfo.TrapEnum)Enum.ToObject(typeof(TrapsInfo.TrapEnum), pair.Key);
        throwButtonObj.SetActive(false);
        putButtonObj.SetActive(false);
        having.PutTrap(key);
        trapPanel.SetActive(false);
        playerAction.CanOpenMenu = true;
    }
}
