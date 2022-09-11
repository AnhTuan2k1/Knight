using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float speedRunning = 6f;
    //[SerializeField] private float speedAttacking = 0.1f;
    private int isWalkingHash;
    private int isRunningHash;
    //private int atk1;
    //private int atk2;
    //private int atk3;
    //private int atk4;
    private bool isRunning = false;
    //private bool isAttacking = false;

    public CharacterController controller;
    public VariableJoystick joystick;   
    public Animator animator;
    public Vector3 direction;
    [SerializeField] private CinemachineVirtualCamera cam;

    private void Start()
    {
        //controller = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        //atk1 = Animator.StringToHash("Attack01_SwordAndShiled");
        //atk2 = Animator.StringToHash("Attack02_SwordAndShiled");
        //atk3 = Animator.StringToHash("Attack03_SwordAndShiled");
        //atk4 = Animator.StringToHash("Attack04_SwordAndShiled");
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        //if (horizontal != 0 && vertical != 0)
        //{
        //    isAttacking = IsAttacking(animator.GetCurrentAnimatorStateInfo(0).shortNameHash);
        //}


        direction = new Vector3(horizontal, 0, vertical);
        bool isWalking = animator.GetBool(isWalkingHash);
       
        if (direction.magnitude > 0.1f)
        {
            // adjust movement vector follow camera's direction         
            direction = Quaternion.AngleAxis(cam.transform.eulerAngles.y, Vector3.up) * direction;

            //float targetAngle = 
            if (isRunning) controller.Move(direction * speedRunning * Time.deltaTime);
            //else if(isAttacking) controller.Move(direction * speedAttacking * Time.deltaTime);
            else controller.Move(direction * speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(direction);

            if (!isWalking) animator.SetBool(isWalkingHash, true);
        }
        else if(isWalking) animator.SetBool(isWalkingHash, false);

        if(animator.GetBool(isWalkingHash) && isRunning)
        {
            animator.SetBool(isRunningHash, true);
            animator.SetBool(isWalkingHash, false);
        } else animator.SetBool(isRunningHash, false);
    }

    //private bool IsAttacking(int shortNameHash)
    //{

    //    if (shortNameHash == atk1) return true;
    //    else if (shortNameHash == atk2) return true;
    //    else if (shortNameHash == atk3) return true;
    //    else if (shortNameHash == atk4) return true;

    //    return false;
    //}


    public void Run(bool _isRunning)
    {
        isRunning = _isRunning;
    }
}
