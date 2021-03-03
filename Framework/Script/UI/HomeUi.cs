using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUi : BaseUI
{
    public Transform content;
    public Text RIGHT_TOP_TXT;
    public Text CONTENT_TXT;
    public Button IsTrue;
    public Text incomplete;
    int index = 0;
    public static int[] number = { 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 ,0};

    string str;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GameObject.Find("1").GetComponent<Button>();
        RIGHT_TOP_TXT.text = btn.transform.Find("Text").GetComponent<Text>().text;
        CONTENT_TXT.text = btn.transform.Find("Text1").GetComponent<Text>().text;
        number[1] = 1;

        
    }

    public void Back()
    {
        UIManager.GetInstance.RemoveTheCurrentFromTheScreenUI();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Handle.GetInstance.AddAction("getLevelCount", getLevelCountCallback);

        if (PlayerPrefs.HasKey("username"))
        {
            Request.GetInstance.getLevelCount(PlayerPrefs.GetString("username"));
        }
        
    }
    
    private void getLevelCountCallback(string str)
    {
        int code = int.Parse(str.Split('#')[1]);
        if(code == 0)
        {
            //成功
            int levelCount = int.Parse(str.Split('#')[2]);
            for(int i = 0;i <= levelCount; i++)
            {
                //Complete(i);
            }
            
            Debug.Log("获取关卡数成功：" + levelCount);
        }
        else
        {
            //失败
            Debug.LogError("获取关卡数失败！");
        }
    }

    public void changeText(Transform obj)
    {
        RIGHT_TOP_TXT.text = obj.Find("Text").GetComponent<Text>().text;
        CONTENT_TXT.text = obj.Find("Text1").GetComponent<Text>().text;
        string temp = obj.name;
        index = Convert.ToInt32(temp);


        switch (index)
        {
            case 1:
                str = "Ex1_1";
                break;
            case 2:
                str = "Ex2_1";
                break;
            case 3:
                str = "experiment_3";
                break;
            case 4:
                str = "Experiment4-1";
                break;
            case 5:
                str = "5";
                break;
            case 7:
                str = "Experiment7-1";
                break;
            default:
                str = "Ex1_1";
                break;
        }
        /*Debug.Log(index);
        Debug.Log(number[index]);*/
    }


    public static void Complete(int index)
    {
        number[index] = 2;
        number[index + 1] = 1;
    }

    public void fnopenSence()
    {
        clsGlobe.prsnextSceneName = str;
        SceneManager.LoadScene("loading");
        UIManager.GetInstance.ShowUIToScreen("ExUI");
        //StartCoroutine(fun(str));
    }
    public IEnumerator fun(string _sence)
    {

        if (number[index] == 1)
        {
            incomplete.text = "准备进行下一个实验。";
            incomplete.gameObject.SetActive(true);
            for (int i = 0; i <= 3; i++)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log(i);
                if (i == 3)
                {
                    clsGlobe.prsnextSceneName = _sence;
                    SceneManager.LoadScene("loading");
                    UIManager.GetInstance.ShowUIToScreen("ExUI");
                }
            }
        }
        else if(number[index] == 2)
        {
            incomplete.text = "该实验已完成，请进行下一个实验。";
            incomplete.gameObject.SetActive(true);
            for (int i = 0; i <= 3; i++)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log(i);
                if (i == 3)
                {
                    clsGlobe.prsnextSceneName = _sence;
                    SceneManager.LoadScene("loading");
                    UIManager.GetInstance.ShowUIToScreen("ExUI");
                    incomplete.gameObject.SetActive(false);
                }
            }
        }
        else if (number[index] == 0)
        {
            incomplete.text = "上一个实验未完成，无法进行下一个实验。";
            incomplete.gameObject.SetActive(true);
            for (int i = 0; i <= 3; i++)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log(i);
                if (i == 3)
                {
                    incomplete.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            incomplete.gameObject.SetActive(false);
        }
    }
}
