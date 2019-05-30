using UnityEngine;
using System;

public class ConnectionHandler : MonoBehaviour
{
    TcpClientHandler clientHandler;

    public string stringToEdit = "127.0.0.1";
    public string port = "4444";
    private TouchScreenKeyboard keyboard;

    private void Start()
    {
        clientHandler = GetComponent<TcpClientHandler>();
    }

    public Font font;
    GUIStyle style = new GUIStyle();

    // Opens native keyboard
    void OnGUI()
    {
        style.fontSize = 60;

        stringToEdit = GUI.TextField(new Rect(30, 20, 200*2, 30*2), stringToEdit, 30,style);
        port = GUI.TextField(new Rect(250*2, 20, 200*2, 30*2), port, 30, style);
    }

    public void TryConnect()
    {
        clientHandler.hostName = stringToEdit;
        clientHandler.port = Int32.Parse(port);
        if (clientHandler.isConnected())
            clientHandler.Disconnect();
        else
        {
            clientHandler.ConnectToServer();
            //gameObject.SetActive(false);
        }
    }


}