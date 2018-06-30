using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    void OnEnable()
    {
        //按住Button
        EasyButton.On_ButtonPress += On_ButtonPress;
    }
    private void OnDisable()
    {
        EasyButton.On_ButtonUp -= On_ButtonUp;
    }

    //按下按钮
    void On_ButtonPress(string buttonName)
    {
        switch (buttonName)
        {
            case "StartGame":
                StartGameClick();
                break;
            case "QuitGame":
                QuitGameClick();
                break;
        }
    }

    void On_ButtonUp(string buttonName)
    {

    }

    public void StartGameClick()
    {
        //Debug.Log("startGame");
        SceneManager.LoadScene("Scene1");//进入游戏
    }

    public void QuitGameClick()
    {
        //Debug.Log("closeGame");
        Application.Quit();//退出游戏
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
