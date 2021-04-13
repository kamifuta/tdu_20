using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public GameObject actionButtonObj;
    public bool IsAction = false;
    public bool CanOpenMenu = true;

    private GameManager gameManager;
    private GameObject menuePanel;
    private GameObject talkPanel;
    //private GameObject trapPrefab;
    private PlayerInput input;
    private Having having;
    //private MenuButtonManager menuButtonManager;
    private CapsuleCollider collider;
    private Text actionText;
    private GameObject targetTrap;

    private const int wallLayerMask = 1 << 8;
    private const int actionLayerMask = 1 << 9 | 1 << 10 | 1 << 11 | 1 << 12;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        input = GetComponent<PlayerInput>();
        menuePanel= GameObject.Find("MenuPanel");
        talkPanel = GameObject.Find("TalkPanel");
        //trapPrefab = Resources.Load<GameObject>("TrapPrefab");
        //menuButtonManager = GameObject.FindObjectOfType<MenuButtonManager>();
        collider = GetComponent<CapsuleCollider>();

        actionText = actionButtonObj.transform.GetChild(0).GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        having = GetComponent<Having>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            having.GetFossil(FossilInfo.FossilSize.small, ItemInfo.pointType.red);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            having.GetFossil(FossilInfo.FossilSize.small, ItemInfo.pointType.red);
        }

        if (input.PushedSerch)
        {
            having.GetTrap(TrapsInfo.Trap.fireTrap);
        }

        if (input.PushedMenue)
        {
            OpenMenu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            MakeRoomGate();
        }

        Vector3 p1 = transform.position + collider.center - Vector3.up * ((collider.height / 2.0f) - collider.radius);
        Vector3 p2 = p1 + Vector3.up * (collider.height - collider.radius * 2);
        RaycastHit hit;

        if (Physics.CapsuleCast(p1, p2, collider.radius/2.0f, transform.forward, out hit, 1.0f, actionLayerMask) && !IsAction)
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
                case 12:
                    SetActionButton("入る");
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

    public void Put(TrapsInfo.Trap key)
    {
        Vector3 pos = transform.position + transform.forward - Vector3.up * 0.5f;
        Addressables.InstantiateAsync((new TrapsInfo().trapInfoDic[(int)key].prefabAddress), pos, Quaternion.Euler(-90,0,0));
        SendTrapPos(pos);
        IsAction = false;
        CanOpenMenu=true;
    }

    public void Pick()
    {
        var pair = new TrapsInfo().trapInfoDic.FirstOrDefault(c => c.Value.itemName == targetTrap.name);
        TrapsInfo.Trap key = (TrapsInfo.Trap)Enum.ToObject(typeof(TrapsInfo.Trap), pair.Key);
        having.GetTrap(key);
        Destroy(targetTrap);
        IsAction = false;
        CanOpenMenu = true;
    }

    public void StartDigScene()
    {

    }

    public void MoveRoomScene()
    {

    }

    public void MakeRoomGate()
    {
        if (!CheckCanMakeRoom()) return;

        Addressables.InstantiateAsync((new TrapsInfo().trapInfoDic[(int)ItemInfo.Item.RoomMaker].prefabAddress), transform.forward, Quaternion.Euler(-90, 0, 0));
    }

    public bool CheckCanMakeRoom()
    {
        Vector3 p1 = transform.position + collider.center - Vector3.up * ((collider.height / 2.0f) - collider.radius);
        Vector3 p2 = p1 + Vector3.up * (collider.height - collider.radius * 2);
        RaycastHit hit;

        if (Physics.CapsuleCast(p1, p2, collider.radius / 2.0f, transform.forward, out hit, 1.0f, wallLayerMask))
        {
            return true;
        }
        return false;
    }

    public void StopTalk()
    {
        IsAction = false;
    }

    private void OpenMenu()
    {
        if (CanOpenMenu)
        {
            menuePanel.SetActive(true);
            CanOpenMenu = false;
            IsAction = true;
        }
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
        CanOpenMenu = false;
        switch (actionText.text)
        {
            case "話す":
                Talk();
                break;
            case "拾う":
                Pick();
                break;
            case "掘る":
                StartDigScene();
                break;
            case "入る":
                MoveRoomScene();
                break;
        }
    }

    public void SendTrapPos(Vector3 pos)
    {
        gameManager.trapPos.Add(pos);
    }
}