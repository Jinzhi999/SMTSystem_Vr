using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// 打开阀芯
/// </summary>
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class Spool : DeviceBase
{
    /// <summary>
    /// 时间计时器
    /// </summary>
    public Text timer;
    protected override void OnHandHoverBegin(Hand hand)
    {
        //Debug.Log("手触碰");

        // 触摸按下
        transform.localPosition = new Vector3(transform.localPosition.x,
                                            transform.localPosition.y,
                                            0.0266f);

        if (isFinish)
            return;
        highlighter.highlighted = false;
        ////inUse = true;
        //Door();
        timer.gameObject.SetActive(true);
    }
    protected override void OnHandHoverEnd(Hand hand)
    {
        //Debug.Log("手触碰");
        // 结束触摸弹起
        transform.localPosition = new Vector3(transform.localPosition.x,
                                        transform.localPosition.y,
                                        0.03334689f);
    }
    protected override void HandHoverUpdate(Hand hand)
    {
        //Debug.Log("拿在手中");
        //highlighter.highlighted = true;
        //inUse = true;
        Operation();
    }
    /// <summary>
    /// 剩余操作时间
    /// </summary>
    public float residueTime = 3;
    /// <summary>
    /// 是否完成
    /// </summary>
    bool isFinish = false;
    public void Operation()
    {
        if (isFinish)
            return;
        residueTime -= Time.deltaTime;
        if (residueTime < 0f)
        {
            isFinish = true;
            timer.gameObject.SetActive(false);
            // 关闭 时间提示
        }
        else
        {
            timer.text = residueTime.ToString("F1");
        }
    }
}
