using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameControl;

public class SterilizerPanel : MyBaseUI
{
    public GameObject Thermometer;
    

    public override void show(Action action)
    {
        base.show(action);
        StartCoroutine(delayshow(action));
    }


    private IEnumerator delayshow(Action action)
    {
        yield return new WaitForSeconds(3f);
        Thermometer.SetActive(true);
        Thermometer.GetComponentInChildren<Thermometer1>().show(this);
    }


}
