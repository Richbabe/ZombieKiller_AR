using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;//玩家
    public float smoothing = 5f;//摄像机跟随的平滑程度(即摄像机移动速度),防止明显抖动

    private Vector3 offset;//摄像机和玩家之间的偏移值

    bool isPause = false;

    void OnEnable()
    {
        //按住Button
        EasyButton.On_ButtonPress += On_ButtonPress;
    }

    //按下按钮
    void On_ButtonPress(string buttonName)
    {
        switch (buttonName)
        {
            case "Pause":
                Time.timeScale = 0;
                isPause = true;
                break;
            
        }
    }

    void Start()
    {
        //Cursor.visible = false;
        offset = this.transform.position - target.position;//初始化偏移值
    }

    //因为跟随对象为物理对象，因此需要在FixedUpdate中更新摄像机位置,如果使用Update则会和玩家的时间错开
    void FixedUpdate()
    {
        //更新摄像机位置
        Vector3 targetCamPos = target.position + offset;//摄像机目标位置
        this.transform.position = Vector3.Lerp(this.transform.position, targetCamPos, smoothing * Time.deltaTime);//移动摄像机

    }
}
