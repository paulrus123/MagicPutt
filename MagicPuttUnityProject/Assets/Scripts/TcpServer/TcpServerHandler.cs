using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TcpServerHandler : MonoBehaviour
{

    public string hostname = "localhost";
    public int port = 4444;

    TcpListener m_listener;
    Thread m_listenerThread;
    TcpClient m_connectedClient;


    // Start is called before the first frame update
    void Start()
    {
        m_listenerThread = new Thread(new ThreadStart(ListenForRequests));
        m_listenerThread.IsBackground = true;
        m_listenerThread.Start();
    }

    void ListenForRequests()
    {
        try
        {
            // Create listener on localhost port 8052.          
            m_listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            m_listener.Start();
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (m_connectedClient = m_listener.AcceptTcpClient())
                {
                    // Get a stream object for reading                  
                    using (NetworkStream stream = m_connectedClient.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary.                      
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message.                            
                            string clientMessage = Encoding.ASCII.GetString(incommingData);
                            Debug.Log("client message received as: " + clientMessage);
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }

    private void SendMessage()
    {
        if (m_connectedClient == null)
        {
            return;
        }

        try
        {
            // Get a stream object for writing.             
            NetworkStream stream = m_connectedClient.GetStream();
            if (stream.CanWrite)
            {
                GetComponent<ClubPositionEncoder>().Encode(stream);
                Debug.Log("Server sent his message - should be received by client");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SendMessage();
    }
}
