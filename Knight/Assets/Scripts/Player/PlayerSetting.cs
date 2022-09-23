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
}

public enum MyGun
{
    Blaster, Rifle
}

public class PlayerSetting : MonoBehaviour
{
    [SerializeField] private MyScene scene;
    [SerializeField] private int swordIndex = 0;
    [SerializeField] private GameObject[] Swords;
    [SerializeField] private int gunIndex = 0;
    [SerializeField] private Gun[] Guns;
    [SerializeField] private PlayerShooting PlayerShooting;
    bool isMeleeWeapon = true;

    public Transform player;
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
        DontDestroyOnLoad(player);
        SwitchScene();
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

    private void SwitchScene()
    {
        switch (scene)
        {
            case MyScene.Shop:
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
                return;
        }
    }

    public void BtnNext(int sceneIndex)
    {
        scene = (MyScene)sceneIndex;
        SceneManager.LoadSceneAsync(sceneIndex);
        SwitchScene();
    }

    public void SetScene(MyScene scene)
    {
        this.scene = scene;
        SwitchScene();
    }

    public void BtnSwapWeapon()
    {
        isMeleeWeapon = !isMeleeWeapon;

        btnAttack.SetActive(isMeleeWeapon);
        Swords[swordIndex].SetActive(isMeleeWeapon);

        btnShooting.SetActive(!isMeleeWeapon);
        Guns[gunIndex].gameObject.SetActive(!isMeleeWeapon);

        if (!isMeleeWeapon) PlayerShooting.SetGun(Guns[gunIndex]);
    }

    private void SetWeapon(bool isMelee, int weaponIndex)
    {
        if (isMeleeWeapon && !isMelee) BtnSwapWeapon();

        if (isMeleeWeapon)
        {
            Swords[swordIndex].SetActive(false);

            swordIndex = weaponIndex;
            Swords[swordIndex].SetActive(true);
        }
        else
        {
            Guns[gunIndex].gameObject.SetActive(false);

            gunIndex = weaponIndex;
            Guns[gunIndex].gameObject.SetActive(true);

            PlayerShooting.SetGun(Guns[gunIndex]);
        }
    }
}
