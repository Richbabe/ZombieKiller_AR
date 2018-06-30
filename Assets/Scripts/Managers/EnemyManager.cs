using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;//玩家生命值
    public GameObject enemy;//敌人
    public float spawnTime = 3f;//生成敌人的间隔时间
    public Transform[] spawnPoints;//生成敌人的地点


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);//重复执行生成敌人函数
    }

    //生成敌人函数
    void Spawn ()
    {
        //如果玩家已经死亡则不生成敌人
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        //随机选取生成地点数组中的某个下标
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        //在某个地点生成敌人
        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
