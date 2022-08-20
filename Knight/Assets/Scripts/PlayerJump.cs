

using System;
using UnityEngine;

class PlayerJump : MonoBehaviour
{
    [SerializeField] private Animator playerAni;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float jumpSpeed = 9f;
    [SerializeField] private float spinJumpSpeed = 9f;
    [SerializeField] private float gravity = 9.8f;
    private Vector3 direction = Vector3.zero;
    private const string normalJump = "JumpFull_Normal_RM_SwordAndShield";
    private const string spinJump = "JumpFull_Spin_RM_SwordAndShield";
    private bool isSpinJump = false;
    private bool spinJumpPossible = false;
    private bool isJump = false;


    private void Update()
    {
        Jump();
        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);
    }

    public void Jump()
    {
        if (isSpinJump) SpinJump(); 
        else if (isJump) NormalJump();
    }

    public void NormalJump()
    {
        if (controller.isGrounded)
        {
            print("jump");
            playerAni.Play(normalJump);

            direction.y = jumpSpeed;
        }
    }

    public void SpinJump()
    {
        if (!controller.isGrounded)
        {
            print("spin jump");
            playerAni.Play(spinJump);

            direction.y = spinJumpSpeed;
            //CanNotSpinJump();
        }
    }

    public void IsJump(bool isJump)
    {
        this.isJump = isJump;
    }

    public void CanSpinJump()
    {
        if (!controller.isGrounded && spinJumpPossible)
            isSpinJump = true;
    }

    public void CanNotSpinJump()
    {
        spinJumpPossible = false;
        isSpinJump = false;
    }

    public void SpinJumpPossible()
    {
        spinJumpPossible = true;
    }
}

