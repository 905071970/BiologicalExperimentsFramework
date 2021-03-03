using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_project : MonoBehaviour
{
    private void OnEnable()
    {
        //界面初始化
        for (int i = 0; i < transform.childCount; i++) { transform.GetChild(i).gameObject.SetActive(true); }
    }

    /// <summary>
    /// 打开知识要点界面
    /// </summary>
    /// <param name="MainPoints"></param>
    public void OpenMainPoints(GameObject MainPoints)
    {
        //激活知识要点界面
        MainPoints.SetActive(true);

        //关闭当前界面
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 打开器材界面
    /// </summary>
    public void OpenEquipment(GameObject Equipment)
    {
        //激活器材界面    
        Equipment.SetActive(true);
        //关闭当前界面     
        gameObject.SetActive(false);
    }
}
