using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Introduced : MyBaseUI
{
    [SerializeField]
    private Text text;


    public void show(string str,Action action)
    {
        base.show(action);
        this.gameObject.SetActive(true);
        text.text = str;

        StartCoroutine(hide());
    }

    public IEnumerator hide()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
        end();
    }
}
