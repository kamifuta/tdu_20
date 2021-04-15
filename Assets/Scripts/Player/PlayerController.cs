using UniRx;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput input;
    private PlayerAction playerAction;
    private Rigidbody rb;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        playerAction = GetComponent<PlayerAction>();
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (input.moveVec != Vector3.zero)
        {
            rb.velocity = input.moveVec.normalized;
            if (input.PushedDash)
            {
                rb.velocity = input.moveVec.normalized * 2;
            }
        }

        if (input.moveVec.x != 0 || input.moveVec.z != 0)
        {
            transform.LookAt(transform.position + input.moveVec);
        }

        rb.angularVelocity = Vector3.zero;
    }
}
 