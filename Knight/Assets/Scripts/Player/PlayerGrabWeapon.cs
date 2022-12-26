using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabWeapon : MonoBehaviour
{
    [SerializeField] private Weapon weapon = null;
    [SerializeField] private bool isMeleeWeapon;
    [SerializeField] private PlayerSetting playerSetting;
    [SerializeField] private GameObject btnGrabWeapon;
    [SerializeField] private GameObject btnSwapWeapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Weapon grabableWeapon = other.GetComponent<Weapon>();
            print("collider weapon ok");
            print(grabableWeapon);
            if (grabableWeapon != null)
            {
                isMeleeWeapon = grabableWeapon.IsMeleeWeapon();
                if (grabableWeapon.GetWeapon() != playerSetting.getWeapon(isMeleeWeapon))
                {
                    btnGrabWeapon.SetActive(true);
                    btnSwapWeapon.SetActive(false);
                    weapon = grabableWeapon;
                }                  
            }           
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            btnGrabWeapon.SetActive(false);
            btnSwapWeapon.SetActive(true);
            weapon = null;
        }
    }

    public void BtnGrabWeapon()
    {
        if (weapon == null) return;

        playerSetting.SetWeapon(isMeleeWeapon, weapon.GetWeapon(), true);
        btnGrabWeapon.SetActive(false);
        btnSwapWeapon.SetActive(true);
        Destroy(weapon.gameObject);
    }

}
