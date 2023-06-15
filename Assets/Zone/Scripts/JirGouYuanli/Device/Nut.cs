using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;
/// <summary>
/// 螺母
/// </summary>
[RequireComponent(typeof(Throwable))]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class Nut : DeviceBase
{
    public bool inPlace = false;    // 是否放置在构建上
    public bool inHand = false;     // 是否在手上
    public bool isTighted = false;  // 是否拧紧
    NutBox nb;                      // 螺母槽 放置上才有
    Wrench wrench;                  // 扳手
    public Vector3  wrenchPos;  //扳手位置
    public Vector3  wrenchRot;  // 扳手角度
    public Vector3  initPos;    // 螺母初始位置
    public Vector3  initRot;    // 螺母初始角度
    public Vector3 tarPos;      // 螺母旋转后角度
    public Vector3 tarRot;      // 螺母旋转后角度

    public float rotTime =2;       // 拧紧时间

    public override void Start()
    {
        base.Start();
        if (isTighted)
        {
            SetInterable(false);
        }
        initPos = transform.localPosition;
        initRot = transform.localEulerAngles;

        //nb = transform.parent.GetComponentInChildren<NutBox>() ;
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        Debug.Log("拿在手中");
        //highlighter.highlighted = true;
        inHand = true;
    }
    protected override void HandHoverUpdate(Hand hand)
    {
    }
    protected override void OnDetachedFromHand(Hand hand)
    {
        Debug.Log("离开手里");
        inHand = false;
        //if (inPlace)
        //    return;
        //else
        //{
        //    Remove();
        //}
    }

    /// <summary>
    /// 拧紧
    /// </summary>
    public void OnWrenchPlace(Wrench wre)
    {
        wrench = wre;
        wre.transform.SetParent(transform);
        wre.transform.localPosition = wrenchPos;
        wre.transform.localEulerAngles = wrenchRot;
        //highlighter.highlighted = false ;
        if (isTighted)
        {
            Relax();
        }
        else
        {
            Tighten();
        }
    }

    /// <summary>
    /// 拧紧
    /// </summary>
    protected virtual void Tighten()
    {
        float time = 1f;
        DOTween.To(() => time,x => time = x,1,.5f).OnComplete(()=> {
            transform.DOLocalMove(initPos, rotTime).OnComplete(() => {
                isTighted = true;
                wrench.FinishRot();
                SetInterable(true);
                FinishEvent(true);
            });
            transform.DOLocalRotate(initRot, rotTime);
        });
    }

    /// <summary>
    /// 拧松
    /// </summary>
    protected virtual void Relax()
    {
        float time = 1f;
        DOTween.To(() => time, x => time = x, 1,.5f).OnComplete(() =>
        {
            transform.DOLocalMove(tarPos, rotTime).OnComplete(() =>
            {
                isTighted = false;
                wrench.FinishRot();
                FinishEvent(false);
            });
            transform.DOLocalRotate(tarRot, rotTime);
        });
    }
    /// <summary>
    /// 完成后事件
    /// </summary>
    protected virtual void FinishEvent(bool isTughtOrRelax)
    {
        
    }
    /// <summary>
    /// 放置在
    /// </summary>
    public void Place(NutBox nutBox)
    {
        //return
        //nb = nutBox;
        //inPlace = true;
    }
   

    /// <summary>
    /// 取下
    /// </summary>
    public void Remove()
    {
        //nb.inUse = false;
        //isPlace = false;
    }
}


