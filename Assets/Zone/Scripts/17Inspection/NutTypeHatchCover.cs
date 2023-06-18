using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// ��ĸ�Ͳո�
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class NutTypeHatchCover : DeviceBase
{
    public bool inHand = false;

    /// <summary>
    /// �Ƿ��ѷ����տ�
    /// </summary>
    public bool inPlaced = true;

    public bool inRelease = false;

    /// <summary>
    /// �ոǷ��ô�
    /// </summary>
    public Hatch Hatch;

    /// <summary>
    /// �ո���ĸ
    /// </summary>
    public SlottedScrew[] screwList;

    /// <summary>
    /// �Ƿ�������˿����
    /// </summary>
    public bool all_screws_off => Array.TrueForAll(screwList, screw => screw.is_screw_off);

    public override void Start()
    {
        base.Start();
        screwList = GetComponentsInChildren<SlottedScrew>();
        SetInterable(all_screws_off);
        Hatch = GetComponentInParent<Hatch>();
        inPlaced = (bool)Hatch;
        Array.ForEach(screwList, screw => screw.can_hold_screwdrive = (bool)Hatch);
    }

    protected override void OnHandHoverBegin(Hand hand)
    {
        base.OnHandHoverBegin(hand);
        Debug.Log("�ִ���");
        SetInterable(all_screws_off);
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);
        Debug.Log("��������");
        inHand = true;
        inPlaced = false;
        if (Hatch != null)
        {
            Hatch.hatchCover = null;
            Hatch = null;
        }
        SetScrewsState();
    }

    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);
        Debug.Log("�뿪����");
        inHand = false;
        inRelease = true;
        Invoke("InFree", 0.2f);
    }

    public void OnTriggerStay(Collider other)
    {
        if (!inRelease) return;
        if (inPlaced) return;
        Hatch hatch = other.GetComponent<Hatch>();
        if (hatch == null || hatch.inUse)
        {
            return;
        }
        inPlaced = true;
        hatch.Place(this);
        Hatch = hatch;
        SetScrewsState();
    }

    void InFree()
    {
        inRelease = false;
    }

    /// <summary>
    /// ���ø�����˿�Ƿ�ɷ���˿������
    /// </summary>
    public void SetScrewsState()
    {
        Array.ForEach(screwList, screw => { screw.can_hold_screwdrive = Hatch != null; });
    }
}
