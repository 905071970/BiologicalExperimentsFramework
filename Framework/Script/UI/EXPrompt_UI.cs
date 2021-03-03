using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPrompt_UI : BaseUI
{
    private Text ErrorText;

    private GameObject TiShi;
    private GameObject image;

    private GameObject StartEX;

    private GameObject JieShao;


    private Coroutine coroutine;

    private Coroutine jieshao_coroutine;


    private void Awake()
    {
        m_UIType = UIType.top;

        ErrorText = transform.Find("ErrorText").GetComponent<Text>();
        TiShi = transform.Find("TiShi").gameObject;
        image = TiShi.transform.Find("Image").gameObject;
        StartEX = transform.Find("StartEX").gameObject;
        JieShao = transform.Find("JieShao").gameObject;

        TiShi.SetActive(false);
        ErrorText.gameObject.SetActive(false);
        StartEX.SetActive(false);
        JieShao.SetActive(false);
    }

    public void showError(Vector3 pos)
    {
        ErrorText.gameObject.SetActive(true);
        ErrorText.transform.position = Camera.main.WorldToScreenPoint(pos);
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(hideError());
    }

    private IEnumerator hideError()
    {
        yield return new WaitForSeconds(2f);
        ErrorText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示提示框,如果不要图片imagePath就传null
    /// </summary>
    /// <param name="str">文本</param>
    /// <param name="imagePath">图片路径</param>
    /// <param name="action">点击继续按钮后的委托</param>
    public void showTiShi(string str,string imagePath,Action action)
    {
        CameraRay.GetCameraRay.SetCanRay(false);
        TiShi.SetActive(true);

        TiShi.transform.Find("Text").GetComponent<Text>().text = str;
        float height = HandleSelfFittingAlongAxis(1,
            TiShi.transform.Find("Text").GetComponent<RectTransform>(),
            TiShi.transform.Find("Text").GetComponent<ContentSizeFitter>());
        float posY = -(height / 2 + 25);

        Vector2 pos = TiShi.transform.Find("Text").GetComponent<RectTransform>().anchoredPosition;
        TiShi.transform.Find("Text").GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, posY);

        float allHeight = height + 30 + 70;

        if (imagePath == null)
        {
            //不需要图片
            image.SetActive(false);
        }
        else
        {
            //需要图片
            image.SetActive(true);
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>(imagePath);
            allHeight = 400;
        }

        

        TiShi.GetComponent<RectTransform>().sizeDelta = new Vector2(550, allHeight);

        TiShi.transform.Find("Button").GetComponent<Button>().onClick.RemoveAllListeners();

        TiShi.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            TiShi.SetActive(false);
            CameraRay.GetCameraRay.SetCanRay(true);

            if (action != null)
            {
                action();
                Debug.Log("执行action");
            }
            
        });
    }

    /// <summary>
    /// 显示开始实验弹窗
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="mudi">目的</param>
    /// <param name="yuanli">原理</param>
    /// <param name="action">开始实验委托</param>
    public void showStartEX(string title, string mudi, string yuanli,Action action)
    {
        StartEX.SetActive(true);
        StartEX.transform.Find("Title").GetComponent<Text>().text = title;
        StartEX.transform.Find("Mudi_Panel").GetComponentInChildren<Text>().text = mudi;
        StartEX.transform.Find("Yuanli_Panel").GetComponentInChildren<Text>().text = yuanli;
        StartEX.transform.Find("Button").GetComponent<Button>().onClick.RemoveAllListeners();
        StartEX.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            StartEX.SetActive(false);

            if (action != null)
            {
                action();
            }
            
        });
    }


    public void showJieShao(string str)
    {
        JieShao.SetActive(true);
        JieShao.GetComponentInChildren<Text>().text = str;
        if(jieshao_coroutine != null)
        {
            StopCoroutine(jieshao_coroutine);
        }
        jieshao_coroutine = StartCoroutine(hideJieShao());
    }


    private IEnumerator hideJieShao()
    {
        yield return new WaitForSeconds(2f);
        JieShao.SetActive(false);
    }



    private static float HandleSelfFittingAlongAxis(int axis, RectTransform rect, ContentSizeFitter contentSizeFitter)
    {
        ContentSizeFitter.FitMode fitting = (axis == 0 ? contentSizeFitter.horizontalFit : contentSizeFitter.verticalFit);
        if (fitting == ContentSizeFitter.FitMode.MinSize)
        {
            return LayoutUtility.GetMinSize(rect, axis);
        }
        else
        {
            return LayoutUtility.GetPreferredSize(rect, axis);
        }
    }

}
