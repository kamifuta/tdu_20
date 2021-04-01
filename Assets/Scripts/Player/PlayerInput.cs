using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 moveVec => new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    public bool PushedSerch => Input.GetKeyDown(KeyCode.Space);
    public bool PushedCheck => Input.GetMouseButtonDown(0);
    public bool PushedMenue => Input.GetKeyDown(KeyCode.Tab);
}
