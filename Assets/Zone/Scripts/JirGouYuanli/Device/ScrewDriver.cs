using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class InpendDevice : DeviceBase
{ 
    public GameObject showObj;
    protected override void HandHoverUpdate(Hand hand)
    {
        //if (ConfirmBtn.GetStateDown(hand.handType))
        //{
        //    if (showObj) showObj.SetActive(true);

        //    if (!Confirm && BPM.instance.CurrentStep == PeekStepID())
        //    {
        //        Confirm = true;
        //        //BPM.instance.BPMUpdate(DequeueStepID(), "ÒÑÍê³É");
    
        //    }
        //}
    }
}
