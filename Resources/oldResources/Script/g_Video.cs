using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class g_Video : MonoBehaviour
{
    #region 变量

    [Header("视频clip")]
    public VideoClip[] videoClip;

    [Header("音频clip")]
    public AudioClip[] audioClip;
    [Header("视频对象")]
    public VideoPlayer video;
    [Header("音频组件")]
    public AudioSource audio;
    [Header("进度条")]
    public Slider videoSlider, audioVolumeSlider;
    [Header("时间")]
    public Text time1, time2;
    [Header("播放、暂停图标")]
    public Sprite playImg, stopImg;
    [Header("播放、暂停按钮")]
    public Image stopOrPlay;
    private int i = 0;
    #endregion


    //进度条拖拽判定
    private bool drop = false;
    // Use this for initialization
    void Start()
    {
        //初始化组件属性
        video.clip = videoClip[i];
        video.playOnAwake = true;
        video.waitForFirstFrame = true;
        video.isLooping = false;
        video.playbackSpeed = 1f;
        

        stopOrPlay.sprite = stopImg;
        audio.volume = 0.5f;
        audioVolumeSlider.value = audio.volume;
        time2.text = ((int)video.clip.length).ToString();
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //滚动鼠标中键增加/降低音量
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            audioVolumeSlider.value += 0.1f;
        }
        if(Input.GetAxis("Mouse ScrollWheel")<0)
        {
            audioVolumeSlider.value -= 0.1f;
        }
        //音量调整
        audio.volume = audioVolumeSlider.value;
        
        //进度条被拖动时
        if(!drop)
        {
            time1.text = ((int)video.time).ToString();
            videoSlider.value = (float)video.frame / ((float)video.frameCount);
        }

        if (video.frame==(int)video.frameCount)
        {
            //播放下一个视频
            if(i<videoClip.Length-1)
            {
                i++;
                video.clip = videoClip[i];
                time2.text = ((int)video.clip.length).ToString();
            }
            else  if (i == videoClip.Length - 1) //显示图片,配音
            {
                i++;
                g_BGUIManager.imgAction();
                audio.PlayOneShot(audioClip[3]);
            }
        }
    }

    /// <summary>
    /// 拖动进度条改变视频进度
    /// </summary>
    public void DropSlider()
    {
        drop = true;
        time1.text = ((int)(video.clip.length* videoSlider.value)).ToString();
    }

    /// <summary>
    /// 退出拖动进度条
    /// </summary>
    public void ExitDropSlider()
    {
        drop = false;
        video.frame = (long)(videoSlider.value * (float)video.frameCount) - 1;
    }
    
    /// <summary>
    /// 播放和暂停
    /// </summary>
    public void StopAndPlay()
    {
        //播放和暂停图标切换，视频播放或暂停
        if (video.isPlaying)
        {
            video.Pause();
            stopOrPlay.sprite = playImg;
        }
        else
        {
            video.Play();
            stopOrPlay.sprite = stopImg;
        }
    }
}
