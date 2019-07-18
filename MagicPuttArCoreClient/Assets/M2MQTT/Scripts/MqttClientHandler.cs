using System;
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

    public delegate void BallPositionRecieved(string msg);
    public static event BallPositionRecieved OnBallPositionRecieved;

    public delegate void ClubPoseReceived(string msg);
    public static event ClubPoseReceived OnClubPoseReceived;

    public delegate void CameraPoseReceived(string msg);
    public static event CameraPoseReceived OnCameraPoseReceived;

    public delegate void RampPoseRecieved(string msg);
    public static event RampPoseRecieved OnRampPoseRecieved;

    public delegate void ScoresRecieved(string msg);
    public static event ScoresRecieved OnScoresRecieved;

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

            //Bug in Library - cannot subscribe to multiple topics at once
            string[] topic1 = new string[] { "MagicPutt/GolfClubPose" };
            string[] topic2 = new string[] { "MagicPutt/GolfBallPosition" };
            string[] topic3 = new string[] { "MagicPutt/CameraPose" };
            string[] topic4 = new string[] { "MagicPutt/RampPose" };
            string[] topic5 = new string[] { "MagicPutt/Scores" };
            Subscribe(topic1);
            Subscribe(topic2);
            Subscribe(topic3);
            Subscribe(topic4);
            Subscribe(topic5);

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

    public void Subscribe(string[] topics)
    {
        byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE };
        client.Subscribe(topics, qosLevels);
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        switch (e.Topic)
        {
            case "MagicPutt/GolfClubPose":
                if (OnClubPoseReceived != null)
                    OnClubPoseReceived(System.Text.Encoding.UTF8.GetString(e.Message));
                break;
            case "MagicPutt/GolfBallPosition":
                if (OnBallPositionRecieved != null)
                    OnBallPositionRecieved(System.Text.Encoding.UTF8.GetString(e.Message));
                break;
            case "MagicPutt/CameraPose":
                if (OnCameraPoseReceived != null)
                    OnCameraPoseReceived(System.Text.Encoding.UTF8.GetString(e.Message));
                break;
            case "MagicPutt/RampPose":
                if (OnRampPoseRecieved != null)
                    OnRampPoseRecieved(System.Text.Encoding.UTF8.GetString(e.Message));
                break;
            case "MagicPutt/Scores":
                if (OnScoresRecieved != null)
                    OnScoresRecieved(System.Text.Encoding.UTF8.GetString(e.Message));
                break;
            default:
                break;
        }
    }
}