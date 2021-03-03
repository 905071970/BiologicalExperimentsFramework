using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptObject/StartExObject")]
public class StartExObject : ScriptableObject
{
    public string Title;
    [TextArea(3,10)]
    public string Mudi;
    [TextArea(3, 10)]
    public string Yuanli;
}
