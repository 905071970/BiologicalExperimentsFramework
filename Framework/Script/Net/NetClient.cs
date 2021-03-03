using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Text;
using UnityEngine.UI;

public class NetClient : MonoBehaviour 
{
    private static NetClient netClient;
    public static NetClient GetNetClient
    {
        get
        {
            if(netClient == null)
            {
                netClient = GameObject.FindGameObjectWithTag("NetClient").GetComponent<NetClient>();
            }
            return netClient;
        }
    }



    public void StartRequest(WWWForm form,string phpLink)
    {
        StartCoroutine(request_php(form, phpLink));
    }

    private IEnumerator request_php(WWWForm form,string phpLink)
    {
        yield return new WaitForSeconds(0.1f);

        WWW www = new WWW(phpLink, form);
        yield return www;

        Handle.GetInstance.HandleMsg(www.text);
    }



}
