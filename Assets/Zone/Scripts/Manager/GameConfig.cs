using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{

    /// <summary>
    /// 协同交互服务器地址
    /// </summary>
    public static string Ip => ip;
    /// <summary>
    /// 用户名
    /// </summary>
    public static string UserName => userName;

    private static string ip;
    private static string userName;

    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
}
