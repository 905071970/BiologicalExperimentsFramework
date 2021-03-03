using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;



[CustomEditor(typeof(EditorManager))]
public class MyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorManager editorManager = target as EditorManager;
        if (GUILayout.Button("摄像机回到起始位置"))
        {
            editorManager.CameraToStartPos();
        }
        if (GUILayout.Button("设置摄像机当前位置为起始位置"))
        {
            editorManager.SetCameraStartPos();
        }


        editorManager.SaveName = EditorGUILayout.TextField("保存名字：", editorManager.SaveName);
        if (GUILayout.Button("保存当前摄像机位置信息"))
        {
            editorManager.SaveCameraPosRot();
        }
    }
}
#endif