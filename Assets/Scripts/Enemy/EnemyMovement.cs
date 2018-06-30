using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;//玩家
    PlayerHealth playerHealth;//玩家生命值
    EnemyHealth enemyHealth;//敌人生命值
    private UnityEngine.AI.NavMeshAgent nav;//NavMesh引用


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;//通过Tag找到玩家
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();//返回敌人的NavMesh引用
    }


    void Update ()
    {
        //如果敌人活着或者玩家玩着
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position);//设置NavMesh的目的地
        }
        else
        {
           nav.enabled = false;
        }
    }
}
