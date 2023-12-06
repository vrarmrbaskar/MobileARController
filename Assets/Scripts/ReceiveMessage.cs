using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ReceiveMessage : MonoBehaviour
{
    const int PORT_NUM = 1998;

    UdpClient receiver;
    public string currMessage = String.Empty;

    private void Awake()
    {
        //Get the gameobject 
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("---- IN Start ----");
        receiver = new UdpClient(PORT_NUM);
        Debug.Log("----- udpClient address:" + receiver.Available.ToString());
        receiver.BeginReceive(DataReceived, receiver);
        Debug.Log("---- Out Start ----");
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("---- IN Update ----");
        StartCoroutine("MessageFromController");
        //Debug.Log("---- Out Update ----");
    }

    private IEnumerator MessageFromController()
    {
      //  Debug.Log("----- In MessageFromController ----");
      //  Debug.Log("--- CurrMessage:" + currMessage);
        if (currMessage.Length > 0)
        {
            try
            {
                Debug.Log("Message received: " + currMessage);
                var msg = currMessage;
                    Debug.Log("---- msg[0]" + msg.ToLower());
                    //Below switch statement is for futuristic purpose.
                    // Before processing the received message ,adding extra operation for 
                    // ROTATE or SCALE actions 
                    switch (msg.ToLower())
                    {
                        case "ROTATE":
                        
                            break;
                        case "SCALE": 
                            break;
                    }
                //}
            }
            catch (Exception e)
            {
                Debug.Log("Error while playing video--->" + e);
            }
            finally
            {
                currMessage = string.Empty;

            }
        //    Debug.Log("----- Out MessageFromController ----");
            yield return currMessage;
        }
    }


    private void OnApplicationQuit()
    {
        receiver.Close();
        receiver.Dispose();
    }


    // This is called whenever data is received
    private void DataReceived(IAsyncResult ar)
    {
        Debug.Log("---- In DataReceived ----");

        UdpClient c = (UdpClient)ar.AsyncState;
        IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        Debug.Log("---- IPEndPoint: Address"+ receivedIpEndPoint.Address.ToString());
        Debug.Log("---- IPEndPoint: Port" + receivedIpEndPoint.Port.ToString());
        Byte[] receivedBytes = c.EndReceive(ar, ref receivedIpEndPoint);

        //string packet = System.Text.Encoding.UTF8.GetString (receivedBytes, 0, 20);
        currMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);
        Debug.Log("---- currMessage:" + currMessage);

        // Restart listening for udp data packages
        c.BeginReceive(DataReceived, ar.AsyncState);
    }
}
