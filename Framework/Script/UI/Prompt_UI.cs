using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prompt_UI : BaseUI {

    private GameObject prompt_item;

    private Coroutine coroutine;

    private void Awake()
    {
        m_UIType = UIType.top;

        prompt_item = transform.Find("Prompt_Item").gameObject;
    }


    public override void OnEnter()
    {
        base.OnEnter();
        Hide();
    }

    public override void OnExit()
    {
        base.OnExit();
    }


    public void Show(string str)
    {
        prompt_item.SetActive(true);
        prompt_item.GetComponentInChildren<Text>().text = str;
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(hideTime());
    }

    private IEnumerator hideTime()
    {
        yield return new WaitForSeconds(2f);
        Hide();
    }

    private void Hide()
    {
        prompt_item.SetActive(false);
    }
}
