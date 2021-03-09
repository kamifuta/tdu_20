using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsInfo : MonoBehaviour
{
    public Dictionary<int, Trap> trapInfo=new Dictionary<int, Trap>(){
        {0, new FireTrap()},
    };
}
