using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign_up_UI : BaseUI {

    private InputField username;
    private InputField password;
    private InputField againpassword;
    private Button sign_in_button;
    private Button sign_up_button;


    private void Awake()
    {
        m_UIType = UIType.normal;

        username = transform.Find("UserName_InputField").GetComponent<InputField>();
        password = transform.Find("Password_InputField").GetComponent<InputField>();
        againpassword = transform.Find("AgainPassword_InputField").GetComponent<InputField>();
        sign_in_button = transform.Find("Sign_in_Button").GetComponent<Button>();
        sign_up_button = transform.Find("Sign_up_Button").GetComponent<Button>();


        sign_up_button.onClick.AddListener(Sign_up);
        sign_in_button.onClick.AddListener(() =>
        {
            UIManager.GetInstance.ShowUIToScreen("Sign_in_UI");
        });

        Handle.GetInstance.AddAction("sign_up", Sign_up_Callback);

    }

    private void Sign_up()
    {
        if (username.text.Length == 0)
        {
            UIManager.GetInstance.ShowPrompt("用户名不能为空！");
            return;
        }
        if (password.text.Length == 0)
        {
            UIManager.GetInstance.ShowPrompt("密码不能为空！");
            return;
        }
        if (againpassword.text.Length == 0)
        {
            UIManager.GetInstance.ShowPrompt("二次密码不能为空！");
            return;
        }

        if (password.text.Equals(againpassword.text)==false)
        {
            UIManager.GetInstance.ShowPrompt("两次密码不一致！");
            return;
        }

        Request.GetInstance.sign_up(username.text, password.text);
    }


    private void Sign_up_Callback(string res)
    {
        string username = res.Split('#')[1];
        int code = int.Parse(res.Split('#')[2]);

        if(code == 0)
        {
            //注册成功
            UIManager.GetInstance.ShowPrompt(username+"注册成功！");
            UIManager.GetInstance.ShowUIToScreen("Sign_in_UI");
        }else if(code == -1)
        {
            UIManager.GetInstance.ShowPrompt(username + "已被注册！");
        }
        else
        {
            UIManager.GetInstance.ShowPrompt("注册出错！");
        }
    }


    public override void OnEnter()
    {
        base.OnEnter();
        username.text = "";
        password.text = "";
        againpassword.text = "";
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
