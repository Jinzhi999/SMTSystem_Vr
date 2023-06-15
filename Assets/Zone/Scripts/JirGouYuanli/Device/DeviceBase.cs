using UnityEngine;
using Valve.VR.InteractionSystem;
using HighlightPlus;
public enum DeviceState
{
    InHand,
    Release,
    Free
}
/// <summary>
/// 设备基类
/// </summary>
public class DeviceBase : MonoBehaviour
{
    protected HighlightEffect highlighter;
    /// <summary>
    /// 设备状态
    /// </summary>
    public DeviceState DeviceState;
    /// <summary>
    /// 是否可以拿
    /// </summary>
    public bool ableTake;
    /// <summary>
    /// 设备名称
    /// </summary>
    public string deviceName;
    /// <summary>
    /// 提示物体
    /// </summary>
    public GameObject tipsObj;
    public virtual void Start()
    {
        highlighter = this.GetComponent<HighlightEffect>();
    }
    protected virtual void OnHandHoverBegin(Hand hand)
    {
        //Debug.Log("开始触碰");
    }
    protected virtual void HandHoverUpdate(Hand hand)
    {
        //Debug.Log("持续触碰");
    }
    protected virtual void OnHandHoverEnd(Hand hand)
    {
        //Debug.Log("结束触碰");
    }
    protected virtual void OnAttachedToHand(Hand hand)
    {
        //Debug.Log("拿在手中");
        if (ableTake && DeviceState == DeviceState.Free)
        {
            DeviceState = DeviceState.InHand;
        }
    }
    protected virtual void HandAttachedUpdate(Hand hand)
    {
        //Debug.Log("持续拿在手中");
    }
    protected virtual void OnDetachedFromHand(Hand hand)
    {
        //Debug.Log("从手中离开");
        if (DeviceState == DeviceState.InHand)
        {
            DeviceState = DeviceState.Release;
            Invoke("ResetFree", 0.5f);
        }
    }
    /// <summary>
    /// 回归自由状态
    /// </summary>
    /// <param name="deviceOP"></param>
    public virtual void ResetFree()
    {
        if (DeviceState == DeviceState.Release)
            DeviceState = DeviceState.Free;
    }
    //protected virtual void OnTriggerEnter(Collider col)
    //{
    //}
    //protected virtual void OnTriggerStay(Collider col)
    //{
    //}
    //protected virtual void OnTriggerExit(Collider col)
    //{
    //}
    public virtual void ProcedureStart()
    {
        
    }
    public virtual void ProcedureEnd()
    {
        
    }

    protected virtual void SetInterable(bool  canMove)
    {
        transform.GetComponent<Interactable>().enabled = canMove;
    }
}
