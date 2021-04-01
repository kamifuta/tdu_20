using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public GameObject actionButtonObj;
    public bool IsAction = false;
    public bool CanOpenMenu = true;

    private GameObject menuePanel;
    private GameObject talkPanel;
    private GameObject trapPrefab;
    private PlayerInput input;
    private Having having;
    private MenuButtonManager menuButtonManager;
    private CapsuleCollider collider;
    private Text actionText;
    private GameObject targetTrap;
    

    private const int actionLayerMask = 1 << 9 | 1 << 10 | 1 << 11;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        menuePanel= GameObject.Find("MenuPanel");
        talkPanel = GameObject.Find("TalkPanel");
        trapPrefab = Resources.Load<GameObject>("TrapPrefab");
        menuButtonManager = GameObject.FindObjectOfType<MenuButtonManager>();
        collider = GetComponent<CapsuleCollider>();

        actionText = actionButtonObj.transform.GetChild(0).GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        having = GetComponent<Having>();

        menuePanel.SetActive(false);
        //talkPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            having.GetItem(ItemInfo.Item.A);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            having.GetItem(ItemInfo.Item.B);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            having.GetItem(ItemInfo.Item.C);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            having.GetTrap(TrapsInfo.TrapEnum.fireTrap);
        }

        if (input.PushedMenue)
        {
            OpenMenu();
        }

        Vector3 p1 = transform.position + collider.center - Vector3.up * ((collider.height / 2.0f) - collider.radius);
        Vector3 p2 = p1 + Vector3.up * (collider.height - collider.radius * 2);
        RaycastHit hit;
        if (Physics.CapsuleCast(p1, p2, collider.radius, transform.forward, out hit, 0.5f, actionLayerMask) && !IsAction)
        {
            switch (hit.collider.gameObject.layer)
            {
                case 9:
                    SetActionButton("話す");
                    break;
                case 10:
                    targetTrap = hit.collider.gameObject;
                    SetActionButton("拾う");
                    break;
                case 11:
                    SetActionButton("掘る");
                    break;
            }
        }
        else
        {
            targetTrap = null;
            actionButtonObj.SetActive(false);
        }
    }

    public void Talk()
    {
        talkPanel.SetActive(true);
    }

    public void Put(TrapsInfo.TrapEnum key)
    {
        var trap=Instantiate(trapPrefab, transform.position + transform.forward - Vector3.up * 0.5f, Quaternion.identity);
        trap.name = new TrapsInfo().trapName[(int)key];
    }

    public void Pick()
    {
        var pair = new TrapsInfo().trapName.FirstOrDefault(c => c.Value == targetTrap.name);
        TrapsInfo.TrapEnum key = (TrapsInfo.TrapEnum)Enum.ToObject(typeof(TrapsInfo.TrapEnum), pair.Key);
        having.GetTrap(key);
        Destroy(targetTrap);
        IsAction = false;
    }

    public void StopTalk()
    {
        IsAction = false;
    }

    private void OpenMenu()
    {
        if (CanOpenMenu) 
            menuePanel.SetActive(true);
    }

    private void SetActionButton(string actionName)
    {
        //canAction = false;
        actionText.text = actionName;
        actionButtonObj.SetActive(true);
    }

    public void OnClickActionButton()
    {
        Debug.Log(actionText.text);
        IsAction = true;
        switch (actionText.text)
        {
            case "話す":
                Talk();
                break;
            case "拾う":
                Pick();
                break;
        }
    }
}