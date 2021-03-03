using cakeslice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Interaction : MonoBehaviour
{
    /// <summary>
    /// 当选中时需要执行的
    /// </summary>
    private Action action;

    /// <summary>
    /// 是否是现在需要交互的对象
    /// </summary>
    private bool canSelect = false;

    /// <summary>
    /// 器材名字
    /// </summary>
    public string ObjName;

    /// <summary>
    /// 高光
    /// </summary>
    public Outline[] outline;


    private ErrorText errorText;
    private ExNameText exNameText;

    private void Awake()
    {
        SetCanSelect(null);
        SetOutLine(false);

        errorText = GameObject.FindGameObjectWithTag("ErrorText").GetComponent<ErrorText>();
        exNameText = GameObject.FindGameObjectWithTag("ExNameText").GetComponent<ExNameText>();
    }


    /// <summary>
    /// 设置当前是否能被选中，如果能被选中那就需要传入action委托，如果不是当前要选中的那就传入null
    /// </summary>
    /// <param name="action">委托</param>
    public void SetCanSelect(Action action)
    {
        this.action = action;
        canSelect = action != null;
    }

    /// <summary>
    /// 设置是否闪烁
    /// </summary>
    /// <param name="b"></param>
    public void SetOutLine(bool b)
    {
        foreach(var v in outline)
        {
            v.eraseRenderer = !b;
        }
    }

    /// <summary>
    /// 当被选中时
    /// </summary>
    public void OnClick(Vector3 clickPoint)
    {
        if(canSelect == false)
        {
            //选中不是当前需要交互的对象
            //UIManager.GetInstance.ShowError(transform.position);
            errorText.show(clickPoint);
            return;
        }

        if(action != null)
        {
            action();
        }

        SetCanSelect(null);
        SetOutLine(false);
    }


    public void OnRay(Vector3 clickPoint)
    {
        exNameText.show(clickPoint,ObjName);
    }

    
}
