using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class VRRaycast : MonoBehaviour
{
    public static VRRaycast instance;
    public static Player Player;
    public static Transform PlayerHand;
    public static SteamVR_LaserPointer LaserPointer;
    public static SteamVR_Action_Boolean GrabGripBtn;
    public static SteamVR_Action_Boolean GrabPinchBtn;
    /// <summary>
    /// 是否打开射线
    /// </summary>
    public static bool Grip;
    public static GameObject cube;
    //public static Action GripBtnEvent;
    /// <summary>
    /// 按键"右手侧键"按下事件
    /// 返回按下事件是否为打开状态
    /// </summary>
    public static Action<bool> GripBtnEvent;
    /// <summary>
    /// 按键"右手扳机"抬起事件
    /// 返回射线信息
    /// </summary>
    public static Action<RaycastHit> GrabPinchUpEvent;
    /// <summary>
    /// 按键"右手扳机"按下事件
    /// 返回射线信息
    /// </summary>
    public static Action<RaycastHit> GrabPinchDnEvent;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindObjectOfType<Player>();
        PlayerHand = Player.hands[1].transform;
        LaserPointer = PlayerHand.GetComponent<SteamVR_LaserPointer>();
        GrabGripBtn = SteamVR_Actions.default_GrabGrip;
        GrabPinchBtn = SteamVR_Actions.default_InteractUI;
    }

    // Update is called once per frame
    void Update()
    {
        ShowLaser();
    }

    public static void ShowLaser(Action<bool> callback = null)
    {
        if (GrabGripBtn.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            GripBtnEvent?.Invoke(StartLaser());
        }
        //if (Grip)
        //{
        //    if (GrabPinchBtn.GetStateDown(SteamVR_Input_Sources.RightHand))
        //    {
        //        GrabPinchDnEvent?.Invoke(RayastHit1());
        //    }
        //    if (GrabPinchBtn.GetStateUp(SteamVR_Input_Sources.RightHand))
        //    {
        //        GrabPinchUpEvent?.Invoke(RayastHit1());
        //    }
        //}
    }

    public static void ClearAllAction()
    {
        Delegate[] gpe = GrabPinchUpEvent.GetInvocationList();
        for (int i = 0; i < gpe.Length; i++)
        {
            GrabPinchUpEvent -= gpe[i] as Action<RaycastHit>;
        }
        Delegate[] gpde = GrabPinchDnEvent.GetInvocationList();
        for (int i = 0; i < gpde.Length; i++)
        {
            GrabPinchDnEvent -= gpde[i] as Action<RaycastHit>;
        }
        //ClearActions.Clear(GrabPinchUpEvent);
        //ClearActions.Clear(GrabPinchDnEvent);
        //ClearActions.Clear(GripBtnEvent);
        Delegate[] gbe = GripBtnEvent.GetInvocationList();
        for (int i = 0; i < gbe.Length; i++)
        {
            GripBtnEvent -= gbe[i] as Action<bool>;
        }
    }

    public static bool StartLaser()
    {
        Grip = !Grip;
        LaserPointer.enabled = Grip;
        LaserObj(Grip);
        return Grip;
    }


    public static void ShowLaserByOther()
    {
        Grip = true;
        LaserPointer.enabled = Grip;
        LaserObj(Grip);
    }

    /// <summary>
    /// 非正常按键关闭射线
    /// </summary>
    public static void RecoveryLaser()
    {
        Grip = false;
        LaserObj(false);
    }

    public static void LaserObj(bool active)
    {
        try
        {
            cube = PlayerHand.transform.Find("New Game Object/Cube").gameObject;
            cube.SetActive(active);
        }
        catch (System.Exception)
        {
            //Debug.LogError(ex.Message);
        }
    }

    //public static RaycastHit RayastHit1()
    //{
    //    return null;
    //   // return LaserPointer.raycastHit;
    //}
}

public class ClearActions
{
    public static void Clear(Action action)
    {
        Delegate[] dele = action.GetInvocationList();
        for (int i = 0; i < dele.Length; i++)
        {
            action -= dele[i] as Action;
        }
    }
    public static void Clear<T>(Action<T> action)
    {
        Delegate[] dele = action.GetInvocationList();
        for (int i = 0; i < dele.Length; i++)
        {
            action -= dele[i] as Action<T>;
        }
    }
}
