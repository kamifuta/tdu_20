using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Having having;
    private Having_trap having_Trap;
    // Start is called before the first frame update
    void Start()
    {
        having = GetComponent<Having>();
        having_Trap = GetComponent<Having_trap>();
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
            Debug.Log("jj");
            having_Trap.GetTrap(TrapsInfo.TrapEnum.fireTrap);
        }
    }
}