using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private GameObject menuePanel;
    private GameObject talkPanel;
    private PlayerInput input;
    private Having having;
    private Having_trap having_Trap;
    private MenuButtonManager menuButtonManager;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        menuePanel= GameObject.Find("MenuPanel");
        talkPanel = GameObject.Find("TalkPanel");
        menuButtonManager = GameObject.FindObjectOfType<MenuButtonManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        having = GetComponent<Having>();
        having_Trap = GetComponent<Having_trap>();

        menuePanel.SetActive(false);
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
            having_Trap.GetTrap(TrapsInfo.TrapEnum.fireTrap);
        }

        if (input.PushedMenue)
        {
            OpenMenu();
        }

        if (input.PushedCheck)
        {
            Check();
        }
    }

    private void OpenMenu()
    {
        if (menuButtonManager.canOpenMenu) 
            menuePanel.SetActive(true);
    }

    private void Talk()
    {

    }

    private void Check()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 1.0f))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "DigPoint":
                    break;
                case "Trap":
                    break;
                case "NPC":
                    Talk();
                    break;
            }
        }
    }
}