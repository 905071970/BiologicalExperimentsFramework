using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text mudi;
    [SerializeField]
    private Text yuanli;
    [SerializeField]
    private Button button;

    public void show(string title,string mudi,string yuanli,Action action)
    {
        this.gameObject.SetActive(true);
        this.title.text = title;
        this.mudi.text = mudi;
        this.yuanli.text = yuanli;
        this.button.onClick.RemoveAllListeners();
        this.button.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);

            if(action != null)
            {
                action();
            }
        });
    }
}
