
using UnityEngine;
using UnityEngine.UI;

public class g_equipment : MonoBehaviour
{
    public Slider content, slider;
    public RectTransform box;
    // Use this for initialization
    void Start()
    {
        content.value = 1f;
        slider.value = 0f;

        //初始化界面
        box.sizeDelta = new Vector2( 0, box.parent.GetComponent<RectTransform>().rect.height * 2);

    }
    
    void Update()
    {
        //上下滚动
        if(Input.GetAxis("Mouse ScrollWheel")>0)
        {
            content.value += 0.1f;
        }
        if(Input.GetAxis("Mouse ScrollWheel")<0)
        {
            content.value -= 0.1f;
        }
        //slider.value同步
        slider.value = 1 - content.value;
    }
}
