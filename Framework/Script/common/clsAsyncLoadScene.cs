using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class clsGlobe
{
    /// <summary>
    /// 下一个场景的名称
    /// </summary>
    public static string prsnextSceneName;
}

public class clsAsyncLoadScene : MonoBehaviour
{
    #region 变量
    [Header("加载数字显示")]
    public Text prtLoadText;

    [Range(0, 1)]
    [SerializeField]
    [Header("加载速度控制")]
    private float _prfLoadingSpeed = 1;

    /// <summary>
    /// 异步操作
    /// </summary>
    private AsyncOperation proOperation;

    /// <summary>
    /// 进度值
    /// </summary>
    private float _prfProgressValue;

    /// <summary>
    /// 加载值
    /// </summary>
    private float _prfLoadValue;

    #endregion

    #region 函数

    private void Awake()
    {
        //print("dfjhdsjfk");
        //Resources.FindObjectsOfTypeAll<Object>()过滤掉预设体的方法
        Object[] _obj = Resources.FindObjectsOfTypeAll<Object>();
        for (int i = 0; i < _obj.Length; i++)
        {
            _obj[i] = null;//解除资源的引用
        }

        //卸载没有被引用的资源
        Resources.UnloadUnusedAssets();

        //立刻进行垃圾回收
        GC.Collect();
        //挂起当前线程，直到处理终结器队列的线程清空该队列为止
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    // Use this for initialization
    void Start()
    {
        _prfLoadValue = 0.0f;
        prtLoadText.text = "0%";
        //当加载场景的名称存在时
        print(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "loading")
        {
            //启动协程
            StartCoroutine(fnAsyncLoad());
        }
    }

    /// <summary>
    /// 同过协程进行异步加载
    /// </summary>
    /// <returns></returns>
    IEnumerator fnAsyncLoad()
    {
        proOperation = SceneManager.LoadSceneAsync(clsGlobe.prsnextSceneName);
        //阻止当加载完成自动切换  
        proOperation.allowSceneActivation = false;
        yield return proOperation;
    }

    private void FixedUpdate()
    {
        //_prfProgressValue = proOperation.progress;
        Debug.Log(proOperation);
        if (proOperation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9  
            _prfProgressValue = 1.0f;
        }

        if (_prfProgressValue != _prfLoadValue)
        {
            //插值运算
            _prfLoadValue = Mathf.Lerp(_prfLoadValue, _prfProgressValue, Time.deltaTime * _prfLoadingSpeed);
            if (Mathf.Abs(_prfLoadValue - _prfProgressValue) < 0.01f)
            {
                _prfLoadValue = _prfProgressValue;
            }
        }
        prtLoadText.text = ((int)(_prfLoadValue * 100)).ToString() + "%";
        //Debug.Log(((int)(_prfLoadValue * 100)).ToString());
        if ((int)(_prfLoadValue * 100) == 100)
        {
            prtLoadText.text = "100" + "%";
            //允许异步加载完毕后自动切换场景  
            proOperation.allowSceneActivation = true;
        }
    }

    private void OnDestroy()
    {
        proOperation = null;
    }
    #endregion


}
