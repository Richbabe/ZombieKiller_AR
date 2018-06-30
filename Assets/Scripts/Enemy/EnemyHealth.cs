using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;//敌人初始血量
    public int currentHealth;//敌人当前血量
    public float sinkSpeed = 2.5f;//敌人死完后沉下的速度
    public int scoreValue = 10;//杀死该敌人获得的分数
    public AudioClip deathClip;//敌人死亡时候的音效


    private Animator anim;
    private AudioSource enemyAudio;//敌人受伤时候的音效
    public ParticleSystem hitParticles;//敌人被攻击时候的粒子系统
    public ParticleSystem DeathParticles;//敌人死亡时候的粒子系统
    private CapsuleCollider capsuleCollider;
    private bool isDead;//敌人是否死亡
    private bool isSinking;//敌人是否下沉


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        //hitParticles = GetComponent("HitParticles") as ParticleSystem;//需要在子对象中寻找粒子系统
        capsuleCollider = GetComponent <CapsuleCollider> ();
        //DeathParticles = GetComponent("DeathParticles") as ParticleSystem

        currentHealth = startingHealth;//初始化当前血量
    }


    void Update ()
    {
        //如果敌人死亡且要下沉
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);//令敌人下沉
        }
    }

    //玩家对敌人的伤害，由玩家调用该方法
    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        //如果敌人已经死亡则不用执行任何操作
        if (isDead)
        {
            return;
        }
            
        enemyAudio.Play ();//播放敌人受伤时候的音效

        currentHealth -= amount;//更新敌人当前血量
            
        hitParticles.transform.position = hitPoint;//让粒子系统坐标和击中坐标相同
        hitParticles.Play();//播放粒子系统

        //如果当前血量<=0，则敌人死亡
        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;//通过设置trigger取消capsuleCollider的物理效果，防止其尸体成为障碍物

        anim.SetTrigger ("Dead");//设置敌人animator controller参数，使敌人播放死亡动画

        enemyAudio.clip = deathClip;//设置敌人播放音效为死亡音效
        enemyAudio.Play ();//播放死亡音效

        DeathParticles.Play();//播放死亡粒子系统

        isSinking = true;

        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;//禁用死亡敌人的NavMeshAgent属性
        GetComponent<Rigidbody>().isKinematic = true;//将刚体属性设置成运动学的，即不受物理力控制
        ScoreManager.score += scoreValue;//计分
        Destroy(gameObject, 1f);//在1s后销毁下沉的敌人
    }

}
