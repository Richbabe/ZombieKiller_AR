using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicManager : MonoBehaviour {
    public GameObject backGroundMusic;
    public static bool isHaveGBM = false;
    private GameObject clone;

	// Use this for initialization
	void Start () {
        //如果没有背景音乐则创建
        if (!isHaveGBM)
        {
            clone = Instantiate(backGroundMusic, transform.position, transform.rotation) as GameObject;
        }
        DontDestroyOnLoad(clone);//在切换场景后保存BGM
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
