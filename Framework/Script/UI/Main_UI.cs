using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_UI : BaseUI {

    private Button CI_button;//课程介绍
    private Button CC_button;//课程内容
    private Button VR_button;//虚拟仿真
    private Button Back_Button;//注销登录


    private void Awake()
    {
        m_UIType = UIType.normal;

        CI_button = transform.Find("ButtonList/CI_Button").GetComponent<Button>();
        CC_button = transform.Find("ButtonList/CC_Button").GetComponent<Button>();
        Back_Button = transform.Find("Back_Button").GetComponent<Button>();
        VR_button = transform.Find("ButtonList/VR_Button").GetComponent<Button>();


        CI_button.onClick.AddListener(() =>
        {
            Application.ExternalEval("window.open('http://1.15.56.187/Movies/霍乱弧菌介绍.mp4','_blank')");
        });

        CC_button.onClick.AddListener(() =>
        {
            //Application.ExternalEval("window.open('http://1.15.56.187/PDF/霍乱弧菌.pdf','_blank')");
            UIManager.GetInstance.ShowUIToScreen("Course_UI");
        });

        Back_Button.onClick.AddListener(() =>
        {
            UIManager.GetInstance.RemoveTheCurrentFromTheScreenUI();
        });

        VR_button.onClick.AddListener(() =>
        {
            //clsGlobe.prsnextSceneName = "Home";
            //SceneManager.LoadScene("loading");
            UIManager.GetInstance.ShowUIToScreen("HomeUI");
        });

    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
