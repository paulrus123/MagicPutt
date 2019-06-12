using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ClubPositionEncoder : MonoBehaviour
{
    public GameObject golfClub;
    public GameObject golfCourse;
    public GameObject golfBall;
    
    public TcpServer _server;

    Vector3 clubPosition;
    Vector3 clubRotation;
    Vector3 ballPosition;

    float currTime;
    readonly float timeOut = 0.03f;

    private void Start()
    {
        clubPosition = new Vector3();
        clubRotation = new Vector3();
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
        string dataString = "";

        clubPosition = golfClub.transform.localPosition;
        clubRotation = golfClub.transform.localEulerAngles;
        ballPosition = golfBall.transform.localPosition;


        dataString =  clubPosition.x.ToString("#.0000") + ","
                    + clubPosition.y.ToString("#.0000") + ","
                    + clubPosition.z.ToString("#.0000") + ","
                    + clubRotation.x.ToString("#.0000") + ","
                    + clubRotation.y.ToString("#.0000") + ","
                    + clubRotation.z.ToString("#.0000") + ","
                    + ballPosition.x.ToString("#.0000") + ","
                    + ballPosition.y.ToString("#.0000") + ","
                    + ballPosition.z.ToString("#.0000") + ",";

        _server.Publish(dataString);
    }
}
