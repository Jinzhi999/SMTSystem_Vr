using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;


public class TcpReciver : MonoBehaviour
{
    //string TCPClientIP = "172.16.1.104";
    Socket socket;
    EndPoint serverEnd;
    IPEndPoint ipEnd;
    byte[] sendData = new byte[1024];
    Thread connectThread;

    public Image image;
    public static TcpReciver instance;
    private void Awake()
    {
        instance = this;
    }
    public void InitSocket(string ip)
    {
        ipEnd = new IPEndPoint(IPAddress.Parse(ip), 50001);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        serverEnd = (EndPoint)sender;
        print("等待连接");
        socket.ReceiveBufferSize = 1024 * 1024;
        try
        {
            socket.Connect(ipEnd);
            //XUIPanel.ShowPanel<TipPanel>("服务器连接成功");

            Thread thread = new Thread(Received);
            thread.IsBackground = true;
            thread.Start();
        }
        catch (Exception)
        {
            //XUIPanel.ShowPanel<TipPanel>("请检查网络设置重试");
        }
    }
    public void InitState()
    {
        recImgFlag = false;
        finishFlag = true;
    }
    public string sendMsg = "111";
    string recMsg = "";

    [ContextMenu("Tcp发送")]
    void TcpSendMessage()
    {
        string msg = sendMsg;
        byte[] buffer = new byte[1024 * 6];
        buffer = Encoding.UTF8.GetBytes(msg);
        socket.Send(buffer);
        Debug.Log("发送的数据为：" + msg);

        // cmd:play_name
        // cmd:play
        // cmd:pause
        // cmd:getList

        // send:startSend_class_name
        // send:finishSend_class_name
    }
    public void TcpSendMessage(string msg)
    {
        byte[] buffer = new byte[1024 * 6];
        buffer = Encoding.UTF8.GetBytes(msg);
        socket.Send(buffer);
        Debug.Log("发送的数据为：" + msg);
     //   XUIPanel.ShowPanel<TipPanel>("send:"+msg);
    }
    //
    bool recImgFlag = false;
    bool finishFlag = false;
    int recImgLength = 0;
    byte[] recImgBuffer;
    int currentLength = 0;
    private void Received()
    {
        try
        {
            while (true)
            {
                byte[] buffer = new byte[1024 * 6];
                int len = socket.Receive(buffer);
                if (len == 0)
                {
                    Debug.Log("");
                    break;
                }
                if (recImgFlag)
                {
                    byte[] tb = new byte[len];
                 
                    for (int i = 0; i < len ; i++)
                    {
                        tb[i] = buffer[i];
                    }
                
                    tb.CopyTo(recImgBuffer, currentLength);
                    currentLength += len;
                    //Debug.Log("接受长度:" + tb.Length+"   当前长度："+currentLength);
                    if (recImgLength == currentLength)
                    {
                        Debug.Log("完成复制");
                        recImgFlag = false;
                        finishFlag = true;
                    }
                }
                else
                {
                    recMsg = Encoding.UTF8.GetString(buffer, 0, len);
                    Debug.Log("接收到服务端的数据 ： " + recMsg);
             
                }
            }
        }
        catch { }
    }

    DictMsg dm=new DictMsg();
    private void Update()
    {
        if (finishFlag==true)
        {
            finishFlag = false;
            StartCoroutine(DelayPic());
        }
        if (string.IsNullOrEmpty(recMsg))
        {
            return;
        }
        Debug.Log("接收到服务端的数据 ： " + recMsg);
      //  XUIPanel.ShowPanel<TipPanel>("recive:" + recMsg);
        if (recMsg.Contains("send:"))
        {
            string[] strs = recMsg.Split(':');
            string cmd = strs[1];
        
            if (cmd.Equals("prepare"))
            {
                string msg = recMsg.Substring(13, recMsg.Length-13);
              
                dm = JsonConvert.DeserializeObject<DictMsg>(msg);
                recImgLength = dm.picLength;
                recImgBuffer = new byte[recImgLength];
                recImgFlag = true;
                currentLength = 0;
                //Debug.Log("接收总长度：" + recImgLength);
                TcpSendMessage("send$:prepare$:"+dm.currentSendPic);
            }
            else if (cmd.Equals("finish"))
            {
            }
        }
    
        else if (recMsg.Contains("cmd:"))
        {
        }
        else if (recMsg.Contains("wrong:"))
        {
            string cmd = recMsg.Split(':')[1];
            if (cmd.Equals("nofile"))
            {
                Debug.Log(" 没有文件");
            }
        }
        recMsg = "";
    }
    IEnumerator DelayPic()
    {
        string path = GameFlowManager.mediaPath + "/媒体文件/"+dm.picClass;
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += "/" + dm.picName;
        FileStream fs = File.Open(path, FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.ReadWrite);
        fs.Write(recImgBuffer, 0, recImgBuffer.Length);
        fs.Flush();
        fs.Close();
        yield return new WaitForSeconds(1f);

        //FileStream files = new FileStream(path, FileMode.Open);
        //byte[] imgByte = new byte[files.Length];
        //files.Read(imgByte, 0, imgByte.Length);
        //files.Close();

        TcpSendMessage("send$:finish$:" + dm.currentSendPic);
     //   RefreshPanel.instace.SetData(dm.currentSendPic + 1, dm.totleSendPic);
        //Texture2D tex = new Texture2D(989, 606);
        //tex.LoadImage(imgByte);
        //Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        //image.sprite = sprite;
    }

    public class DictMsg
    {
        public int picLength;
        public int totleSendPic;    // 总数量
        public int currentSendPic;  // 从0开始
        public string picClass;
        public string picName;
    }
}