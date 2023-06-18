using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// 一字螺丝刀
/// </summary>
[RequireComponent(typeof(Throwable))]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class SlottedScrewdriver : DeviceBase
{
    public bool inHand = false;
    public bool inUse = false;
    public bool inRelease = false;
    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
        Debug.Log("拿在手中");
        inHand = true;
    }

    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);
        Debug.Log("离开手里");
        inHand = false;
        inRelease = true;
        Invoke("InFree", 0.2f);
    }

    public void OnTriggerStay(Collider other)
    {
        if (!inRelease) return;
        if (inUse) return;
        SlottedScrew screw = other.GetComponent<SlottedScrew>();
        if (screw == null)
        {
            return;
        }

        if (screw.OnScrewdriverPlace(this))
        {
            inUse = true;
            SetInterable(false);
        }
    }

    /// <summary>
    /// 结束操作
    /// </summary>
    public void FinishOp()
    {
        transform.parent = null;
        inUse = false;
        SetInterable(true);
    }

    void InFree()
    {
        inRelease = false;
    }
}
