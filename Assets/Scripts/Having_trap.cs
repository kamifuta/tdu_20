using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Having_trap : MonoBehaviour
{
    public class _trap
    {
        public string itemName;
        public int itemCount;
        public _trap(string name, int count)
        {
            itemName = name;
            itemCount = count;
        }
    }

    private TrapsInfo trapInfo = new TrapsInfo();
    public Dictionary<int, _trap> HaveTrap = new Dictionary<int, _trap>();
    public GameObject trapPrefab;

    public void GetTrap(TrapsInfo.TrapEnum key)
    {
        if (CheckHadTrap(key))
        {
            HaveTrap[(int)key].itemCount++;
        }
        else
        {
            _trap trap = new _trap(trapInfo.trapName[(int)key], 1);
            HaveTrap.Add((int)key, trap);
        }
    }

    public void PutTrap(TrapsInfo.TrapEnum key)
    {
        if (HaveTrap[(int)key].itemCount > 0)
        {
            HaveTrap[(int)key].itemCount--;
        }

        Instantiate(trapPrefab, transform.position + Vector3.forward, Quaternion.identity);
    }

    public void ThrowTrap(TrapsInfo.TrapEnum key)
    {
        if (HaveTrap[(int)key].itemCount > 0)
        {
            HaveTrap[(int)key].itemCount--;
        }
    }

    public bool CheckHadTrap(TrapsInfo.TrapEnum key)
    {
        if (HaveTrap.ContainsKey((int)key))
        {
            return true;
        }

        return false;
    }

}
