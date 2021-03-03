using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBaseUI : MonoBehaviour
{
    /// <summary>
    /// 完成委托
    /// </summary>
    private Action action;

    /// <summary>
    /// 显示UI
    /// </summary>
    public virtual void show(Action action)
    {
        this.gameObject.SetActive(true);
        this.action = action;
    }
    /// <summary>
    /// 结束UI
    /// </summary>
    public void end()
    {
        if(action != null)
        {
            action();
        }
        this.gameObject.SetActive(false);
    }
}
