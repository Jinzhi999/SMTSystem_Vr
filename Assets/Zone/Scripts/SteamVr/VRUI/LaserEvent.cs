using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;
using UnityEngine.EventSystems;

public class LaserEvent : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public SteamVR_LaserPointer laserPointer;

    public UnityEvent mOnEnter = null;
    public UnityEvent mOnClick = null;
    public UnityEvent mOnUp = null;

    public Image targetImage;
    // Start is called before the first frame update
    void Start()
    {
        targetImage = GetComponent<Image>();
        mOnEnter.AddListener(OnButtonEnter);
        mOnClick.AddListener(OnButtonClick);
        mOnUp.AddListener(OnButtonUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {

        laserPointer.PointerClickUp += SteamVrLaserPointer_PointerClick;
        laserPointer.PointerIn += SteamVrLaserPointer_PointerIn;
        laserPointer.PointerOut += SteamVrLaserPointer_PointerOut;
        
    }
    void OnDestroy()
    {

        laserPointer.PointerClickUp -= SteamVrLaserPointer_PointerClick;
        laserPointer.PointerIn -= SteamVrLaserPointer_PointerIn;
        laserPointer.PointerOut -= SteamVrLaserPointer_PointerOut;
        
    }

    private void SteamVrLaserPointer_PointerOut(object sender, PointerEventArgs e)
    {
        try
        {
            if (e.target.gameObject == this.gameObject)
            {
                if (mOnUp != null) mOnUp.Invoke();
            }
        }
        catch { }
    }
    private void SteamVrLaserPointer_PointerIn(object sender, PointerEventArgs e)
    {
        try
        {
            if (e.target.gameObject == this.gameObject)
            {
                if (mOnEnter != null) mOnEnter.Invoke();
            }
        }
        catch { }
    }

    private void SteamVrLaserPointer_PointerClick(object sender, PointerEventArgs e)
    {
        try
        {
            if (e.target.gameObject == this.gameObject)
            {
                if (mOnClick != null) mOnClick.Invoke();
            }
        }
        catch
        { }
    }

    public void OnButtonClick()
    {
        Debug.Log("OnButtonClick");
    }
    /// <summary>
    /// 经过
    /// </summary>
    public void OnButtonEnter()
    {
        Debug.Log("OnButtonEnter");
    }
    /// <summary>
    /// 抬起
    /// </summary>
    public void OnButtonUp()
    {
        Debug.Log("OnButtonUp");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("1111");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("222");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("3333");
    }

}
