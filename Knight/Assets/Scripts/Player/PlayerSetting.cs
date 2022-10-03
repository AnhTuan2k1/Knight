using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MyScene
{
    Shop, DockThing, LoadScene
}

public enum MySword
{
    OHS06,
    Sword12Purple,
}

public enum MyGun
{
    Blaster, Rifle
}

public class PlayerSetting : MonoBehaviour
{
    [SerializeField] private MyScene scene;
    [SerializeField] private int swordIndex = 0;
    [SerializeField] private Sword[] Swords;
    [SerializeField] private int gunIndex = 0;
    [SerializeField] private Gun[] Guns;
    [SerializeField] private PlayerShooting PlayerShooting;
    [SerializeField] private PlayerAttack PlayerAttack;
    [SerializeField] private Weapon DropedWeapon;

    bool isMeleeWeapon = true;

    public Transform femaleTransform;
    public GameObject btnAttack;
    public GameObject btnJump;
    public GameObject btnMove;
    public GameObject btnRotate;
    public GameObject btnDash;
    public GameObject btnShooting;
    public GameObject btnSwapWeapon;
    public GameObject virtualCamera;
    public GameObject canvasPlayerHeath;
    

    private void Start()
    {
        DontDestroyOnLoad(transform.parent);
        SwitchScene(MyScene.DockThing);
        SetWeapon(true, ((int)MySword.OHS06));
        //virtualCamera.enabled = false;
        //canvasInput.enabled = false;
        //player.rotation.E(0, 200, 0);
        //Vector3 quaternion = player.rotation.eulerAngles;
        //quaternion.y = 200;
        //quaternion.x = 0;
        //quaternion.z = 0;
        //player.rotation.eulerAngles = quaternion;
    }

    private void SwitchScene(MyScene scene = MyScene.Shop)
    {
        switch (scene)
        {
            case MyScene.Shop:
                GameObject.FindWithTag("Player").transform.position
                    = new Vector3(959.41f, 748.53f, -781.17f);
                btnAttack.SetActive(false);
                btnJump.SetActive(false);
                btnMove.SetActive(true);
                btnRotate.SetActive(false);
                btnDash.SetActive(false);
                btnShooting.SetActive(false);
                btnSwapWeapon.SetActive(false);
                virtualCamera.SetActive(false);
                canvasPlayerHeath.SetActive(false);
                //canvasInput.GetComponentsInChildren<GameObject>()[2]
                //characterController.enabled = false;
                return;
            case MyScene.DockThing:
                Debug.Log("Dock Thing Scene");
                GameObject.FindWithTag("Player").transform.position = new Vector3(-2, 8, 12);
                btnAttack.SetActive(isMeleeWeapon);
                btnJump.SetActive(true);
                btnMove.SetActive(true);
                btnRotate.SetActive(true);
                btnDash.SetActive(true);
                btnShooting.SetActive(!isMeleeWeapon);
                btnSwapWeapon.SetActive(true);
                virtualCamera.SetActive(true);
                canvasPlayerHeath.SetActive(true);
                return;
            case MyScene.LoadScene:
                break;
    
        }
    }

    public void BtnNext(int sceneIndex)
    {
        scene = (MyScene)sceneIndex;
        SceneManager.LoadSceneAsync(sceneIndex);
        SwitchScene(scene);
    }

    public void SetScene(MyScene scene)
    {
        this.scene = scene;
        SwitchScene(scene);
    }

    public void BtnSwapWeapon()
    {
        isMeleeWeapon = !isMeleeWeapon;

        btnAttack.SetActive(isMeleeWeapon);
        Swords[swordIndex].gameObject.SetActive(isMeleeWeapon);

        btnShooting.SetActive(!isMeleeWeapon);
        Guns[gunIndex].gameObject.SetActive(!isMeleeWeapon);

        if (isMeleeWeapon) PlayerAttack.SetSword(Swords[swordIndex]);
        else PlayerShooting.SetGun(Guns[gunIndex]);

    }

    public void SetWeapon(bool isMelee, int weaponIndex, bool isInstantiate = false)
    {
        if (isMeleeWeapon && !isMelee) BtnSwapWeapon();

        // drop weapon
        if(isInstantiate)
        {
            Vector3 position = new Vector3(femaleTransform.position.x,
                femaleTransform.position.y + 0.3f, femaleTransform.position.z);

            Quaternion rotation = new Quaternion(femaleTransform.rotation.x,
                femaleTransform.rotation.y, femaleTransform.rotation.z, femaleTransform.rotation.w);

            if (isMelee)
            {
                DropedWeapon = Instantiate(Swords[swordIndex],position, rotation);
                DropedWeapon.GetComponent<MeshCollider>().enabled = true;
            }
            else
            {
                DropedWeapon = Instantiate(Guns[gunIndex], position, rotation);
            }
        }


        //change weapon
        if (isMeleeWeapon)
        { 
            Swords[swordIndex].gameObject.SetActive(false);

            swordIndex = weaponIndex;
            Swords[swordIndex].gameObject.SetActive(true);

            PlayerAttack.SetSword(Swords[swordIndex]);
        }
        else
        {
            Guns[gunIndex].gameObject.SetActive(false);

            gunIndex = weaponIndex;
            Guns[gunIndex].gameObject.SetActive(true);

            PlayerShooting.SetGun(Guns[gunIndex]);
        }
    }

    public int getWeapon(bool isMelee)
    {
        if (isMelee) return swordIndex;
        else return gunIndex;
    }
}
