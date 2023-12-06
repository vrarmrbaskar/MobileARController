using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{

    Thread receiveThread;
    UdpClient client;
    //  public int port = 5053;
    const int port = 5053;
    public bool startRecieving = true;
    public bool printToConsole = true;
    public string data;


    public void Start()
    {
        Debug.Log("---- In Start ----");
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        Debug.Log("---- Out Start ----");
    }

    public void Update()
    {
      ///  Debug.Log("---- Out update ----");
    }
    // receive thread
    private void ReceiveData()
    {
        Debug.Log("----- ReceiveData -----");
        if (client == null)
        {
            Debug.Log("---- Client is NULL ----");
            client = new UdpClient(port);
        }
        while (startRecieving)
        {
            Debug.Log("----- While loop -----");
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any,0);
                byte[] dataByte = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);
                Debug.Log("----- data:" + data);
                if (printToConsole) 
                { print(data); }
            }
            catch (Exception err)
            {
                Debug.Log("---- Error:" + err.ToString());
                print(err.ToString());
            }
        }
        Debug.Log("----- Out  ReceiveData -----");
    }

}
