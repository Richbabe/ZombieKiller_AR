using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;//敌人每次攻击的间隔时间
    public int attackDamage = 10;//每次攻击给玩家带来的伤害


    private Animator anim;
    private GameObject player;
    private PlayerHealth playerHealth;//玩家生命值
    EnemyHealth enemyHealth;//敌人生命值
    private bool playerInRange;//判断玩家是否在攻击范围内
    private float timer;//计时器，用来判断每次攻击后是否达到下一次攻击的间隔时间


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

    
    void OnTriggerEnter (Collider other)
    {
        //如果敌人与玩家发生碰撞
        if (other.gameObject == player)
        {
            playerInRange = true;//设置可攻击
        }
    }


    void OnTriggerExit (Collider other)
    {
        //如果敌人与玩家停止碰撞
        if (other.gameObject == player)
        {
            playerInRange = false;//设置不可攻击
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;//更新计时器

        //如果时间大于攻击间隔且可攻击且敌人还活着则攻击玩家
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        //如果玩家生命值小于等于0则死亡且敌人在玩家旁边，让敌人的animator controller从move状态到eating状态
        if(playerHealth.currentHealth <= 0)
        {
            var dist = Vector3.Distance(this.transform.position, player.transform.position);
            //Debug.Log(dist);
            if(dist < 5.0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }
    }

    //攻击函数
    void Attack ()
    {
        timer = 0f;//在每次攻击后重置计时器

        //判断玩家是否还活着，如果活着就造成伤害
        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
