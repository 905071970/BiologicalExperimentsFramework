using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptObject/ChoiceObject")]
public class ChoiceObject : ScriptableObject
{
    /// <summary>
    /// 题目
    /// </summary>
    public string title;

    public Choice[] choices;

}

/// <summary>
/// 选项
/// </summary>
[System.Serializable]
public class Choice
{
    public string str;
    public bool isCorrect;
}
