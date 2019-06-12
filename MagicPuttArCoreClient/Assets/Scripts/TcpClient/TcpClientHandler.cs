﻿using UnityEngine;
using System.Threading;
using System.Net.Sockets;
using System;
using System.Text;

public class TcpClientHandler : MonoBehaviour
{

    public string hostName = "localhost";
    public int port = 4444;

    private TcpClient m_client;
    private Thread clientThread;

    public bool isConnected() 
    { 
        if(m_client!=null )
        {
            if (m_client.Connected)
                return true;
        }
        return false;
    }

    public void ConnectToServer()
    {
        try
        {
            clientThread = new Thread(new ThreadStart(ReceiveData));
            clientThread.IsBackground = true;
            clientThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e);
        }
    }

    public void Disconnect()
    {
        try
        {
            m_client.Dispose();
            m_client.Close();
            m_client = null;
            clientThread.Abort();
            clientThread = null;

        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e);
        }
    }

    private void OnDestroy()
    {
        if(m_client.Connected)
        {
            Disconnect();
        }
    }

    private void ReceiveData()
    {
        try
        {
            Debug.Log("Trying to receive data");
            m_client = new TcpClient(hostName, port);
            Debug.Log("created a client");

            using (NetworkStream stream = m_client.GetStream())
            {
                while (true)
                {
                    Debug.Log("Listening");
                    if (DataDecoder.DecodeString(stream) == 0) 
                    {
                        Debug.Log("Received Data!");
                        break; 
                    }
                }
            } 
        }
        catch(SocketException e)
        {
            Debug.Log("Socket Exception: " + e);
        }
    }
}
