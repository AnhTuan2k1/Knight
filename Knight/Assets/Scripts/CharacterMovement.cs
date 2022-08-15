using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float speed = 4f;
    private float speedRunning = 6f;
    private int isWalkingHash;
    private int isRunningHash;
    private bool isRunning = false;

    public CharacterController controller;
    public FixedJoystick joystick;
    public Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void Update()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        //Vector3 direction = new Vector3(horizontal, 0, vertical);

        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        bool isWalking = animator.GetBool(isWalkingHash);

        if (direction.magnitude > 0.1f)
        {
            //float targetAngle = 
            if(isRunning) controller.Move(direction * speedRunning * Time.deltaTime);
            else controller.Move(direction * speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(direction);
            
            if(!isWalking) animator.SetBool(isWalkingHash, true);
        }
        else if(isWalking) animator.SetBool(isWalkingHash, false);

        if(animator.GetBool(isWalkingHash) && isRunning)
        {
            animator.SetBool(isRunningHash, true);
            animator.SetBool(isWalkingHash, false);
        } else animator.SetBool(isRunningHash, false);
    }

    public void Run(bool _isRunning)
    {
        isRunning = _isRunning;
    }
}
