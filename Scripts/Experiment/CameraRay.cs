using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    private static CameraRay cameraRay;
    public static CameraRay GetCameraRay
    {
        get
        {
            if(cameraRay == null)
            {
                cameraRay = Camera.main.transform.GetComponent<CameraRay>();
            }
            return cameraRay;
        }
    }

    private bool canRay = true;

    public void SetCanRay(bool b)
    {
        canRay = b;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool ishit = Physics.Raycast(ray, out var hit);

        if (ishit)
        {
            var inter = hit.collider.gameObject.GetComponent<Interaction>();
            if (inter != null)
            {
                //是交互物品
                inter.OnRay(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(0) && canRay)
        {
            if (ishit)
            {
                var inter = hit.collider.gameObject.GetComponent<Interaction>();
                if (inter != null)
                {
                    //是交互物品
                    inter.OnClick(hit.point);
                }
            }
        }
    }
}
