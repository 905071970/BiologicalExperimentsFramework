using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clsLoadCommon : MonoBehaviour
{
    #region 变量
    /// <summary>
    /// 单例化脚本
    /// </summary>
    public static clsLoadCommon proCommon;

    [Range(0, 1000)]
    [Header("旋转速度")]
    [SerializeField]
    private float _prfRotationSpeed = 0;

    [Header("控制旋转对象")]
    public bool _prbControlRotation = false;

    [Header("旋转对象")]
    [SerializeField]
    private GameObject _proRotationObj;

    [Header("旋转对象")]
    [SerializeField]
    private GameObject _proRotationContainerObj;

    [Header("加载场景名称")]
    public string prsLoadSceneName;

    [Header("目标场景名称")]
    [SerializeField]
    private string _prsTargetSceneName;
    #endregion

    #region 函数
    private void Awake()
    {
        if (proCommon == null)
        {
            proCommon = this;
        }
        else if (proCommon != this)
        {
            //删除不相关的脚本属性
            DestroyImmediate(this);
        }
    }

    private void FixedUpdate()
    {
        if (_prbControlRotation)
        {
            fnRotationObject();
        }
    }

    /// <summary>
    /// 旋转参数对象
    /// </summary>
    /// <param name="obj">物体对象</param>
    public void fnRotationObject()
    {
        _proRotationObj.transform.Rotate(0, 0, _prfRotationSpeed * Time.deltaTime);
    }

    public void fnLoadScene()
    {
        if (string.IsNullOrEmpty(prsLoadSceneName) || string.IsNullOrEmpty(_prsTargetSceneName))
        {
            return;
        }
        if (_proRotationContainerObj != null)
        {
            _proRotationContainerObj.SetActive(true);
        }

        //保存需要加载的目标场景  
        clsGlobe.prsnextSceneName = _prsTargetSceneName;

        SceneManager.LoadScene(prsLoadSceneName);
    }

    /// <summary>
    /// 退出登录
    /// </summary>
    public void fnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion

}
