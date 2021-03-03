using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    private Dictionary<string, string> uiPathDict = new Dictionary<string, string>();

    private Dictionary<string, BaseUI> uiDict = new Dictionary<string, BaseUI>();

    private Stack<BaseUI> uiStack = new Stack<BaseUI>();

    private Transform normalRoot;

    private Transform topRoot;


    private Prompt_UI m_Prompt_UI;
    private EXPrompt_UI m_EXPrompt_UI;

    public void Init()
    {
        normalRoot = GameObject.FindGameObjectWithTag("NormalRoot").transform;
        topRoot = GameObject.FindGameObjectWithTag("TopRoot").transform;

        uiPathDict.Add("Prompt_UI", "UI/Prompt_UI");
        uiPathDict.Add("Sign_in_UI", "UI/Sign_in_UI");
        uiPathDict.Add("Sign_up_UI", "UI/Sign_up_UI");
        uiPathDict.Add("Main_UI", "UI/Main_UI");
        uiPathDict.Add("Course_UI", "UI/Course_UI");
        uiPathDict.Add("EXPrompt_UI", "UI/EXPrompt_UI");
        uiPathDict.Add("Sterilizer_UI", "UI/Sterilizer_UI");
        uiPathDict.Add("HomeUI", "UI/HomeUI");
        uiPathDict.Add("ExUI", "UI/ExUI");

        m_Prompt_UI = ShowUIToScreen("Prompt_UI").GetComponent<Prompt_UI>();
        m_EXPrompt_UI = ShowUIToScreen("EXPrompt_UI").GetComponent<EXPrompt_UI>();
    }

    /// <summary>
    /// 添加UI到屏幕上
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns></returns>
    public BaseUI ShowUIToScreen(string uiName)
    {
        
        BaseUI ui = null;

        if (uiDict.ContainsKey(uiName))
        {
            ui = uiDict[uiName];
            ui.OnEnter();
            if(ui.GetUIType == UIType.normal)
            {
                if (uiStack.Count > 0)
                {
                    uiStack.Peek().OnExit();
                }
                uiStack.Push(ui);
            }
            
        }
        else
        {
            if (uiPathDict.ContainsKey(uiName))
            {
                string path = uiPathDict[uiName];
                GameObject uiPrefab = Resources.Load<GameObject>(path);
                GameObject uiObj = GameObject.Instantiate(uiPrefab);
                
                ui = uiObj.GetComponent<BaseUI>();
                ui.OnEnter();
                uiDict[uiName] = ui;

                if (ui.GetUIType == UIType.normal)
                {
                    if (uiStack.Count > 0)
                    {
                        uiStack.Peek().OnExit();
                    }
                    uiObj.transform.SetParent(normalRoot, false);
                    uiStack.Push(ui);
                }
                else
                {
                    uiObj.transform.SetParent(topRoot, false);
                }
            }
        }


        return ui;
    }

    /// <summary>
    /// 移除当前屏幕上的UI
    /// </summary>
    public void RemoveTheCurrentFromTheScreenUI()
    {
        if (uiStack.Count > 0)
        {
            uiStack.Pop().OnExit();
            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnEnter();
            }
        }
    }


    public void RemoveTopUI(string name)
    {
        if(uiDict.TryGetValue(name,out var baseUI))
        {
            baseUI.OnExit();
        }
    }


    public void ShowPrompt(string str)
    {
        if(m_Prompt_UI != null)
        {
            m_Prompt_UI.Show(str);
        }
    }


    public void ShowError(Vector3 pos)
    {
        m_EXPrompt_UI.showError(pos);
    }

    public void ShowTiShi(string str,string imagePath,Action action)
    {
        m_EXPrompt_UI.showTiShi(str, imagePath, action);
    }

    public void showStartEX(string title, string mudi, string yuanli, Action action)
    {
        m_EXPrompt_UI.showStartEX(title, mudi, yuanli, action);
    }

    public void showJieShao(string str)
    {
        m_EXPrompt_UI.showJieShao(str);
    }
}
