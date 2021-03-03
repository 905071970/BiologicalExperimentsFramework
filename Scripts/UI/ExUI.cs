using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExUI : BaseUI
{
    [SerializeField]
    private Button back_button;


    private void Awake()
    {
        back_button = transform.Find("Button").GetComponent<Button>();

        back_button.onClick.AddListener(() =>
        {
            clsGlobe.prsnextSceneName = "MainScene";
            SceneManager.LoadScene("loading");
            UIManager.GetInstance.RemoveTheCurrentFromTheScreenUI();
        });
    }
}
