using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class VRUIButon : MonoBehaviour
{
    public Image image;
    public Color originColor;
    public Sprite OrigionSprite;
    public Sprite MouseInSprite;
    public UnityEvent OnClickDn = null;
    public UnityEvent OnClickUp = null;

    public UnityEvent OnPointEnterEvent = null;
    public UnityEvent OnPointExitEvent = null;

    public string btnName;
    // Start is called before the first frame update
   // public SteamVR_LaserPointer sv;
    public virtual void Start()
    {
        image = GetComponent<Image>();
        if (image)
        {
            originColor = image.color;
            OrigionSprite = image.sprite;
        }
        //sv = Player.FindObjectOfType<SteamVR_LaserPointer>();
        //if (sv == null)
        //{
        //    sv = GameObject.FindObjectOfType<SteamVR_LaserPointer>();
        //}
        //sv.PointerClickDn += OnPointClick;
    }

    public virtual void OnPointEnter()
    {
        //Debug.Log("enter");
        OnPointEnterEvent?.Invoke();
        originColor.a = 0.7f;
        if (image)
        {
            if (MouseInSprite) image.sprite = MouseInSprite;
            image.color = originColor;
        }
     
    }

    public virtual void OnPointExit()
    {
        Debug.Log("exit");
        OnPointExitEvent?.Invoke();
        originColor.a = 1f;
        if (image)
        {
            if (OrigionSprite) image.sprite = OrigionSprite;
            image.color = originColor;
        }
    }

    public virtual void OnPointClickUp()
    {
        Debug.Log("up");
        OnClickUp?.Invoke();
    }
    //public virtual void OnPointClick(object a, PointerEventArgs e)
    //{
    //    if (e.target == transform)
    //    {
    //      //  Debug.Log("click " +e.target.name);
    //        //OnPointClickDn();
    //    }
    //}
    public virtual void OnPointClickDn()
    {
        Debug.Log("down tar:"+transform.name);
        OnClickDn?.Invoke();
    }
}
