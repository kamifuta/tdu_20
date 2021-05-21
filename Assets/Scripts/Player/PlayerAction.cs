using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public GameObject actionButtonObj;
    public bool IsAction = false;
    public bool CanOpenMenu = true;
    //public bool IsMining = false;

    public CameraController cameraController;
    private GameManager gameManager;
    private GameObject menuePanel;
    private GameObject talkPanel;
    //private GameObject trapPrefab;
    private PlayerInput input;
    private PlayerAction targetPlayerAction;
    private Having having;
    //private MenuButtonManager menuButtonManager;
    private CapsuleCollider collider;
    public Text actionText;
    private GameObject targetTrap;
    private CancellationToken token;
    private TrapsInfo trapsInfo = new TrapsInfo();
    private ItemInfo itemInfo = new ItemInfo();
    public SetCustomPropertiesManager propertiesManager;
    private const int wallLayerMask = 1 << 8;
    private const int actionLayerMask = 1 << 9 | 1 << 10 | 1 << 11 | 1 << 12|1<<16;
    public bool digMaster=false;
    public GameObject talkToPlayerObj;
    public Player talkToPlayer;
    public GameObject digSceneManagerObj;
    public DigSceneManager digSceneManager;

    private void Awake()
    {
        actionButtonObj = GameObject.Find("ActionButton");
        gameManager = GameObject.FindObjectOfType<GameManager>();
        input = GetComponent<PlayerInput>();
        menuePanel= GameObject.Find("MenuPanel");
        talkPanel = GameObject.Find("TalkPanel");
        //trapPrefab = Resources.Load<GameObject>("TrapPrefab");
        //menuButtonManager = GameObject.FindObjectOfType<MenuButtonManager>();
        collider = GetComponent<CapsuleCollider>();
        //Debug.Log(actionButtonObj.transform.GetChild(0));
        actionText = actionButtonObj.transform.GetChild(0).GetComponent<Text>();
        cameraController = GameObject.Find("Player Camera").GetComponent<CameraController>();
        cameraController.enabled = true;
        propertiesManager = FindObjectOfType<SetCustomPropertiesManager>();
        digSceneManagerObj = GameObject.Find("DigSceneManager");
        digSceneManager= digSceneManagerObj.GetComponent<DigSceneManager>();
        digSceneManager.enabled = true;
        digSceneManager.playerAction = GetComponent<PlayerAction>();
        token = this.GetCancellationTokenOnDestroy();
    }

    // Start is called before the first frame update
    void Start()
    {
        having = GetComponent<Having>();

        gameManager.ObserveEveryValueChanged(x => x.isDigScene)
            .Where(x => x == false)
            .Subscribe(_ =>
            {
                CanOpenMenu = true;
                IsAction = false;
            })
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            having.GetItem(ItemInfo.Item.RoomMaker);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            having.GetFossil(FossilInfo.FossilSize.small, ItemInfo.pointType.red);
        }

        if (input.PushedSerch)
        {
            having.GetTrap(TrapsInfo.Trap.LeftMoveTrap);
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
            if (targetPlayerAction == null)
            {
                targetPlayerAction = hit.collider.gameObject.GetComponent<PlayerAction>();
            }
            if (talkToPlayerObj==null)
            {
                talkToPlayerObj = hit.collider.gameObject;
            }

            switch (hit.collider.gameObject.layer)
            {
                case 9:
                    SetActionButton("NPCに話す");
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
                case 16:
                    SetActionButton("話す");
                    break;
            }
        }
        else
        {
            targetPlayerAction = null;
            targetTrap = null;
            actionButtonObj.SetActive(false);
            talkToPlayerObj = null;
        }
       
    }

    public void Talk()
    {
        talkPanel.SetActive(true);
    }

    public void Put(TrapsInfo.Trap key)
    {
        Vector3 pos = transform.position + transform.forward - Vector3.up * 0.5f;
        Addressables.InstantiateAsync((trapsInfo.trapInfoDic[(int)key].prefabAddress), pos, Quaternion.Euler(-90,0,0));
        SendTrapPos(pos);
        IsAction = false;
        CanOpenMenu=true;
    }

    public void Pick()
    {
        var pair = trapsInfo.trapInfoDic.FirstOrDefault(c => c.Value.itemName == targetTrap.name);
        TrapsInfo.Trap key = (TrapsInfo.Trap)Enum.ToObject(typeof(TrapsInfo.Trap), pair.Key);
        having.GetTrap(key);
        Destroy(targetTrap);
        IsAction = false;
        CanOpenMenu = true;
    }

    public void StartDigScene()
    {
        //digMaster = false;
        talkToPlayer = null;
        gameManager.isDigScene = true;
    }

    public void TalkToOtherPlayer()
    {
        talkToPlayer= talkToPlayerObj.GetComponent<PhotonView>().Owner;
        Debug.Log("talkToPlayer: "+ talkToPlayer);
        if (talkToPlayer.CustomProperties[propertiesManager.digKey]is bool digbool)
        {
            digMaster = digbool;
            Debug.Log("digMaster" + digMaster);
        }
        

        //旗を持っていたら
        /*if ()
        {
            //旗取る
        }*/
        
        if(targetPlayerAction != null && digMaster)
        {
            gameManager.isDigScene = true;
        }


        
    }

    public void MoveRoomScene()
    {

    }

    public async void MakeRoomGate()
    {
        if (!CheckCanMakeRoom()) return;
        if (!await CheckRealyMakeRoom()) return;

        await Addressables.InstantiateAsync("RoomGate", transform.position+transform.forward*1.5f, Quaternion.Euler(0, 0, 0));
        having.ThrowItem(ItemInfo.Item.RoomMaker);
    }

    public bool CheckCanMakeRoom()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position,transform.forward,out hit,1.0f,wallLayerMask))
        {
            return true;
        }
        return false;
    }

    public async UniTask<bool> CheckRealyMakeRoom()
    {
        if (await gameManager.SetTextPanelAsync(token, text: "本当にここに基地を作りますか？")) 
            return true;
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
        IsAction = true;
        CanOpenMenu = false;
        Debug.Log(actionText);
        Debug.Log(actionText.text);

        switch (actionText.text)
        {
            case "NPCに話す":
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
            case "話す":
                TalkToOtherPlayer();
                break;
            
        }
    }

    public void SendTrapPos(Vector3 pos)
    {
        gameManager.trapPos.Add(pos);
    }
}