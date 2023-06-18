using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// һ����˿
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class SlottedScrew : DeviceBase
{
    /// <summary>
    /// �Ƿ�š��
    /// </summary>
    public bool is_screw_off;

    /// <summary>
    /// �Ƿ�ɷ���˿��
    /// </summary>
    public bool can_hold_screwdrive = true;

    /// <summary>
    /// ��˿��
    /// </summary>
    public SlottedScrewdriver Screwdriver;

    /// <summary>
    /// ��˿������
    /// </summary>
    public Transform placeTra;

    /// <summary>
    /// ��ʼ״̬
    /// </summary>
    public Vector3 initPos, initRot;
    public Vector3 unscrewPos, unscrewRot;

    /// <summary>
    /// ����ʱ��
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
    /// š��
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
    /// š��
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
    /// ��ɺ��¼�
    /// </summary>
    protected virtual void FinishEvent(bool isTughtOrRelax)
    {

    }
}
