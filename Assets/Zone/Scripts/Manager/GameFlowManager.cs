using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class GameFlowManager : MonoBehaviour
{
    public static bool needRefreshList=true;
    public static string mediaPath =Application.streamingAssetsPath;
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            mediaPath = Application.persistentDataPath;
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("删除ip配置")]
    void DeleteKey()
    {
        if (PlayerPrefs.HasKey("ipConfig"))
        {
            PlayerPrefs.DeleteKey("ipConfig");
            Debug.Log("删除配置成功");
        }
    }
    [ContextMenu("ce")]
    void Ce()
    {
        string path = Application.streamingAssetsPath + "/媒体文件";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        else
        {
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] dis = di.GetDirectories();
            foreach (var item in dis)
            {
                Debug.Log(File.Exists(item.FullName)+"   "+item.FullName);
                Directory.Delete(item.FullName, true);
            }
        }
    }
}
