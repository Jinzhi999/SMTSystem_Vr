using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using System;

public class UDPServer : MonoBehaviour
{
    public string recvStr;
    static UdpClient udpClient;
    IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 50001);
    //初始化
    void Start()
    {
        udpClient = new UdpClient(50001);
        udpClient.BeginReceive(UdpRecived, null);
        t.text = ("开启udp 准备接收");
    }
    void UdpRecived(IAsyncResult result)
    {
        byte[] tmps = null;
        try
        {
            tmps = udpClient.EndReceive(result, ref iPEndPoint);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        udpClient.BeginReceive(UdpRecived, null);

        //处理
        if (tmps != null)
        {
            msg = (Encoding.UTF8.GetString(tmps));//Replace("\r\n", "")) 
            Debug.Log(msg);
        }
    }

    string msg = "";
    public Text t;
    private void Update()
    {
        if (string.IsNullOrEmpty(msg))
        {
            return;
        }
        t.text = "接收："+msg;
        msg = "";
    }

    public void quit()
    {
        Application.Quit();
    }
}