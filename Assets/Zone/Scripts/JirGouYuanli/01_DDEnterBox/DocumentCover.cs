using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;

/// <summary>
/// 打开文件该
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class DocumentCover : DeviceBase
{
    bool inUse = false;
    protected override void OnHandHoverBegin(Hand hand)
    {
        Debug.Log("手触碰");
        highlighter.highlighted = false;
        //inUse = true;
        Door();
    }
    protected override void OnAttachedToHand(Hand hand)
    {
        Debug.Log("拿在手中");
        highlighter.highlighted = true;
        inUse = true;
    }
    /// <summary>
    /// 是否关闭
    /// </summary>
    bool isOpen = false;
    /// <summary>
    /// 正在操作
    /// </summary>
    bool isOperation = false;
    public void Door()
    {
        if (isOperation)
            return;
        isOperation = true;
        float angle = isOpen ? -90 : 20;
        transform.DOLocalRotate(new Vector3(0, angle, 0),2).OnComplete(()=> {
            isOperation = false;
            isOpen = !isOpen;
        });
    }
}
