using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot;//玩家每一枪的伤害
    public float timeBetweenBullets;//射速
    public float range = 100f;//子弹的射程

    private float timer;//计时器，用来计算是否达到射击时间
    private Ray shootRay = new Ray();//射击射线
    private RaycastHit shootHit;//返回击中物体
    private int shootableMask;//设置可以击中的东西
    private ParticleSystem gunParticles;//枪口火焰例子效果
    private LineRenderer gunLine;//枪的子弹射线
    private AudioSource gunAudio;//开枪音效
    private Light gunLight;//子弹光照效果
    private float effectsDisplayTime = 0.2f;//开枪效果持续时间

    private Animator anim;
    private string gunType;
    private Transform defaultWeapon;//默认为手枪状态
    private int bullet;

    private GameObject player;
    private PlayerWeapon playerWeapon;

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");//返回可以击中对象的层
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        anim = GetComponentInParent<Animator>();
        defaultWeapon = transform.parent.parent.Find("Weapon_Pistol");
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = player.GetComponent<PlayerWeapon>();
        bullet = playerWeapon.gunBullet;
        BulletManager.bulletNumber = bullet;
        //Debug.Log(BulletManager.bulletNumber);
    }

    private void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
    }

    private void OnDisable()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
    }

    private void OnJoystickMove(MovingJoystick move)
    {
        if (move.joystickName != "Fire")
        {
            return;
        }
        float PositionY = -1 * move.joystickAxis.x;
        float PositionX = 1 * move.joystickAxis.y;

        if (PositionX != 0 || PositionY != 0)
        {
            player.GetComponent<PlayerMovement>().FaceWithMove = false;
            player.transform.LookAt(new Vector3(player.transform.position.x + PositionX, player.transform.position.y, player.transform.position.z + PositionY));
            //Debug.Log("aaa");
        }
        else
        {
            return;
        }

        //如果按下开火键（鼠标左键）且时间大于每一枪间隔时间则射击
        if (timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            switch (gunType)
            {
                case "PistolHead":
                    Shoot();
                    anim.SetBool("IsPistolShoot", true);
                    break;

                case "AKHead":
                    Shoot();
                    anim.SetBool("IsAKShoot", true);
                    playerWeapon.gunBullet--;

                    //用完子弹
                    if (playerWeapon.gunBullet == 0)
                    {
                        this.transform.parent.gameObject.SetActive(false);
                        anim.SetBool("IsAKShoot", false);
                        defaultWeapon.gameObject.SetActive(true);
                        playerWeapon.UpdateCurrentGun();//更新当前枪
                        playerWeapon.UpdateGunBullet();//更新当前子弹
                        //Debug.Log(defaultWeapon.name);
                    }
                    //Debug.Log(gunType);
                    break;

                case "ShortgunHead":
                    Shoot();
                    anim.SetBool("IsShortgunShoot", true);
                    playerWeapon.gunBullet--;

                    //用完子弹
                    if (playerWeapon.gunBullet == 0)
                    {
                        this.transform.parent.gameObject.SetActive(false);
                        anim.SetBool("IsShortgunShoot", false);
                        defaultWeapon.gameObject.SetActive(true);
                        playerWeapon.UpdateCurrentGun();//更新当前枪
                        playerWeapon.UpdateGunBullet();//更新当前子弹
                        //Debug.Log(defaultWeapon.name);
                    }
                    //Debug.Log(gunType);
                    break;

                case "MinigunHead":
                    Shoot();
                    anim.SetBool("IsMinigunShoot", true);
                    playerWeapon.gunBullet--;

                    //用完子弹
                    if (playerWeapon.gunBullet == 0)
                    {
                        this.transform.parent.gameObject.SetActive(false);
                        anim.SetBool("IsMinigunShoot", false);
                        defaultWeapon.gameObject.SetActive(true);
                        playerWeapon.UpdateCurrentGun();//更新当前枪
                        playerWeapon.UpdateGunBullet();//更新当前子弹
                        //Debug.Log(defaultWeapon.name);
                    }
                    //Debug.Log(gunType);
                    break;
            }
        }
    }

    void OnJoystickMoveEnd(MovingJoystick move)
    {
        player.GetComponent<PlayerMovement>().FaceWithMove = true;
        if (move.joystickName.Equals("Fire"))
        {
            switch (gunType)
            {
                case "PistolHead":
                    anim.SetBool("IsPistolShoot", false);
                    player.GetComponent<PlayerMovement>().speed = 6.0f;
                    break;
                case "AKHead":
                    anim.SetBool("IsAKShoot", false);
                    //修改玩家速度
                    player.GetComponent<PlayerMovement>().speed = 5.0f;
                    break;
                case "ShortgunHead":
                    anim.SetBool("IsShortgunShoot", false);
                    //修改玩家速度
                    player.GetComponent<PlayerMovement>().speed = 4.8f;
                    break;
                case "MinigunHead":
                    anim.SetBool("IsMinigunShoot", false);
                    //修改玩家速度
                    player.GetComponent<PlayerMovement>().speed = 4.0f;
                    break;
            }
        }
    }

    void Update ()
    {
        timer += Time.deltaTime;//更新计时器

        BulletManager.bulletNumber = playerWeapon.gunBullet;

        //Debug.Log(anim.name);

        gunType = this.name;
        /*
        //如果按下开火键（鼠标左键）且时间大于每一枪间隔时间则射击
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {   
            switch (gunType)
            {
                case "PistolHead":
                    Shoot();
                    anim.SetBool("IsPistolShoot", true);
                    break;

                case "AKHead":
                    Shoot();
                    anim.SetBool("IsAKShoot", true);
                    playerWeapon.gunBullet--;
                    
                    //用完子弹
                    if (playerWeapon.gunBullet == 0)
                    {
                        this.transform.parent.gameObject.SetActive(false);
                        anim.SetBool("IsAKShoot", false);
                        defaultWeapon.gameObject.SetActive(true);
                        playerWeapon.UpdateCurrentGun();//更新当前枪
                        playerWeapon.UpdateGunBullet();//更新当前子弹
                        //Debug.Log(defaultWeapon.name);
                    }
                    //Debug.Log(gunType);
                    break;

                case "ShortgunHead":
                    Shoot();
                    anim.SetBool("IsShortgunShoot", true);
                    playerWeapon.gunBullet--;

                    //用完子弹
                    if (playerWeapon.gunBullet == 0)
                    {
                        this.transform.parent.gameObject.SetActive(false);
                        anim.SetBool("IsShortgunShoot", false);
                        defaultWeapon.gameObject.SetActive(true);
                        playerWeapon.UpdateCurrentGun();//更新当前枪
                        playerWeapon.UpdateGunBullet();//更新当前子弹
                        //Debug.Log(defaultWeapon.name);
                    }
                    //Debug.Log(gunType);
                    break;

                case "MinigunHead":
                    Shoot();
                    anim.SetBool("IsMinigunShoot", true);
                    playerWeapon.gunBullet--;

                    //用完子弹
                    if (playerWeapon.gunBullet == 0)
                    {
                        this.transform.parent.gameObject.SetActive(false);
                        anim.SetBool("IsMinigunShoot", false);
                        defaultWeapon.gameObject.SetActive(true);
                        playerWeapon.UpdateCurrentGun();//更新当前枪
                        playerWeapon.UpdateGunBullet();//更新当前子弹
                        //Debug.Log(defaultWeapon.name);
                    }
                    //Debug.Log(gunType);
                    break;
            }
        }
        else
        {
            switch (gunType)
            {
                case "PistolHead":
                    anim.SetBool("IsPistolShoot", false);
                    player.GetComponent<PlayerMovement>().speed = 6.0f;
                    break;
                case "AKHead":
                    anim.SetBool("IsAKShoot", false);
                    //修改玩家速度
                    player.GetComponent<PlayerMovement>().speed = 5.0f;
                    break;
                case "ShortgunHead":
                    anim.SetBool("IsShortgunShoot", false);
                    //修改玩家速度
                    player.GetComponent<PlayerMovement>().speed = 4.8f;
                    break;
                case "MinigunHead":
                    anim.SetBool("IsMinigunShoot", false);
                    //修改玩家速度
                    player.GetComponent<PlayerMovement>().speed = 4.0f;
                    break;
            }
            
        }
        
        //如果超过开枪效果持续时间，取消开枪效果
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
        */
    }

    //取消开枪效果
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    //玩家射击函数
    void Shoot ()
    {
        timer = 0f;//每次开枪时重置计时器

        gunAudio.Play ();//播放开枪音效

        gunLight.enabled = true;//激活子弹光照效果

        //停止之前枪口火焰粒子效果并重新播放枪口火焰粒子效果
        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;//激活子弹射线
        gunLine.SetPosition (0, transform.position);//射线初始坐标为0，即枪口位置

        shootRay.origin = transform.position;//射线发射点为枪口位置
        shootRay.direction = transform.forward;//射线方向为枪口朝向，即Z轴正方向

        //如果子弹击中物体，其中shootableMask保证击中的物体都在可击中层中
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            //如果击中物体是敌人，则enemyHealth为非空，如果不是敌人（如障碍物）则为空
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);//对敌人造成伤害，shootHit.point为击中点
            }
            gunLine.SetPosition (1, shootHit.point);//设置子弹射线的起始点和终止点
        }
        //如果没有击中物体
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);//设置子弹射线的起始点和终止点，终止点为射程最远处
        }

        Invoke("DisableEffects", 0.05f);
    }

}
