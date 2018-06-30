using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public PlayerHealth playerHealth;//玩家生命值
    public GameObject weapon;//所要生成的武器
    public float spawnWeaponTime = 50f;//生成武器的间隔时间
    public Transform[] spawnWeaponPoints;//生成武器的地点

    public bool weaponExist = false;//判断武器是否已经生成

    private double timer = 0f;//计时器

    private void Awake()
    {
        
    }

    void Start()
    {
        //InvokeRepeating("SpawnWeapon", spawnWeaponTime, spawnWeaponTime);//重复执行生成武器函数
    }

    private void Update()
    {
        
        if (!GameObject.Find(weapon.name + "(Clone)"))
        {
            weaponExist = false;
        }

        //武器不存在时才更新计时器
        if (!weaponExist)
        {
            timer += 0.1;//更新计时器
        }

        //如果大于生成武器的间隔时间，生成武器并重置计时器
        if(timer >= spawnWeaponTime)
        {
            SpawnWeapon();
            timer = 0f;
        }

        //Debug.Log(timer);
    }

    //生成武器函数
    void SpawnWeapon()
    {
        //如果玩家已经死亡则不生成武器
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //随机选取生成地点数组中的某个下标
        int spawnPointIndex = Random.Range(0, spawnWeaponPoints.Length);

        //在某个地点生成武器
        if(!weaponExist)
        {
            Instantiate(weapon, spawnWeaponPoints[spawnPointIndex].position, spawnWeaponPoints[spawnPointIndex].rotation);
            weaponExist = true;
        }
        
    }
}
