using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private Animator playerAni;
    bool comboPossible;
    int comboStep;
    int[] checkUnityError = { 0, 0, 0, 0 };

    const string ATK1 = "Attack01_SwordAndShiled";
    const string ATK2 = "Attack02_SwordAndShiled";
    const string ATK3 = "Attack03_SwordAndShiled";
    const string ATK4 = "Attack04_SwordAndShiled";

    private void Start()
    {
        playerAni = GetComponent<Animator>();
    }

    // create attack function for button atk
    public void Attack()
    {
        print("btn press" + comboPossible + "aa" + comboStep);
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

    private void CheckMissingEventError()
    {
        for (int i = 0; i < 4; i++)
        {
            if (checkUnityError[i] > 4)
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
        //print("reset combo");
    }
}
