using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Introduced : MonoBehaviour
{
    [SerializeField]
    private Text text;


    private Action action;

    public void show(string str,Action action)
    {
        this.gameObject.SetActive(true);
        text.text = str;
        this.action = action;

        StartCoroutine(hide());
    }

    public IEnumerator hide()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}
