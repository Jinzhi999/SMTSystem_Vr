using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// 打开锁扣
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class Buckle : DeviceBase
{
    /// <summary>
    /// 是否打开
    /// </summary>
    public bool isOpen=false;
    /// <summary>
    /// 正在操作
    /// </summary>
    bool isOperation = false;
    public Vector3 tarAngle;
    protected override void OnHandHoverBegin(Hand hand)
    {
        Operation();
    }
    public void Operation()
    {
        if (isOperation)
            return;
        isOperation = true;
        Vector3 angle = isOpen ?  new Vector3(0,0,0): tarAngle;
        transform.DOLocalRotate(angle, 2).OnComplete(() => {
            isOperation = false;
            isOpen = !isOpen;
            // 更新锁扣状态
            transform.GetComponentInParent<BuckleParent>().UpdateDoorState();
        });
    }
}
