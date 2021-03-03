using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;


public class SingleChoice : MyBaseUI
{
    public Toggle answer;
    public bool comfirm = false;
    public Button button;//确认
    public GameObject correctShow;//回答正确
    public GameObject errorShow;//回答错误
    


    public override void show(Action action)
    {
        base.show(action);
        comfirm = false;
    }



    public void BackQuest()
    {
        errorShow.SetActive(false);
    }

    public void EndQuest()
    {
        correctShow.SetActive(false);
        end();
    }
}

