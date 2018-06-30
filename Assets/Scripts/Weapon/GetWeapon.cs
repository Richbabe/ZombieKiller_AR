using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeapon : MonoBehaviour {
    private GameObject player;
    private GameObject weaponSet;
    private string weaponType;
    private PlayerWeapon playerWeapon;

    public bool weaponIsDestoryed = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = player.GetComponent<PlayerWeapon>();
        weaponSet = player.transform.Find("Root_jnt/Hips_jnt/Body_jnt/Spine_jnt/UpperArm_Right_jnt/LowerArm_Right_jnt/Hand_Right_jnt/SimpleMilitary_Weapons").gameObject;
        //Debug.Log(weaponSet.name);
        string temp = "(Clone)";
        weaponType = this.name.Replace(temp,"");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gunRotate();
	}

    private void OnTriggerEnter(Collider other)
    {
        //如果玩家获得武器
        if (other.gameObject == player)
        {
            //Debug.Log("Get weapon!");
            //更新武器
            playerWeapon.currentGun.SetActive(false);
            weaponSet.transform.Find(weaponType).gameObject.SetActive(true);
            playerWeapon.UpdateCurrentGun();

            //更新子弹
            playerWeapon.UpdateGunBullet();

            //销毁道具
            DestroyObject(gameObject);
            weaponIsDestoryed = true;
        }
    }

    private void gunRotate()
    {
        this.transform.Rotate(Vector3.up * Time.deltaTime * 100);
    }
}
