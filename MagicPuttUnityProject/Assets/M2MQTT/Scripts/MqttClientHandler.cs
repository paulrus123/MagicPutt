﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

//Reference: http://blog.jorand.io/2017/08/02/MQTT-on-Unity/
public class MqttClientHandler : MonoBehaviour
{
    //http://tdoc.info/blog/2014/11/10/mqtt_csharp.html
    private MqttClient client;

    private string clientId;
    // The connection information
    public string brokerHostname = "broker.hivemq.com";
    public int brokerPort = 8000;

    // Use this for initialization
    void Start()
    {
        if (brokerHostname != null)
        {
            Debug.Log("connecting to " + brokerHostname + ":" + brokerPort);

            client = new MqttClient(brokerHostname, brokerPort, false, MqttSslProtocols.TLSv1_0, null, null);
            clientId = Guid.NewGuid().ToString();

            Connect();
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        }
    }

    private void Connect()
    {
        Debug.Log("about to connect on '" + brokerHostname + "'");

        if (client == null)
        {
            Debug.Log("MQTT Client Null - Unable to publish");
            return;
        }

        if (client.IsConnected)
        {
            Debug.Log("Already connected");
            return;
        }

        try
        {
            client.Connect(clientId);
        }
        catch (Exception e)
        {
            Debug.LogError("Connection error: " + e);
        }
    }

    public void Publish(string _topic, string msg)
    {
        if (client == null)
        {
            Debug.Log("MQTT Client Null - Unable to publish");
            return;
        }

        if (!client.IsConnected)
        {
            Debug.Log("MQTT Client not connected - Unable to publish");
            return;
        }

        client.Publish(
            _topic, Encoding.UTF8.GetBytes(msg),
            MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
    }

    public void Subscribe(string topic)
    {
        byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE };
        client.Subscribe(new string[] { topic }, qosLevels);
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string msg = System.Text.Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received message from " + e.Topic + " : " + msg);
    }
}