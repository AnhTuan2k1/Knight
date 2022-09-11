using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerMovement moveScript;
    [SerializeField] private Animator playerAni;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float dashSpeed = 20;
    [SerializeField] private float dasTime = 0.15f;
    const string dash = "SprintFWD_Battle_InPlace_SwordAndShield 0";

    public void BtnDash()
    {
        StartCoroutine(Dash());
    }

    public IEnumerator Dash()
    {
        if(moveScript.direction.magnitude != 0)
        {
            playerAni.Play(dash);
            float time = Time.time;
            while (Time.time < time + dasTime)
            {
                moveScript.controller.Move(moveScript.direction * dashSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

     
}
