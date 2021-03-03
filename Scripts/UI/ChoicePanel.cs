using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MyBaseUI
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private GameObject toggles;//所有选框
    [SerializeField]
    private Button button;//确认按钮
    [SerializeField]
    private GameObject correctShow;//回答正确
    [SerializeField]
    private GameObject errorShow;//回答错误


    private ChoiceObject choiceObject;

    private void Awake()
    {
        correctShow.GetComponentInChildren<Button>().onClick.AddListener(EndQuest);
        errorShow.GetComponentInChildren<Button>().onClick.AddListener(BackQuest);
        button.onClick.AddListener(OnButtonClick);
    }


    public void show(ChoiceObject choiceObject,Action action)
    {
        base.show(action);
        this.choiceObject = choiceObject;

        SwitchSingleOrMultiple();
        UpdateContent();
    }

    /// <summary>
    /// 切换单选或多选
    /// </summary>
    private void SwitchSingleOrMultiple()
    {
        int count = 0;
        foreach(var v in choiceObject.choices)
        {
            if (v.isCorrect)
            {
                count++;
            }
        }

        var alltoggle = toggles.GetComponentsInChildren<Toggle>();
        if(count > 1)
        {
            //多选
            foreach(var v in alltoggle)
            {
                v.group = null;
            }
        }
        else
        {
            //单选
            var toggleGroup = toggles.GetComponent<ToggleGroup>();
            foreach (var v in alltoggle)
            {
                v.group = toggleGroup;
            }
        }
    }

    /// <summary>
    /// 更新选项内容和题目
    /// </summary>
    private void UpdateContent()
    {
        title.text = choiceObject.title;
        var alltoggle = toggles.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < alltoggle.Length; i++)
        {
            alltoggle[i].GetComponentInChildren<Text>().text = choiceObject.choices[i].str;
        }
    }

    private void OnButtonClick()
    {
        var alltoggle = toggles.GetComponentsInChildren<Toggle>();
        for(int i = 0; i < alltoggle.Length; i++)
        {
            if(alltoggle[i].isOn != choiceObject.choices[i].isCorrect)
            {
                //回答错误
                errorShow.SetActive(true);
                return;
            }
        }
        //回答正确
        correctShow.SetActive(true);
    }
    


    /// <summary>
    /// 回答错误
    /// </summary>
    public void BackQuest()
    {
        errorShow.SetActive(false);
    }

    /// <summary>
    /// 回答正确
    /// </summary>
    public void EndQuest()
    {
        correctShow.SetActive(false);
        end();
    }
}
