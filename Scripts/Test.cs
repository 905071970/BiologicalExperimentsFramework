using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private Text text;
    private Coroutine coroutine;

    /// <summary>
    /// 倒计时携程
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator Timeing(int time)
    {
        int curtime = time;
        while(curtime > 0)
        {
            curtime--;
            yield return new WaitForSeconds(1f);//等待1秒后继续循环
            text.text = "倒计时：" + curtime;
        }
    }

    /// <summary>
    /// 开始倒计时
    /// </summary>
    /// <param name="time"></param>
    private void StartTime(int time)
    {
        coroutine = StartCoroutine(Timeing(time));
    }

    /// <summary>
    /// 停止倒计时
    /// </summary>
    private void StopTime()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
