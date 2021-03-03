using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Course_UI : BaseUI
{
    private Button Lecture_Button;
    private Button PPT_Button;
    private Button ST_Button;
    private Button Back_Button;


    private void Awake()
    {
        Lecture_Button = transform.Find("ButtonList/Lecture_Button").GetComponent<Button>();
        PPT_Button = transform.Find("ButtonList/PPT_Button").GetComponent<Button>();
        Back_Button = transform.Find("Back_Button").GetComponent<Button>();
        ST_Button = transform.Find("ButtonList/ST_Button").GetComponent<Button>();


        Lecture_Button.onClick.AddListener(() =>
        {
            //讲课视频
            Application.ExternalEval("window.open('http://1.15.56.187/Movies/霍乱弧菌讲课.mp4','_blank')");
        });

        PPT_Button.onClick.AddListener(() =>
        {
            //PPT
            Application.ExternalEval("window.open('http://1.15.56.187/PDF/霍乱弧菌.pdf','_blank')");
        });

        Back_Button.onClick.AddListener(() =>
        {
            //返回
            UIManager.GetInstance.RemoveTheCurrentFromTheScreenUI();
        });

        ST_Button.onClick.AddListener(() =>
        {
            //小测
            if (PlayerPrefs.HasKey("username"))
            {
                Application.ExternalEval("window.open('http://1.15.56.187/ex/index.html?name="+
                    PlayerPrefs.GetString("username")+"','_blank')");
            }
            else
            {
                Debug.LogError("获取用户名失败");
            }
            
        });
    }
}
