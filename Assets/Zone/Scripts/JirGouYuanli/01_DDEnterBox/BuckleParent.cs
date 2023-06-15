using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BuckleParent : MonoBehaviour
{
    /// <summary>
    /// 舱门
    /// </summary>
   public CircularDrive door;
    /// <summary>
    /// 锁扣s
    /// </summary>
    Buckle[] buckles;
    void Start()
    {
        buckles = transform.GetComponentsInChildren<Buckle>();
    }
    
    /// <summary>
    /// 更新锁扣状态
    /// </summary>
    public void UpdateDoorState()
    {
        bool isOpen = door.GetComponent<BoxCollider>().enabled;
        if (isOpen)
        {

        }
        else
        {
            // 全部打开  才可以开舱门
            bool allOpen = true;
            foreach (var item in buckles)
            {
                if (!item.isOpen)
                {
                    allOpen = false;
                    return;
                }
            }
            if (allOpen)
            {
                door.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

}
