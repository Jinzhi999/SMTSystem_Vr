using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// 火工品扳手
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
public class PyrotechnicsWrench : DeviceBase
{
    public bool inHand = false;

    /// <summary>
    /// 是否已附加火工品帽
    /// </summary>
    public bool inUsed = true;

    public bool inRelease = false;

    public override void Start()
    {
        base.Start();
    }

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
        if (inUsed) return;

        PyrotechnicsSafelyHat hat = other.GetComponent<PyrotechnicsSafelyHat>();
        if (hat == null || hat.inTool)
        {
            return;
        }

        inUsed = true;
        hat.OnWrenchPlace(this);
    }
}
