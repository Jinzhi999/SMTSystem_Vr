using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class GridMananger : MonoBehaviour
{
    [Tooltip("确认、检查按钮")] public SteamVR_Action_Boolean ConfirmBtn;
    [Tooltip("显示提示信息按钮")] public SteamVR_Action_Boolean PromptsBtn;
    public SteamVR_LaserPointer steamVR_Laser;
    public GameObject panel;

    public virtual void Start()
    {
        ConfirmBtn.onStateDown += Test;
    }

    public void Test(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("按下"+fromAction.activeDevice);
        if (fromAction.activeDevice.ToString().Equals("LeftHand"))
        {
        panel.gameObject.SetActive(!panel.gameObject.activeInHierarchy);

        }
        else if (fromAction.activeDevice.ToString().Equals("RightHand"))
        {
            steamVR_Laser.SetLinerActive();
        }
    }
}
