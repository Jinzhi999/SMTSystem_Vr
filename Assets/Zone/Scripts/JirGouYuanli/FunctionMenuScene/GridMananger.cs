using UnityEngine;
using Valve.VR;

public class GridMananger : MonoBehaviour
{
    [Tooltip("确认、检查按钮")] public SteamVR_Action_Boolean ConfirmBtn;
    [Tooltip("显示提示信息按钮")] public SteamVR_Action_Boolean PromptsBtn;

    public GameObject panel;

    public virtual void Start()
    {
        ConfirmBtn.onStateDown += Test;
    }

    public void Test(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("按下"+fromSource.ToString());

        panel.gameObject.SetActive(!panel.gameObject.activeInHierarchy);
    }
}
