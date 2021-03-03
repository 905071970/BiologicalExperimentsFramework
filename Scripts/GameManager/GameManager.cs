using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager self;

	// Use this for initialization
	void Awake () {

		if(self != null)
        {
			GameObject.Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		self = this;

		UIManager.GetInstance.Init();

		//UIManager.GetInstance.ShowPrompt("测试弹窗");

		UIManager.GetInstance.ShowUIToScreen("Sign_in_UI");
	}
	
}
