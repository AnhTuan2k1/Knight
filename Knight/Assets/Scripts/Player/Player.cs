using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private Animator playerAni;
    [SerializeField] private PlayerAttack playerAttack;

    private const float maxHealth = 1000;
    private static string getHit = "GetHit01_SwordAndShield";

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealth(currentHealth / maxHealth);

        if (currentHealth > 0)
            PlayAnimatorGetHit();
        else DestroyObj();

    }
    public void UpdateHealth(float fraction) => healthBar.fillAmount = fraction;


    private void DestroyObj()
    {
        //isDie = true;

        //gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
        //playerAni.Play(die);

        //Destroy(gameObject, timeDie);
    }

    void PlayAnimatorGetHit()
    {
        playerAni.Play(getHit);
        playerAttack.EndAttack();
    }
}
