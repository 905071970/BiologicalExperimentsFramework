using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecimenPanel : MyBaseUI
{
    [SerializeField]
    private Image image;

    public override void show(Action action)
    {
        base.show(action);

        StartCoroutine(imageswitch());

    }

    private IEnumerator imageswitch()
    {
        int i = 0;
        while (i < 3)
        {
            yield return new WaitForSeconds(0.5f);
            image.sprite = Resources.Load<Sprite>("Images/标本图1");
            yield return new WaitForSeconds(0.5f);
            image.sprite = Resources.Load<Sprite>("Images/标本图2");
            yield return new WaitForSeconds(0.5f);
            image.sprite = Resources.Load<Sprite>("Images/标本图3");
            i++;
        }

        end();
    }
}
