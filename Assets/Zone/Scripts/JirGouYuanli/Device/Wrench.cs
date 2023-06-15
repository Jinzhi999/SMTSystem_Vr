using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
/// <summary>
/// 扳手
/// </summary>
[RequireComponent(typeof(Throwable))]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class Wrench : DeviceBase
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
    protected override void HandHoverUpdate(Hand hand)
    {
        //if (ConfirmBtn.GetStateDown(hand.handType))
        //{
        //    if (showObj) showObj.SetActive(true);

        //    if (!Confirm && BPM.instance.CurrentStep == PeekStepID())
        //    {
        //        Confirm = true;
        //        //BPM.instance.BPMUpdate(DequeueStepID(), "已完成");
        //    }
        //}
    }
    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);
        Debug.Log("离开手里");
        inHand = false;
        inRelease = true;
        Invoke("InRree", 0.2f);
    }

    public void OnTriggerStay(Collider other)
    {
        if (!inRelease) return;
        if (inUse) return;
        Nut nut = other.GetComponent<Nut>();
        if (nut == null)
        {
            return;
        }
        inUse = true;
        nut.OnWrenchPlace(this);
        SetInterable(false);
    }
    public void FinishRot()
    {
        transform.parent = null;
        inUse =false;
    
    }

    void InRree()
    {
        inRelease = false;
    }
}
