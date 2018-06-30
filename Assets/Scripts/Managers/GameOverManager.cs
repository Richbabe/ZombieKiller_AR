using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;//玩家生命值
    public float restartDelay = 5f;//重新开始等待时间


    private Animator anim;
    private float restartTimer;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        //如果玩家死亡
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

            restartTimer += Time.deltaTime;

            if(restartTimer >= restartDelay)
            {
                //Application.LoadLevel(Application.loadedLevel);//重新开始游戏
                SceneManager.LoadScene("Scene1");
                restartTimer = 0;
            }
        }
    }
}
