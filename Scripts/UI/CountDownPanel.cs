using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CountDownPanel : MyBaseUI
{
    private Animator animator;



    private void Awake()
    {
        //countDownText = transform.Find("timeText").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }


    public override void show(Action action)
    {
        base.show(action);

        StartCoroutine(videoComplete());
    }

    private IEnumerator videoComplete()
    {
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                break;
            }
            yield return null;
        }
        end();
    }


    
}
