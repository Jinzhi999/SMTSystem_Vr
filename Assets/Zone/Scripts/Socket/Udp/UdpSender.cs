using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

public class UdpSender: MonoBehaviour
{
    string UDPClientIP = "10.145.139.219";
    Socket socket;
    EndPoint serverEnd;
    IPEndPoint ipEnd;
    byte[] sendData = new byte[1024];
    Thread connectThread;

    void Start()
    {

        using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/Configs/ServerIp.txt"))
        {
            UDPClientIP = sr.ReadLine();
        }
        InitSocket();
    }

    void InitSocket()
    {
        ipEnd = new IPEndPoint(IPAddress.Parse(UDPClientIP), 50001);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        serverEnd = (EndPoint)sender;
        print("等待连接");
        //SocketSend(str);
    }

    public void SocketSend(string sendStr)
    {
        //清空
        sendData = new byte[1024];
        //数据转换
        sendData = Encoding.UTF8.GetBytes(sendStr);
        //发送给指定服务端
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
        Debug.Log(sendStr);
    }

    public string sendMsg = "111";
    [ContextMenu("发送")]

    public void Send()
    {
        SocketSend(sendMsg);
    }
    void SocketQuit()
    {
        //关闭线程
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最后关闭socket
        if (socket != null)
            socket.Close();
    }
    void OnApplicationQuit()
    {
        SocketQuit();
    }
}