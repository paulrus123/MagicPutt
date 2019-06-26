﻿using UnityEngine;

public class MQTTEncoder : MonoBehaviour
{
    public GameObject phone;
    
    public MqttClientHandler mqttClientHandler;

    PhonePoseMessage phonePoseMessage;

    float currTime;
    readonly float timeOut = 0.03f;

    private void Start()
    {
        phonePoseMessage = new PhonePoseMessage();
        currTime = 0;
    }

    private void Update()
    {

        currTime += Time.deltaTime;
        if(currTime >timeOut)
        {
            currTime = 0;
            Send();
        }
    }

    public void Send()
    {
        //Populate messages
        phonePoseMessage.position = phone.transform.localPosition;
        phonePoseMessage.eulerAngles = phone.transform.localEulerAngles;

        //Convert to JSON string
        string phonePoseJson = JsonUtility.ToJson(phonePoseMessage);

        mqttClientHandler.Publish("MagicPutt/PhonePose", phonePoseJson);
    }
}