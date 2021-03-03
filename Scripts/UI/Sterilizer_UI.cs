using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sterilizer_UI : BaseUI
{
    private GameObject text;
    private GameObject thermonmeter;




    private void Awake()
    {
        m_UIType = UIType.top;

        text = transform.Find("Text").gameObject;
        thermonmeter = transform.Find("thermometer").gameObject;

        thermonmeter.SetActive(false);
    }


    public override void OnEnter()
    {
        base.OnEnter();
        text.SetActive(true);
        StartCoroutine(delayShow());
    }

    private IEnumerator delayShow()
    {
        yield return new WaitForSeconds(2f);
        thermonmeter.SetActive(true);
    }
}
