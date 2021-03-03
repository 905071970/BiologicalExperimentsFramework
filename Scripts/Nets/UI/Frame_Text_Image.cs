using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame_Text_Image : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Button button;
    [SerializeField]
    private Image image;

    /// <summary>
    /// 显示提示框
    /// </summary>
    /// <param name="str">文本</param>
    /// <param name="sprite">图片</param>
    /// <param name="action">确定委托</param>
    public void show(string str,Sprite sprite ,Action action)
    {
        //显示提示框
        this.gameObject.SetActive(true);

        CameraRay.GetCameraRay.SetCanRay(false);

        text.text = str;
        image.sprite = sprite;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            //隐藏提示框
            this.gameObject.SetActive(false);
            CameraRay.GetCameraRay.SetCanRay(true);

            if (action != null)
            {
                //执行委托
                action();
            }
        });
    }
}
