using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
/// <summary>
/// 六角扳手
/// </summary>
[RequireComponent(typeof(Throwable))]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class HexagonWrench : Wrench
{
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
}
