using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLCameraPoseHandler : MonoBehaviour
{
    MagicLeapCameraPoseMessage cameraPoseMessage;
    public GameObject origin;


    // Start is called before the first frame update
    void Start()
    {
        MqttClientHandler.OnCameraPoseReceived += ReceiveMessage;
    }

    private void Update()
    {
        if (cameraPoseMessage != null)
        {
            transform.localPosition = cameraPoseMessage.position;
            //transform.localEulerAngles = cameraPoseMessage.eulerAngles;
        }
    }

    void ReceiveMessage(string msg)
    {
        cameraPoseMessage = JsonUtility.FromJson<MagicLeapCameraPoseMessage>(msg);
    }
}
