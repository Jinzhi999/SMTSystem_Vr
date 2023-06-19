using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// 一字螺丝
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class SlottedScrew : DeviceBase
{
    /// <summary>
    /// 是否拧松
    /// </summary>
    public bool is_screw_off;

    /// <summary>
    /// 是否可放螺丝刀
    /// </summary>
    public bool can_hold_screwdrive = true;

    /// <summary>
    /// 螺丝刀
    /// </summary>
    public SlottedScrewdriver Screwdriver;

    /// <summary>
    /// 螺丝刀放置
    /// </summary>
    public Transform placeTra;

    /// <summary>
    /// 初始状态
    /// </summary>
    public Vector3 initPos, initRot;
    public Vector3 unscrewPos, unscrewRot;

    /// <summary>
    /// 操作时间
    /// </summary>
    public float opTime;

    public override void Start()
    {
        base.Start();
        initPos = transform.localPosition;
        initRot = transform.localEulerAngles;
    }

    public bool OnScrewdriverPlace(SlottedScrewdriver screwdriver)
    {
        if (!can_hold_screwdrive) return false;

        Screwdriver = screwdriver;
        screwdriver.transform.SetParent(placeTra);
        screwdriver.transform.localPosition = Vector3.zero;
        screwdriver.transform.localEulerAngles = Vector3.zero;
        //highlighter.highlighted = false ;
        if (!is_screw_off)
        {
            Relax();
        }
        else
        {
            Tighten();
        }
        return true;
    }

    /// <summary>
    /// 拧紧
    /// </summary>
    protected virtual void Tighten()
    {
        float time = 1f;
        DOTween.To(() => time, x => time = x, 1, .5f).OnComplete(() =>
        {
            transform.DOLocalMove(initPos, opTime).OnComplete(() =>
            {
                is_screw_off = false;
                Screwdriver.FinishOp();
                FinishEvent(true);
            });
            transform.DOLocalRotate(initRot, opTime);
        });
    }

    /// <summary>
    /// 拧松
    /// </summary>
    protected virtual void Relax()
    {
        float time = 1f;
        DOTween.To(() => time, x => time = x, 1, .5f).OnComplete(() =>
        {
            transform.DOLocalMove(unscrewPos, opTime).OnComplete(() =>
            {
                is_screw_off = true;
                Screwdriver.FinishOp();
                FinishEvent(false);
            });
            transform.DOLocalRotate(unscrewRot, opTime);
        });
    }

    /// <summary>
    /// 完成后事件
    /// </summary>
    protected virtual void FinishEvent(bool isTughtOrRelax)
    {

    }
}
