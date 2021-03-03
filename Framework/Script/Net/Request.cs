using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : Singleton<Request> {

    /// <summary>
    /// 登录验证
    /// </summary>
    public void sign_in(string username,string userpassword)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("userpassword", userpassword);

        NetClient.GetNetClient.StartRequest(form, "http://1.15.56.187/PHP/sign_in.php");
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="username"></param>
    /// <param name="userpassword"></param>
    public void sign_up(string username,string userpassword)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("userpassword", userpassword);

        NetClient.GetNetClient.StartRequest(form, "http://1.15.56.187/PHP/sign_up.php");
    }


    /// <summary>
    /// 获取关卡数
    /// </summary>
    /// <param name="username"></param>
    public void getLevelCount(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);

        NetClient.GetNetClient.StartRequest(form, "http://1.15.56.187/PHP/getLevelCount.php");
    }

    /// <summary>
    /// 更新关卡数
    /// </summary>
    /// <param name="username"></param>
    /// <param name="level"></param>
    public void updateLevelCount(string username,int level)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("level", level);

        NetClient.GetNetClient.StartRequest(form, "http://1.15.56.187/PHP/updateLevelCount.php");
    }
}
