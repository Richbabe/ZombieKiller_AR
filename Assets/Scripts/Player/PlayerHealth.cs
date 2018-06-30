using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;//玩家初始血量
    public int currentHealth;//玩家当前血量
    public Slider healthSlider;//血条Slider游戏对象
    public Image damageImage;//受伤时显示的图片
    public AudioClip deathClip;//玩家死亡时的音效，只播放一次
    public float flashSpeed = 5f;//图片淡出速度
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    private Animator anim;
    private AudioSource playerAudio;
    private PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    private bool isDead;//保存玩家是否死亡
    private bool damaged;//保存玩家是否受伤


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;//设置当前血量为初始血量
    }


    void Update ()
    {
        //如果受伤则让受伤图片闪一下
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);//让受伤图片淡出（变成完全透明）
        }
        damaged = false;
    }

    //给玩家造成伤害函数，由敌人调用，必须是public
    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;//减少血量

        healthSlider.value = currentHealth;//更新血量

        playerAudio.Play ();//播放玩家受伤音效

        //如果玩家死亡
        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        //Debug.Log("Player die!");

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");//设置animator controller参数，播放死亡动画

        playerAudio.clip = deathClip;//设置播放音效为死亡音效
        playerAudio.Play ();//播放死亡音效

        playerMovement.enabled = false;//让playerMovement脚本不能被执行，玩家不能被移动
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
