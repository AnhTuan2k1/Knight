using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator playerAni;
    [SerializeField] private WeaponCollider weaponCollider;
    [SerializeField] bool isHolding = false;
    [SerializeField] bool comboPossible = false;
    [SerializeField] bool isAtacking = false;
    [SerializeField] int comboStep = 0;
    [SerializeField] float timeAtk = 0.5f;
    int[] checkUnityError = { 0, 0, 0, 0 };

    const string ATK1 = "Attack01_SwordAndShiled";
    const string ATK2 = "Attack02_SwordAndShiled";
    const string ATK3 = "Attack03_SwordAndShiled";
    const string ATK4 = "Attack04_SwordAndShiled";

    public void HoldingBtnAtk(bool isHolding)
    {
        this.isHolding = isHolding;
    }

    public void BtnAtk()
    {
        if (!isAtacking && comboStep == 0) 
        {
            playerAni.Play(ATK1);
            StartCoroutine(ComboAtk());
            comboStep = 1;
        }

        else if (isAtacking)
        {
            comboPossible = true;
        }
    }

    private void Atk()
    {
        if (comboStep == 1)
        {
            playerAni.Play(ATK2);
            StartCoroutine(ComboAtk());
            comboStep = 2;
        }
        else if (comboStep == 2)
        {
            playerAni.Play(ATK3);
            StartCoroutine(ComboAtk());
            comboStep = 3;
        }
        else if (comboStep == 3)
        {
            playerAni.Play(ATK4);
            StartCoroutine(IsAtk());
            comboStep = 0;
        }
    }

    private IEnumerator ComboAtk()
    {
        isAtacking = true;
        yield return new WaitForSeconds(timeAtk);
        isAtacking = false;

        if (comboPossible || isHolding) Atk();
        else comboStep = 0;

        comboPossible = false;
    }

    private IEnumerator IsAtk()
    {
        isAtacking = true;
        yield return new WaitForSeconds(timeAtk + 0.15f);
        isAtacking = false;

        comboPossible = false;

        if (isHolding)
        {
            BtnAtk();
        }
    }

    // create attack function for button atk
    public void Attack()
    {
        //print("btn press" + comboPossible + "aa" + comboStep);
        if (comboStep == 0)
        {
            playerAni.Play(ATK1);
            comboStep = 1;
            return;
        }

        //write the part that presses button while attacking
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                //print("btn press" + comboPossible + "a" + comboStep);
                comboPossible = false;
                comboStep += 1;
            }
        }

        CheckMissingEventError();
    }

    // for holding on button atk
    public void HoldingBtnAttack(bool isHolding)
    {
        this.isHolding = isHolding;

        //print("holding");
    }

    private void CheckMissingEventError()
    {
        for (int i = 0; i < 4; i++)
        {
            if (checkUnityError[i] > 2)
            {
                ResetCombo();
                checkUnityError[i] = 0;
                print("fixed missing event error step" + i + 1);
            }
            else if (!comboPossible && comboStep == i + 1) checkUnityError[i]++;
            else checkUnityError[i] = 0;
        }
    }

    //create a switch that detects a button being entered during attack
    public void ComboPossible()
    {
        comboPossible = true;

        if (isHolding) Attack();
        //print("Possible for" + comboStep);
    }

    //create a combo attack
    public void Combo()
    {
        //print("do combo" + comboStep);

        if (comboStep == 2)
            playerAni.Play(ATK2); //enter the name of the second attack animation

        if (comboStep == 3)
            playerAni.Play(ATK3); //enter the name of the third attack animation

        if (comboStep == 4)
            playerAni.Play(ATK4); //enter the name of the forth attack animation

    }

    // to reset combo attack
    public void ResetCombo()
    {
        comboPossible = false;
        comboStep = 0;
        if (isHolding) Attack();
        //print("reset combo");
    }

    public void StartAttack()
    {
        weaponCollider.StartAttack();
    }

    public void EndAttack()
    {
        weaponCollider.EndAttack();
    }

}
