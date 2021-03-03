using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Loading: MonoBehaviour
{
    public static Loading proLoading;
    public Image loadImg;
    AsyncOperation async;
    static string nextSceneName;
    float f = 0;

    private void Awake()
    {
        if (proLoading == null)
            proLoading = this;
    }

    private void Start()    //不能用Awake，会直接加载新场景
    {
        
        if (SceneManager.GetActiveScene().name== "loading")
        {
            async = SceneManager.LoadSceneAsync(nextSceneName);
            async.allowSceneActivation = false;

            StartCoroutine("LoadScnen");
        }
    }
    IEnumerator LoadScnen()     //过渡场景动画
    {

        //实时进度
        while(f<=1f)
        {
            f += Time.deltaTime*2;
            loadImg.fillAmount = Mathf.Lerp(0, async.progress / 9 * 10, f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        async.allowSceneActivation = true;
        yield return null;
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void NextScene(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene("loading");
    }
}