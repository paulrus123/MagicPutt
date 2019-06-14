using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionHandler : MonoBehaviour
{
    GolfBallPositionMessage golfBallPositionMessage;

    private void Start()
    {
        golfBallPositionMessage = new GolfBallPositionMessage();
        MqttClientHandler.OnBallPositionRecieved += ReceiveMessage;
    }

    void ReceiveMessage(string msg)
    {
        golfBallPositionMessage = JsonUtility.FromJson<GolfBallPositionMessage>(msg);
    }

    private void Update()
    {
        transform.localPosition = golfBallPositionMessage.position;
    }
}
