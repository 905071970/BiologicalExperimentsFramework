using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExNameText : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private GameObject background;


    private Coroutine coroutine;

    public void show(Vector3 pos,string str)
    {
        background.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        text.text = str;
        Vector3 screen = Camera.main.WorldToScreenPoint(pos);
        transform.position = screen;

        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(delayhide());
    }

    private IEnumerator delayhide()
    {
        yield return new WaitForSeconds(0.1f);
        hide();
    }

    public void hide()
    {
        background.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
