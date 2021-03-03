using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    [Header("主摄像机起始位置")]
    [SerializeField]
    private Vector3 startPos;

    public string SaveName
    {
        get;set;
    }

    public void CameraToStartPos()
    {
        Camera.main.transform.position = startPos;
    }

    public void SetCameraStartPos()
    {
        startPos = Camera.main.transform.position;
    }

    public void SaveCameraPosRot()
    {
        CameraPosRotObject obj = ScriptableObject.CreateInstance<CameraPosRotObject>();
        obj.pos = Camera.main.transform.position;
        obj.rot = Camera.main.transform.rotation;
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(obj, "Assets/Resources/ScriptObject/CameraPosRot/"+SaveName+".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }
}
