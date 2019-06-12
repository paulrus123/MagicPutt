using System;
using System.Collections; 
using System.Collections.Generic; 
using System.Net; 
using System.Net.Sockets; 
using System.Text; 
using System.Threading; 
using UnityEngine;
using UnityEngine.XR.MagicLeap;


public class TcpServer : MonoBehaviour {    
    #region private members     
    /// <summary>   
    /// TCPListener to listen for incomming TCP connection  
    /// requests.   
    /// </summary>  
    private TcpListener tcpListener; 
    /// <summary> 
    /// Background thread for TcpServer workload.   
    /// </summary>  
    private Thread tcpListenerThread;   
    /// <summary>   
    /// Create handle to connected tcp client.  
    /// </summary>  
    private TcpClient connectedTcpClient;
    #endregion

    string serverMessage;

    // Use this for initialization
    bool isAlive = false;

    public TextMesh tcpTextMesh;

    private void Start()
    {
        MLInput.OnControllerButtonDown += HandleBumperDown;
        StartServer();
    }

    void StartServer () {
        // Start TcpServer background thread        
        isAlive = true;
        tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests));       
        tcpListenerThread.IsBackground = true;      
        tcpListenerThread.Start();  
    }  
    
    /// <summary>   
    /// Runs in background TcpServerThread; Handles incomming TcpClient requests    
    /// </summary>  
    private void ListenForIncommingRequests () {        
        try {           
            // Create listener on localhost port 8052.          
            tcpListener = new TcpListener(IPAddress.Any, 4444);          
            tcpListener.Start();              
            Debug.Log("Server is listening");              
            Byte[] bytes = new Byte[1024];              
            while (isAlive) {              
                using (connectedTcpClient = tcpListener.AcceptTcpClient()) {                    
                    // Get a stream object for reading                  
                    using (NetworkStream stream = connectedTcpClient.GetStream()) {                         
                        int length;                         
                        // Read incomming stream into byte arrary.                      
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) {                           
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
        catch (SocketException socketException) {           
            Debug.Log("SocketException " + socketException.ToString());         
        }     
    }   


    public void Publish(string message)
    {
        serverMessage = message;
        SendMessage();
    }

    /// <summary>   
    /// Send message to client using socket connection.     
    /// </summary>  
    private void SendMessage() {        
        if (connectedTcpClient == null) {             
            return;         
        }       
        
        try {           
            // Get a stream object for writing.             
            NetworkStream stream = connectedTcpClient.GetStream();          
            if (stream.CanWrite) {                 
                // Convert string message to byte array.                 
                byte[] serverMessageAsByteArray = Encoding.UTF8.GetBytes(serverMessage);               
                // Write byte array to socketConnection stream.               
                stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);               
                Debug.Log("Server sent his message - should be received by client");           
            }       
        }       
        catch (SocketException socketException) {             
            Debug.Log("Socket exception: " + socketException);         
        }   
    }


    void HandleBumperDown(byte controller_id, MLInputControllerButton button)
    {
        if ((button == MLInputControllerButton.Bumper))
        {
            if(isAlive)
            {
                ResetServer();
            }
            else
            {
                StartServer();
            }
        }
    }

    public void ResetServer()
    {
        isAlive = false;
    }

    private void Update()
    {
        tcpTextMesh.text = isAlive ? "Connected" : "Disconnected";
    }
}