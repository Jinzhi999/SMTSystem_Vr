using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeviceTest : MonoBehaviour
{

    List<Device> devList = new List<Device>();
    public static bool isPos = true;
    void Start()
    {
       
    }

    public void InitToTarPro()
    {
        if (devList.Count == 0)
        {
            devList = transform.GetComponentsInChildren<Device>().ToList();
        }
        foreach (var item in devList)
        {
            item.OnLockPro();
        }
        devList[0].OnInitPro();
        isPos = true;
    }

    public void InitToInitPro()
    {
        if (devList.Count == 0)
        {
            devList = transform.GetComponentsInChildren<Device>().ToList();
        }
        foreach (var item in devList)
        {
            item.OnLockPro();
        }
        devList[devList.Count-1].OnInitPro();
        isPos = false;
    }

    public void HideAllHightLight()
    {
        if (devList.Count == 0)
        {
            devList = transform.GetComponentsInChildren<Device>().ToList();
        }
        foreach (var item in devList)
        {
            item.highlight(false);
        }
    }
}
