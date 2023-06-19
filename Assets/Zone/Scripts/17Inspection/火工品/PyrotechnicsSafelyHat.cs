using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// 火工品安全保护帽
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Throwable))]
public class PyrotechnicsSafelyHat : DeviceBase
{
    public bool inHand = false;

    /// <summary>
    /// 是否已附加操作工具
    /// </summary>
    public bool inTool = true;

    public bool inRelease = false;

    /// <summary>
    /// 是否拧松
    /// </summary>
    public bool is_screw_off;//拧松后可拿

    public Vector3 off_Pos;
    /// <summary>
    /// 扳手吸附位置
    /// </summary>
    public Transform wrenchPlace;

    /// <summary>
    /// 正在使用的扳手工具
    /// </summary>
    public PyrotechnicsWrench wrenchTool;

    public override void Start()
    {
        base.Start();
        SetInterable(is_screw_off);
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
        Debug.Log("拿在手中");
        inHand = true;

        if (wrenchTool)
        {
            wrenchTool.inUsed = false;
            wrenchTool = null;
        }
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
        PyrotechnicsWrench wrench = other.GetComponent<PyrotechnicsWrench>();
        if (wrench == null || wrench.inUsed)
        {
            return;
        }



        ////安装
        //if (is_screw_off)
        //{
        //    if (inTool) return;
        //    PyrotechnicsPort port = other.GetComponent<PyrotechnicsPort>();
        //    if (port == null || port.inUsed)
        //    {
        //        return;
        //    }
        //}
        ////卸下 
        //else
        //{

        //}


    }

    void InFree()
    {
        inRelease = false;
    }

    public void OnWrenchPlace(PyrotechnicsWrench wrench)
    {
        wrenchTool = wrench;
        wrench.transform.SetParent(wrenchPlace);

        wrench.transform.localPosition = Vector3.zero;
        wrench.transform.localEulerAngles = Vector3.zero;
    }
}
