using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class g_BGUIManager : MonoBehaviour
{
    [Header("流程图片")]
    public Image img;
    public static Action imgAction;//图片激活委托

    [Header("视频控件")]
    public RectTransform videoUI;

    private Vector3 videoUIV3;
    private void Awake()
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        imgAction += ImageAction;
        img.gameObject.SetActive(false);

        #region 初始化视频控件
        //保存在屏幕中的坐标
        videoUIV3 = videoUI.position;   
        //移动到屏幕外下方
        videoUI.localPosition = new Vector3(videoUI.localPosition.z, -Screen.height / 2 - videoUI.rect.yMax, videoUI.localPosition.z);
        #endregion
    }

    public void ImageAction()
    {
        img.gameObject.SetActive(true);

        StartCoroutine("ImageAlpha");
    }

    public IEnumerator ImageAlpha()     //图片渐变
    {

        float a = 1f, aa = Time.deltaTime,r= img.color.r,g= img.color.g,b= img.color.b;
        if(img.color.a>0.5f)
        {
            a = 0;
            aa = -Time.deltaTime;
        }
        while(Math.Abs(img.color.a-a)>=0.02f)
        {
            img.color = new Color(r, g, b, img.color.a + aa);

            yield return new WaitForSeconds(Time.deltaTime);
        }
        img.color = new Color(r, g, b,a);

        yield return null;
    }
    float f = 0;
    private void Update()
    {
        //鼠标放到视频控件的区域时，控件从下往上弹出来
        if (Input.mousePosition.y < videoUI.rect.yMax * 2)
        {
            f += 0.02f;
            videoUI.position = Vector3.Lerp(videoUI.position, videoUIV3, f);
            if (f > 1)
                f = 1;
        }
        //鼠标不在视频控件的区域时，控件从上往下弹出屏幕
        else
        {
            f -= 0.02f;
            videoUI.localPosition = Vector3.Lerp(videoUI.localPosition, new Vector3(videoUI.localPosition.z, -Screen.height / 2 - videoUI.rect.yMax, videoUI.localPosition.z), f);
            if (f < 0)
                f = 0;
        }

    }
}
