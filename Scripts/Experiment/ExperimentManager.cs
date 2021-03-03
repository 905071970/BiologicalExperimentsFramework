using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ExperimentManager : MonoBehaviour
{
    #region 通用成员
    /// <summary>
    /// 动画状态机
    /// </summary>
    private Animator animator;
    /// <summary>
    /// 文本提示框
    /// </summary>
    private Frame_Text m_Frame_Text;
    /// <summary>
    /// 文本和图片提示框
    /// </summary>
    private Frame_Text_Image m_Frame_Text_Image;
    /// <summary>
    /// 介绍弹窗
    /// </summary>
    private Introduced m_Introduced;
    /// <summary>
    /// 开始实验弹窗
    /// </summary>
    private StartPanel m_startPanel;
    /// <summary>
    /// 选择题
    /// </summary>
    private ChoicePanel m_ChoicePanel;
    /// <summary>
    /// 当前动画段数
    /// </summary>
    private int curAnim = 1;

    #endregion

    #region 通用方法

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animName">动画名字</param>
    /// <param name="action">播放完成回调</param>
    private void PlayAnim(string animName, Action action)
    {
        animator.Play(animName);
        StartCoroutine(AnimIsComplete(animName, action));
    }

    /// <summary>
    /// 播放下一段动画
    /// </summary>
    /// <param name="action"></param>
    private void NextAnim(Action action)
    {
        PlayAnim(curAnim.ToString(), action);
        curAnim++;
    }

    /// <summary>
    /// 动画是否播放完成
    /// </summary>
    /// <returns></returns>
    private IEnumerator AnimIsComplete(string animName, Action action)
    {
        while (true)
        {
            var animInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.IsName(animName) && animInfo.normalizedTime >= 1.0f)
            {
                break;
            }

            yield return null;
        }

        //播放完成
        if (action != null)
        {
            action();
        }
    }

    /// <summary>
    /// 设置点击就交互
    /// </summary>
    /// <param name="objectPath">要交互的物品路径</param>
    /// <param name="action">交互委托</param>
    private void setClick(string objectPath, Action action)
    {
        //animator.transform.Find(objectPath).GetComponent<Interaction>().SetCanSelect(action);
        getTarget<Interaction>(objectPath).SetCanSelect(action);
        setOutLine(objectPath, true);
    }

    /// <summary>
    /// 设置物体闪烁
    /// </summary>
    /// <param name="path"></param>
    /// <param name="b"></param>
    private void setOutLine(string path,bool b)
    {
        getTarget<Interaction>(path).SetOutLine(b);
    }

    /// <summary>
    /// 延时调用
    /// </summary>
    /// <param name="time"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    private void delayCall(float time, Action action)
    {
        StartCoroutine(IE_delayCall(time, action));
    }

    private IEnumerator IE_delayCall(float time,Action action)
    {
        yield return new WaitForSeconds(time);
        if(action != null)
        {
            action();
        }
    }

    /// <summary>
    /// 摄像机移动
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="time"></param>
    /// <param name="action"></param>
    private void cameraMoveTo(Vector3 pos, float time, Action action)
    {
        StartCoroutine(IE_cameraMoveTo(pos, time, action));
    }

    private IEnumerator IE_cameraMoveTo(Vector3 pos, float time, Action action)
    {
        Vector3 orgin = Camera.main.transform.position;
        float cur = 0;
        while (cur < time)
        {
            cur += Time.deltaTime;
            float a = cur / time;
            Camera.main.transform.position = Vector3.Lerp(orgin, pos, a);
            yield return null;
        }

        Camera.main.transform.position = pos;

        if (action != null)
        {
            action();
        }
    }

    /// <summary>
    /// 摄像机旋转
    /// </summary>
    /// <param name="rot"></param>
    /// <param name="time"></param>
    /// <param name="action"></param>
    private void cameraRotateTo(Vector3 rot,float time,Action action)
    {
        StartCoroutine(IE_cameraRotateTo(rot, time, action));
    }

    private IEnumerator IE_cameraRotateTo(Vector3 rot, float time, Action action)
    {
        Quaternion orgin = Camera.main.transform.rotation;
        Quaternion target = Quaternion.Euler(rot);
        float curtime = 0;
        while(curtime < time)
        {
            curtime += Time.deltaTime;
            float a = curtime / time;
            Camera.main.transform.rotation = Quaternion.Lerp(orgin, target, a);
            yield return null;
        }
        Camera.main.transform.rotation = target;
        if(action != null)
        {
            action();
        }
    }

    /// <summary>
    /// 获取自身下面的东西
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    private T getTarget<T>(string path)
    {
        Debug.Log("find：" + path);
        return transform.Find(path).GetComponent<T>();
    }

    /// <summary>
    /// 切换游戏物体的Active状态
    /// </summary>
    private void switchGameObjectActive(string path,bool b)
    {
        getTarget<Transform>(path).gameObject.SetActive(b);
    }

    /// <summary>
    /// 显示提示框
    /// </summary>
    /// <param name="content">文本</param>
    /// <param name="imagePath">图片路径</param>
    /// <param name="action">点击继续按钮后执行的操作</param>
    private void showPromptBox(string content,string imagePath,Action action)
    {
        if(imagePath == null)
        {
            //文本提示框
            m_Frame_Text.show(content, action);
        }
        else
        {
            //文本和图片提示框
            Sprite sprite = Resources.Load<Sprite>(imagePath);
            m_Frame_Text_Image.show(content, sprite, action);
        }
    }

    /// <summary>
    /// 显示实验开始提示框
    /// </summary>
    /// <param name="title">主题</param>
    /// <param name="objective">目的</param>
    /// <param name="principle">原理</param>
    /// <param name="action">点击开始实验后执行的操作</param>
    private void showStartBox(string title,string objective,string principle,Action action)
    {
        //UIManager.GetInstance.showStartEX(title, objective, principle, action);
        m_startPanel.show(title, objective, principle, action);
    }

    /// <summary>
    /// 加载Resources文件夹资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    private T loadResource<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    /// <summary>
    /// 显示或隐藏介绍弹窗
    /// </summary>
    /// <param name="str"></param>
    private void showIntroduced(string str,Action action)
    {
        m_Introduced.show(str,action);
    }

    /// <summary>
    /// 跳转下一个场景
    /// </summary>
    /// <param name="sceneName"></param>
    private void LoadScene()
    {
        var sceneName = SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex + 1).name;
        clsGlobe.prsnextSceneName = sceneName;
        SceneManager.LoadScene("loading");
    }

    /// <summary>
    /// 显示选择题
    /// </summary>
    /// <param name="path"></param>
    /// <param name="action"></param>
    private void showChoice(ChoiceObject choiceObject,Action action)
    {
        m_ChoicePanel.show(choiceObject, action);
    }

    #endregion


    //#region 实验器材路径常量
    ///// <summary>
    ///// 试管
    ///// </summary>
    //private const string CON_Tube = "1235";
    ///// <summary>
    ///// 灭菌器
    ///// </summary>
    //private const string CON_Sterilizer = "001";
    ///// <summary>
    ///// 灭菌器开关
    ///// </summary>
    //private const string CON_Sterilizer_Switch = "146";
    ///// <summary>
    ///// 载玻片盒
    ///// </summary>
    //private const string CON_Slide_Box = "hx_fyl_jxdy001";
    ///// <summary>
    ///// 接种环
    ///// </summary>
    //private const string CON_Inoculation_Loops = "jiezhonghuan";
    ///// <summary>
    ///// 生理盐水
    ///// </summary>
    //private const string CON_Water = "hx_fyl_tmdp_p";
    ///// <summary>
    ///// 玻璃片
    ///// </summary>
    //private const string CON_Glass = "hx_jcyql_ldj";
    ///// <summary>
    ///// 镊子
    ///// </summary>
    //private const string CON_Nie = "niezi";
    ///// <summary>
    ///// 盖玻片盒
    ///// </summary>
    //private const string CON_hezi = "hezi02";
    //#endregion

    #region 实验器材动画名字常量
    /// <summary>
    /// 试管移动到面前
    /// </summary>
    ///private const string TUBE_MOVE_TO_FRONT = "1";
    #endregion

    #region 路径
    private  string TiShiPath = "ScriptObject/TiShi/";
    private  string CameraPosRotPath = "ScriptObject/CameraPosRot/";
    private  string StartPath = "ScriptObject/Start/";
    private string ChoicePath = "ScriptObject/Choice/";
    #endregion



    /// <summary>
    /// 通用UI面板
    /// </summary>
    [SerializeField]
    private GameObject GeneralCanvas;

    /// <summary>
    /// 用户自定义UI画布
    /// </summary>
    [SerializeField]
    private GameObject userCanvas;

    [SerializeField]
    private GameObject userObjects;

    /// <summary>
    /// 表格名字
    /// </summary>
    [SerializeField]
    private string TableName;

    /// <summary>
    /// 实验名字
    /// </summary>
    [SerializeField]
    private string ExName;


    private Dictionary<string, GameObject> userObjectDict = new Dictionary<string, GameObject>();


    /// <summary>
    /// 用户自定义UI缓存
    /// </summary>
    private Dictionary<string, MyBaseUI> userUIDict = new Dictionary<string, MyBaseUI>();

    /// <summary>
    /// 表格数据缓存
    /// </summary>
    private List<string> lines = new List<string>();

    /// <summary>
    /// 当前读到第几行数据
    /// </summary>
    private int curOp = 0;

    /// <summary>
    /// 自定义指令集合
    /// </summary>
    private Dictionary<string, Action<string>> actions = new Dictionary<string, Action<string>>();

    /// <summary>
    /// 表格数据
    /// </summary>
    private string TableData = null;


    /// <summary>
    /// 开始
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();

        TiShiPath += ExName + "/";
        CameraPosRotPath += ExName + "/";
        ChoicePath += ExName + "/";

        if(userObjects != null)
        {
            for (int i = 0; i < userObjects.transform.childCount; i++)
            {
                userObjectDict.Add(userObjects.transform.GetChild(i).name, userObjects.transform.GetChild(i).gameObject);
            }
        }
        

        ///加载表格数据或请求表格数据
        StartCoroutine(GetData());

        #region 加载自定义指令
        ///播放动画
        actions.Add("PlayAnim", (line) =>
        {
            Debug.Log("playanim:" + line);
            PlayAnim(line.Split(',')[1],NextOp);
        });
        ///文本提示框
        actions.Add("PromptText", (line) =>
         {
             TiShiObject tishi = loadResource<TiShiObject>(TiShiPath+line.Split(',')[1]);
             showPromptBox(tishi.GetText, null, NextOp);
         });
        ///文本和图片提示框
        actions.Add("PromptTextImage", (line) =>
        {
            TiShiObject tishi = loadResource<TiShiObject>(TiShiPath+line.Split(',')[1]);
            showPromptBox(tishi.GetText, tishi.imagePath, NextOp);
        });
        ///设置交互
        actions.Add("SetClick", (line) =>
         {
             line = line.Split(',')[1];
             if (line.IndexOf(' ') == line.Length - 1)
             {
                 line = line.Substring(0, line.Length - 1);
             }
             setClick(line, NextOp);
         });
        ///设置高亮
        actions.Add("SetOutLine", (line) =>
        {
            setOutLine(line.Split(',')[1], bool.Parse(line.Split(',')[2]));
            NextOp();
        });
        ///延时
        actions.Add("DelayCall", (line) =>
        {
            delayCall(float.Parse(line.Split(',')[1]), NextOp);
        });
        ///摄像机移动
        actions.Add("CameraMove", (line) =>
        {
            CameraPosRotObject obj = loadResource<CameraPosRotObject>(CameraPosRotPath+line.Split(',')[1]);
            cameraMoveTo(obj.pos, float.Parse(line.Split(',')[2]), NextOp);
        });
        ///摄像机旋转
        actions.Add("CameraRotate", (line) =>
        {
            CameraPosRotObject obj = loadResource<CameraPosRotObject>(CameraPosRotPath+line.Split(',')[1]);
            cameraRotateTo(obj.rot.eulerAngles, float.Parse(line.Split(',')[2]), NextOp);
        });
        ///开始实验弹窗
        actions.Add("PromptStart", (line) =>
         {
             StartExObject obj = loadResource<StartExObject>(StartPath+line.Split(',')[1]);
             showStartBox(obj.Title, obj.Mudi, obj.Yuanli, NextOp);
         });
        ///切换灯的状态
        actions.Add("SwitchLight", (line) =>
        {
            getTarget<Light>(line.Split(',')[1]).intensity = bool.Parse(line.Split(',')[2]) ? 1 : 0;
            NextOp();
        });
        ///显示用户自定义UI
        actions.Add("ShowCustomUI", (line) =>
         {
             if(userUIDict.TryGetValue(line.Split(',')[1], out var myBaseUI))
             {
                 myBaseUI.show(NextOp);
             }
             else
             {
                 NextOp();
             }
         });
        ///打开指定场景
        actions.Add("LoadScene", (line) =>
        {
            LoadScene();
        });
        ///上传场景数，标记当前场景已完成,结束
        actions.Add("End", (line) =>
        {
            if (PlayerPrefs.HasKey("username"))
            {
                Request.GetInstance.updateLevelCount(PlayerPrefs.GetString("username"),
                    int.Parse(line.Split(',')[1]));
            }
        });
        ///设置物品active
        actions.Add("SetActive", (line) =>
        {
            if(userObjectDict.TryGetValue(line.Split(',')[1],out var obj))
            {
                obj.SetActive(bool.Parse(line.Split(',')[2]));
            }
            NextOp();
        });
        ///显示介绍
        actions.Add("Introduced", (line) =>
        {
            TiShiObject tishi = loadResource<TiShiObject>(TiShiPath+ line.Split(',')[1]);
            showIntroduced(tishi.GetText, NextOp);
        });
        ///选择题
        actions.Add("ChoicePanel", (line) =>
        {
            ChoiceObject choiceObject = loadResource<ChoiceObject>(ChoicePath + line.Split(',')[1]);
            showChoice(choiceObject, NextOp);
        });

        Debug.Log("加载自定义指令完成");
        #endregion

        #region 加载用户自定义UI
        for(int i = 0; i < userCanvas.transform.childCount; i++)
        {
            var child = userCanvas.transform.GetChild(i);
            userUIDict.Add(child.name, child.GetComponent<MyBaseUI>());
            child.gameObject.SetActive(false);
        }

        Debug.Log("加载用户自定义UI完成");
        #endregion

        #region 加载通用UI脚本
        m_startPanel = GeneralCanvas.transform.GetComponentInChildren<StartPanel>();
        m_Frame_Text = GeneralCanvas.transform.GetComponentInChildren<Frame_Text>();
        m_Frame_Text_Image = GeneralCanvas.transform.GetComponentInChildren<Frame_Text_Image>();
        m_Introduced = GeneralCanvas.transform.GetComponentInChildren<Introduced>();
        m_ChoicePanel = GeneralCanvas.transform.GetComponentInChildren<ChoicePanel>();
        m_startPanel.gameObject.SetActive(false);
        m_Frame_Text.gameObject.SetActive(false);
        m_Frame_Text_Image.gameObject.SetActive(false);
        m_Introduced.gameObject.SetActive(false);
        m_ChoicePanel.gameObject.SetActive(false);
        Debug.Log("加载通用UI完成");
        #endregion

        Debug.Log("初始化完成");

        //NextOp();
        Handle.GetInstance.AddAction("updateLevelCount", (str) =>
        {
            Debug.Log("handle："+str);
            if(str.IndexOf("0") > 0)
            {
                Debug.Log("更新成功");
            }
            else
            {
                Debug.Log("更新失败");
            }
        });
    }


    /// <summary>
    /// 初始化表格数据
    /// </summary>
    private void InitTable()
    {
        if(TableData != null)
        {
            string[] line = TableData.Split('\n');
            for(int i = 0; i < line.Length; i++)
            {
                if(line[i].Length == 0)
                {
                    break;
                }
                lines.Add(line[i]);
            }

            Debug.Log("初始化表格数据完成");

            NextOp();
        }
    }


    /// <summary>
    /// 进行操作
    /// </summary>
    private void NextOp()
    {
        if(curOp >= lines.Count)
        {
            Debug.Log("已经到最后一步");
            return;
        }
        string line = lines[curOp++];

        string op = line.Split(',')[0];
        
        if(actions.TryGetValue(op,out var action))
        {
            if(line.IndexOf('$') >=0)
            {
                ///$代表0，因为csv中不能输入类似001
                line = line.Replace('$', '0');
            }
            
            action(line);
        }
        else
        {
            Debug.LogError("没有找到对应的处理指令:"+line);
        }
    }


    /// <summary>
    /// 请求表格数据
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetData()
    {
        var uri = new System.Uri(Path.Combine(Application.streamingAssetsPath,"Tables", TableName + ".csv"));
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            ///加载失败
            Debug.Log("加载表格失败");
        }
        else
        {
            TableData = www.downloadHandler.text;
            Debug.Log("加载表格成功");
            InitTable();
        }
    }



    #region 注释代码
    ///// <summary>
    ///// 试管步骤
    ///// </summary>
    //private void s1()
    //{
    //    TiShiObject tiShi = loadResource<TiShiObject>(TiShiPath + "生物安全柜的准备");
    //    ///“生物安全柜的准备”弹窗
    //    showPromptBox(tiShi.GetText, null,()=>
    //    {
    //        ///设置试管高亮
    //        setOutLine(CON_Tube, true);
    //        setClick(CON_Tube, ()=>
    //        {
    //            ///播放试管到面前动画
    //            PlayAnim("1", () =>
    //            {
    //                ///显示介绍试管
    //                switchIntroduced("“米泔水”样便\n\n提供待检菌种");
    //                ///等待3秒
    //                delayCall(3f, () =>
    //                {
    //                    ///隐藏介绍弹窗
    //                    switchIntroduced(null);
    //                    ///播放试管归位动画
    //                    ///跳转到s2
    //                    PlayAnim("2", s2);

    //                });

    //            });

    //        });

    //    });

    //}

    ///// <summary>
    ///// 灭菌器步骤
    ///// </summary>
    //private void s2()
    //{
    //    TiShiObject tiShi = loadResource<TiShiObject>(TiShiPath + "红外线灭菌器的准备");
    //    ///显示“红外线灭菌器的准备”弹窗
    //    showPromptBox(tiShi.GetText, null, () =>
    //    {
    //        ///高亮灭菌器
    //        setOutLine(CON_Sterilizer, true);
    //        ///延时3秒
    //        delayCall(3f, () =>
    //        {
    //            ///取消灭菌器高亮
    //            setOutLine(CON_Sterilizer, false);

    //            CameraPosRotObject cpr_sterilizer = loadResource<CameraPosRotObject>(CameraPosRotPath + "灭菌器");
    //            ///摄像机移动到灭菌器
    //            cameraMoveTo(cpr_sterilizer.pos, 1f, () =>
    //            {
    //                ///设置灭菌器开关高亮
    //                setOutLine(CON_Sterilizer_Switch, true);
    //                ///设置灭菌器开关交互
    //                setClick(CON_Sterilizer_Switch, () =>
    //                {
    //                    ///播放灭菌器开关动画
    //                    PlayAnim("3", () =>
    //                    {
    //                        getTarget<Light>(CON_Sterilizer + "/PointLight").intensity = 1;
    //                        ///显示灭菌器UI
    //                        m_sterilizerPanel.show(() =>
    //                        {
    //                            CameraPosRotObject cpr_start = loadResource<CameraPosRotObject>(CameraPosRotPath + "起始点");
    //                            ///摄像机移动到起始点
    //                            ///跳转到s3
    //                            cameraMoveTo(cpr_start.pos,1f,s3);
    //                        });

    //                    });

    //                });

    //            });

    //        });

    //    });
    //}


    ///// <summary>
    ///// 载玻片盒
    ///// </summary>
    //private void s3()
    //{
    //    setOutLine(CON_Slide_Box, true);
    //    ///设置载玻片盒交互
    //    setClick(CON_Slide_Box, () =>
    //    {
    //        ///播放载玻片盒动画
    //        PlayAnim("4", () =>
    //        {
    //            var tishi = loadResource<TiShiObject>(TiShiPath+"载玻片盒");
    //            ///显示载玻片盒弹窗
    //            ///跳转到s4
    //            showPromptBox(tishi.GetText, tishi.imagePath, s4);

    //        });

    //    });
    //}


    ///// <summary>
    ///// 生理盐水
    ///// </summary>
    //private void s4()
    //{
    //    ///设置生理盐水高亮
    //    setOutLine(CON_Water, true);
    //    ///设置生理盐水交互
    //    setClick(CON_Water, () =>
    //    {
    //        ///播放滴生理盐水动画
    //        PlayAnim("5", s5);
    //    });
    //}


    ///// <summary>
    ///// 接种环
    ///// </summary>
    //private void s5()
    //{
    //    ///接种环高亮
    //    setOutLine(CON_Inoculation_Loops, true);
    //    ///设置接种环交互
    //    setClick(CON_Inoculation_Loops, () =>
    //    {
    //        ///播放接种环动画
    //        PlayAnim("6", () =>
    //        {
    //            var tishi_jiezhonghuan = loadResource<TiShiObject>(TiShiPath + "接种环");
    //            ///显示接种环弹窗
    //            showPromptBox(tishi_jiezhonghuan.GetText, null,s6);

    //        });

    //    });
    //}


    ///// <summary>
    ///// 灭菌器
    ///// </summary>
    //private void s6()
    //{
    //    ///设置灭菌器高亮
    //    setOutLine(CON_Sterilizer, true);
    //    ///设置灭菌器交互
    //    setClick(CON_Sterilizer, () =>
    //    {
    //        ///播放接种环灭菌器动画
    //        PlayAnim("7", s7);

    //    });
    //}


    ///// <summary>
    ///// 玻璃片
    ///// </summary>
    //private void s7()
    //{
    //    setOutLine(CON_Glass, true);
    //    setClick(CON_Glass, () =>
    //    {
    //        var tishi_na = loadResource<TiShiObject>(TiShiPath + "拿去米泔水");
    //        showPromptBox(tishi_na.GetText, tishi_na.imagePath, s8);

    //    });
    //}


    ///// <summary>
    ///// 试管
    ///// </summary>
    //private void s8()
    //{
    //    setOutLine(CON_Tube, true);
    //    setClick(CON_Tube, () =>
    //    {
    //        PlayAnim("8", () =>
    //        {
    //            var tishi_xi = loadResource<TiShiObject>(TiShiPath + "细菌菌液");
    //            showPromptBox(tishi_xi.GetText, tishi_xi.imagePath, s9);

    //        });

    //    });
    //}


    ///// <summary>
    ///// 玻璃片
    ///// </summary>
    //private void s9()
    //{
    //    setOutLine(CON_Glass, true);
    //    setClick(CON_Glass, () =>
    //    {
    //        PlayAnim("9",s10);

    //    });
    //}


    ///// <summary>
    ///// 灭菌器
    ///// </summary>
    //private void s10()
    //{
    //    setOutLine(CON_Sterilizer, true);
    //    setClick(CON_Sterilizer, () =>
    //    {
    //        PlayAnim("10",()=>
    //        {
    //            var tishi_nie = loadResource<TiShiObject>(TiShiPath + "镊子");
    //            showPromptBox(tishi_nie.GetText, tishi_nie.imagePath,s11);
    //        });
    //    });
    //}


    ///// <summary>
    ///// 镊子
    ///// </summary>
    //private void s11()
    //{
    //    setOutLine(CON_Nie, true);
    //    setClick(CON_Nie, () =>
    //    {
    //        PlayAnim("11",s12);
    //    });
    //}


    ///// <summary>
    ///// 盖玻片盒
    ///// </summary>
    //private void s12()
    //{
    //    setOutLine(CON_hezi, true);
    //    setClick(CON_hezi, () =>
    //    {
    //        PlayAnim("12", s13);
    //    });
    //}


    ///// <summary>
    ///// 灭菌器
    ///// </summary>
    //private void s13()
    //{
    //    setOutLine(CON_Sterilizer, true);
    //    setClick(CON_Sterilizer, () =>
    //    {
    //        var camerapos = loadResource<CameraPosRotObject>(CameraPosRotPath + "灭菌器");
    //        cameraMoveTo(camerapos.pos, 1f,s14);
    //    });
    //}


    ///// <summary>
    ///// 灭菌器开关
    ///// </summary>
    //private void s14()
    //{
    //    setOutLine(CON_Sterilizer_Switch, true);
    //    setClick(CON_Sterilizer_Switch, () =>
    //    {
    //        PlayAnim("13", () =>
    //        {
    //            getTarget<Light>(CON_Sterilizer + "/PointLight").intensity = 0;
    //            var camerapos = loadResource<CameraPosRotObject>(CameraPosRotPath + "起始点");
    //            cameraMoveTo(camerapos.pos, 1f, s15);
    //        });

    //    });
    //}


    ///// <summary>
    ///// 玻璃片
    ///// </summary>
    //private void s15()
    //{
    //    setOutLine(CON_Glass, true);
    //    setClick(CON_Glass, () =>
    //    {
    //        PlayAnim("14", null);
    //    });
    //}
    #endregion





    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 10;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            Time.timeScale = 1;
        }
    }


}
