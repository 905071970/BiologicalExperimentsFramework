using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class g_home : MonoBehaviour
{

    private void OnEnable()
    {
        //界面初始化
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 打开ppt界面
    /// </summary>
    public void OpenPPT(GameObject ppt)
    {
        //激活ppt界面
        ppt.SetActive(true);

        //关闭主页
        gameObject.SetActive(false);

    }

    /// <summary>
    /// 打开项目界面
    /// </summary>
    public void OpenProject(GameObject intoProject)
    {
        //激活Project界面
        intoProject.SetActive(true);
        //关闭主页       
        gameObject.SetActive(false);
    }
}
