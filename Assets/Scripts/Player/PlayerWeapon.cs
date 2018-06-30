using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    public int gunBullet;//武器剩余子弹
    public GameObject weaponSet;
    public GameObject currentGun;

    private void Awake()
    {
        weaponSet = this.transform.Find("Root_jnt/Hips_jnt/Body_jnt/Spine_jnt/UpperArm_Right_jnt/LowerArm_Right_jnt/Hand_Right_jnt/SimpleMilitary_Weapons").gameObject;
        currentGun = weaponSet.transform.Find("Weapon_Pistol").gameObject;
        gunBullet = 10000;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentGun();
    }

    //更新当前所持枪
    public void UpdateCurrentGun()
    {
        foreach (Transform gun in weaponSet.transform)
        {
            if (gun.gameObject.activeSelf)
            {
                currentGun = gun.gameObject;
            }
        }
    }

    //更新子弹
    public void UpdateGunBullet()
    {
        switch (currentGun.name)
        {
            case "Weapon_Pistol":
                gunBullet = 10000;
                break;
            case "Weapon_AssultRifle01":
                gunBullet = 60;
                break;
            case "Weapon_Shotgun":
                gunBullet = 30;
                break;
            case "Weapon_Minigun":
                gunBullet = 150;
                break;
        }
    }
}
