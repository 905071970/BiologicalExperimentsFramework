using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorText : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private GameObject background;


    private Coroutine coroutine;

    public void show(Vector3 pos)
    {
        background.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        Vector3 screen = Camera.main.WorldToScreenPoint(pos);
        transform.position = screen;

        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(hide());
    }

    private IEnumerator hide()
    {
        yield return new WaitForSeconds(2f);
        background.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
