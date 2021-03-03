using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 文本提示框
/// </summary>
public class Frame_Text : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Button button;

    /// <summary>
    /// 显示提示框
    /// </summary>
    /// <param name="str">文本</param>
    /// <param name="action">确定委托</param>
    public void show(string str,Action action)
    {
        //显示提示框
        this.gameObject.SetActive(true);

        CameraRay.GetCameraRay.SetCanRay(false);

        text.text = str;
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


        ///实时更改UI大小
        float height = HandleSelfFittingAlongAxis(1, text.GetComponent<RectTransform>(), text.GetComponent<ContentSizeFitter>());
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(800, height+200);
        text.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(text.GetComponent<RectTransform>().anchoredPosition.x,50);
    }



    private static float HandleSelfFittingAlongAxis(int axis, RectTransform rect, ContentSizeFitter contentSizeFitter)
    {
        UnityEngine.UI.ContentSizeFitter.FitMode fitting = (axis == 0 ? contentSizeFitter.horizontalFit : contentSizeFitter.verticalFit);
        if (fitting == UnityEngine.UI.ContentSizeFitter.FitMode.MinSize)
        {
            return LayoutUtility.GetMinSize(rect, axis);
        }
        else
        {
            return LayoutUtility.GetPreferredSize(rect, axis);
        }
    }
}
