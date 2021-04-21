using UniRx;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput input;
    private PlayerAction playerAction;
    private Rigidbody rb;
    private Animator _animator;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        playerAction = GetComponent<PlayerAction>();
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
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
                _animator.SetBool("is_running",true);
                _animator.SetBool("is_walking", false);
            }
            else
            {
                _animator.SetBool("is_walking", true);
                _animator.SetBool("is_running", false);
            }
        }
        else
        {
            _animator.SetBool("is_walking", false);
            _animator.SetBool("is_running", false);
        }
        if (input.moveVec.x != 0 || input.moveVec.z != 0)
        {
            transform.LookAt(transform.position + input.moveVec);
        }

        rb.angularVelocity = Vector3.zero;
    }
}
 