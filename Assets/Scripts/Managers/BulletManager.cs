using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour {

    public static int bulletNumber;//总弹夹子弹数


    Text text;//子弹数量文本


    void Awake()
    {
        text = GetComponent<Text>();
    }


    void Update()
    {
        //Debug.Log(bulletNumber);
        //不是手枪
        if(bulletNumber != 10000)
        {
            text.text = bulletNumber.ToString();
        }
        //是手枪
        else
        {
            text.text = "inf";
        }
    }
}
