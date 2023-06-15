using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;

public class SteamVRLaserWrapper : MonoBehaviour
{
    private SteamVR_LaserPointer steamVrLaserPointer;

    private void Awake()
    {
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClickUp += OnPointerClickUp;
        steamVrLaserPointer.PointerClickDn += OnPointerClickDn;
    }


    private void OnPointerClickUp(object sender, PointerEventArgs e)
    {
      //  Debug.Log("Click"+e.target.name);
        VRUIButon vrUIBtn = e.target.GetComponent<VRUIButon>();
        if (vrUIBtn) vrUIBtn.OnPointClickUp();
        //IPointerClickHandler clickHandler = e.target.GetComponent<IPointerClickHandler>();
        //if (clickHandler == null)
        //{
        //    return;
        //}
        //clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    private void OnPointerClickDn(object sender, PointerEventArgs e)
    {
     //   Debug.Log("Click" + e.target.name);
        VRUIButon vrUIBtn = e.target.GetComponent<VRUIButon>();
        if (vrUIBtn) vrUIBtn.OnPointClickDn();
        //IPointerClickHandler clickHandler = e.target.GetComponent<IPointerClickHandler>();
        //if (clickHandler == null)
        //{
        //    return;
        //}
        //clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    private void OnPointerOut(object sender, PointerEventArgs e)
    {
    //    Debug.Log("In"+e.target.name);
        VRUIButon vrUIBtn = e.target.GetComponent<VRUIButon>();
        if (vrUIBtn) vrUIBtn.OnPointExit();
        //IPointerExitHandler pointerExitHandler = e.target.GetComponent<IPointerExitHandler>();
        //if (pointerExitHandler == null)
        //{
        //    return;
        //}
        //pointerExitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
    {
    //    Debug.Log("Out"+e.target.name);
        VRUIButon vrUIBtn = e.target.GetComponent<VRUIButon>();
        if (vrUIBtn) vrUIBtn.OnPointEnter();
        //IPointerEnterHandler pointerEnterHandler = e.target.GetComponent<IPointerEnterHandler>();
        //if (pointerEnterHandler == null)
        //{
        //    return;
        //}
        //pointerEnterHandler.OnPointerEnter(new PointerEventData(EventSystem.current));
    }

}