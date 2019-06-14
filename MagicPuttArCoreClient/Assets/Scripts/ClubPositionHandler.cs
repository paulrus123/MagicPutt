using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubPositionHandler : MonoBehaviour
{
    GolfClubPoseMessage golfClubPoseMessage;

    private void Start()
    {
        golfClubPoseMessage = new GolfClubPoseMessage();
        MqttClientHandler.OnClubPoseReceived += ReceiveMessage;
    }

    private void Update()
    {
        transform.localPosition = golfClubPoseMessage.position;
        transform.localEulerAngles = golfClubPoseMessage.eulerAngles;
    }

    void ReceiveMessage(string msg)
    {
        golfClubPoseMessage = JsonUtility.FromJson<GolfClubPoseMessage>(msg);
    }
}
