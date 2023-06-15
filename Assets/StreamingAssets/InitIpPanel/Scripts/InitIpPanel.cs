using UnityEngine;
using UnityEngine.UI;
using XFrame.Core.UI;
using XFrame.Core.Tool;
public class InitIpPanel : XUIPanel
{
    public Button loginBtn, quitBtn;
    public InputField ipAddressInput, playerNameInput;
    public InitIpPanel() : base(UIType.PopUp, UIMode.None, UICollider.None)
    {
        uiPath = "UI/InitIpPanel/Prefabs/InitIpPanel";
    }

    public override void Awake(GameObject go)
    {
        AutoAssign.InitObject(this, transform);
        loginBtn.onClick.AddListener(()=> {
            string ip = ipAddressInput.text;
            System.Net.IPAddress address;
            if (System.Net.IPAddress.TryParse(ip, out address))
            {
                PlayerPrefs.SetString("ipConfig", ip);
                XUIPanel.ShowPanel<TipPanel>("主机ip地址设置成功，请重新启动");
                Hide();
                Application.Quit();
            }
            else
            {
                XUIPanel.ShowPanel<TipPanel>("主机ip地址不合法");
            }
        });
        quitBtn.onClick.AddListener(() => {
            Application.Quit();
        });
    }
    public override void Active()
    {
        base.Active();
        if (PlayerPrefs.HasKey("ipConfig"))
        {
            ipAddressInput.text = PlayerPrefs.GetString("ipConfig");
        }
        else
        {
            ipAddressInput.text ="";
        }

        if (PlayerPrefs.HasKey("playerName"))
        {
            playerNameInput.text = PlayerPrefs.GetString("playerName");
        }
        else
        {
            playerNameInput.text = "";
        }
    }
}
