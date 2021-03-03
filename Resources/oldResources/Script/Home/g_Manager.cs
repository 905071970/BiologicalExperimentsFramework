using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g_Manager : MonoBehaviour
{
    private void Awake()
    {
        //界面初始化
        for (int i = 0; i < transform.childCount; i++) { transform.GetChild(i).gameObject.SetActive(false); }
        transform.Find("home").gameObject.SetActive(true);
    }
}
