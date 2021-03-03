using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptObject/TiShiObject")]
public class TiShiObject : ScriptableObject
{
    [TextArea(3,10)]
    [SerializeField]
    private string Text;
    public string imagePath;


    public string GetText
    {
        get
        {
            return Text.Replace('&', '\n');
        }
    }
}
