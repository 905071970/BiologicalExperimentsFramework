
using UnityEngine;

public class Back : MonoBehaviour
{
    public void GoBack()
    {
        //要返回的界面对象
        transform.parent.gameObject.GetComponent<g_Parent>().g_parent.SetActive(true);
        //关闭当前界面对象
        transform.parent.gameObject.SetActive(false);
    }
}
