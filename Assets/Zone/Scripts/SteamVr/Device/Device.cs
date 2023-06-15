using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System.Collections;
/// <summary>
/// 设备基类
/// </summary>
public class Device : MonoBehaviour
{
    public Device lastDev;
    public Device newtDev;

    
    protected virtual void OnHandHoverBegin(Hand hand)
    {

    }
    /// <summary>
    /// 当手持续悬浮
    /// </summary>
    /// <param name="hand"></param>
    protected virtual void HandHoverUpdate(Hand hand)
    {

    }
    /// <summary>
    /// 当手悬浮结束
    /// </summary>
    /// <param name="hand"></param>
    protected virtual void OnHandHoverEnd(Hand hand)
    {
    }
    /// <summary>
    /// 当被添加到手
    /// </summary>
    /// <param name="hand"></param>
    protected virtual void OnAttachedToHand(Hand hand)
    {
    }
    /// <summary>
    /// 当持续添加到手
    /// </summary>
    /// <param name="hand"></param>
    protected virtual void HandAttachedUpdate(Hand hand)
    {
    }
    /// <summary>
    /// 当从手中分离
    /// </summary>
    /// <param name="hand"></param>
    protected virtual void OnDetachedFromHand(Hand hand)
    {
        OnStartNextPro();
        StartCoroutine(Delay());
    }
    public  IEnumerator Delay()
    {
        yield return 1;
        transform.SetParent(pare);
    }
    Transform pare;
    public virtual void Start()
    {
        //ConfirmBtn = SteamVR_Actions.default_InteractUI;
        //devList.Add(this);
        //if (this.proNo == 1)
        //{
        //    currentDev = this;
        //    OnStartPro();
        //}
        pare = transform.parent;
    }
    public virtual void OnInitPro()
    {
        // 初始化 解禁
        transform.GetComponent<Collider>().enabled = true;
        highlight(true);
    }
    public virtual void OnLockPro()
    {
        // 锁定
        transform.GetComponent<Collider>().enabled = false;

    }
    public virtual void OnStartNextPro()
    {
        if (DeviceTest.isPos)
        {
            if (newtDev != null)
            {
                newtDev.OnInitPro();
      
            }
            else
            { 
            }
        }
        else
        {
            if (lastDev != null)
            {
                lastDev.OnInitPro();
            }
            else
            {
                
            }
        }
        highlight(false);
    }
    //public virtual void OnFinishPro()
    //{

    //}
    //public virtual void OnEndPro()
    //{

    //}

   public void highlight(bool open)
    {
        HighlightPlus.HighlightEffect high = transform.GetComponent<HighlightPlus.HighlightEffect>();
        high.highlighted = open;
    }
}
