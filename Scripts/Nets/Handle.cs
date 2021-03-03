using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle: Singleton<Handle>
{

    private Dictionary<string, Action<string>> actions = new Dictionary<string, Action<string>>();


	public void HandleMsg(string res)
    {
        try
        {
            if (res.Equals(""))
            {
                return;
            }

            string head = res.Split('#')[0];//处理消息类型

            if (actions.ContainsKey(head))
            {
                actions[head](res);
            }
            else
            {
                Debug.LogError(res);
                Debug.LogError("没有找到对应处理方法");
            }
            

        }
        catch(Exception ex)
        {
            Debug.LogWarning(res);
            Debug.LogError(ex.Message);
        }
    }


    public void AddAction(string key,Action<string> action)
    {
        actions[key] = action;
    }
}
