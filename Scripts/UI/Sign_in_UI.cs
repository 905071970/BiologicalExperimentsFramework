using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign_in_UI : BaseUI {

    private InputField username;
    private InputField password;
    private Button sign_in_button;
    private Button sign_up_button;

    private void Awake()
    {
        m_UIType = UIType.normal;

        username = transform.Find("UserName_InputField").GetComponent<InputField>();
        password = transform.Find("Password_InputField").GetComponent<InputField>();
        sign_in_button = transform.Find("Sign_in_Button").GetComponent<Button>();
        sign_up_button = transform.Find("Sign_up_Button").GetComponent<Button>();

        sign_in_button.onClick.AddListener(Sign_in);
        sign_up_button.onClick.AddListener(() =>
        {
            UIManager.GetInstance.ShowUIToScreen("Sign_up_UI");
        });

        Handle.GetInstance.AddAction("sign_in", Sign_in_Callback);
    }


    private void Sign_in()
    {
        if(username.text.Length == 0)
        {
            UIManager.GetInstance.ShowPrompt("用户名不能为空！");
            return;
        }
        if (password.text.Length == 0)
        {
            UIManager.GetInstance.ShowPrompt("密码不能为空！");
            return;
        }

        Request.GetInstance.sign_in(username.text, password.text);
    }


    private void Sign_in_Callback(string res)
    {
        string username = res.Split('#')[1];
        int code = int.Parse(res.Split('#')[2]);

        if(code == 0)
        {
            //登录成功
            PlayerPrefs.SetString("username", username);
            UIManager.GetInstance.ShowPrompt("欢迎" + username + "回来！");
            UIManager.GetInstance.ShowUIToScreen("Main_UI");
        }
        else
        {
            UIManager.GetInstance.ShowPrompt("登录失败!");
        }
    }


    public override void OnEnter()
    {
        base.OnEnter();
        username.text = "";
        password.text = "";
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
