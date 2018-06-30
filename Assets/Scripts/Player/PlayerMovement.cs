using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;//玩家速度

    public Texture aimPicture;//准心图片

    private Vector3 movement;//玩家移动
    private Animator anim;//玩家动画状态机
    private Rigidbody playerRigidbody;//玩家刚体
    private int floorMask;//保存layerMask,使得Raycast只照射Floor
    private float camRayLength = 100f;//从摄像机发出的射线长度

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");//返回Floor的LayerMask
        anim = GetComponent<Animator>();//返回玩家Animator的引用
        playerRigidbody = GetComponent<Rigidbody>();//返回玩家Rigidbody的引用
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
        if(move.joystickName != "Direction")
        {
            return;
        }

        float PositionY = -1 * move.joystickAxis.x;
        float PositionX = 1 * move.joystickAxis.y;

        if (PositionX != 0 || PositionY != 0)
        {
            //transform.LookAt(new Vector3(transform.position.x + PositionX, transform.position.y, transform.position.z + PositionY));

            movement.Set(PositionX, 0f, PositionY);

            movement = movement.normalized * speed * Time.deltaTime;//normalized为了保证每个方向（包括斜着移动）速度一样

            playerRigidbody.MovePosition(transform.position + movement);

            anim.SetBool("IsWalking", true);//设置Animator中的IsWalking参数
        }
        else
        {
            anim.SetBool("IsWalking", false);//设置Animator中的IsWalking参数
        }
    }

    private void OnJoystickMoveEnd(MovingJoystick move)
    {
        if (move.joystickName.Equals("Direction"))
        {
            anim.SetBool("IsWalking", false);//设置Animator中的IsWalking参数
        }
    }
    
    //用于物理系统更新（Rigidbody更新）
    private void FixedUpdate()
    {
        /*
        float h = -1.0f * Input.GetAxisRaw("Horizontal");//水平输入(a,d)
        float v = -1.0f * Input.GetAxisRaw("Vertical");//垂直输入(w,s)

        Move(h, v);
        Turning();
        Animating(h, v);     
        */
    }

    //玩家移动
    void Move(float h,float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;//normalized为了保证每个方向（包括斜着移动）速度一样

        playerRigidbody.MovePosition(transform.position + movement);
    }

    //玩家转向(跟随鼠标)
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        //如果射线击中Floor
        if(Physics.Raycast(camRay,out floorHit, camRayLength, floorMask))
        {

            Vector3 playerToMouse = floorHit.point - this.transform.position;//计算玩家朝向方向，即鼠标所在位置和玩家所在位置的差
            playerToMouse.y = 0f;//保证玩家不会倾倒
            //设置玩家朝向
            Quaternion newRotaion = Quaternion.LookRotation(playerToMouse);//旋转到朝向位置的四元数
            playerRigidbody.MoveRotation(newRotaion);//应用旋转四元素

        }
    }

    //设置玩家动画状态机
    void Animating(float h,float v)
    {
        bool walking = h != 0f || v != 0f;//如果h或者v有一个不是0说明玩家在移动
        anim.SetBool("IsWalking", walking);//设置Animator中的IsWalking参数
    }
    /*
    //绘制准心
    void OnGUI()
    {
        Rect rect = new Rect(Input.mousePosition.x - (aimPicture.width >> 1),
            Screen.height - Input.mousePosition.y - (aimPicture.height >> 1),
            aimPicture.width, aimPicture.height);

        GUI.DrawTexture(rect, aimPicture);
    }
    */
}
