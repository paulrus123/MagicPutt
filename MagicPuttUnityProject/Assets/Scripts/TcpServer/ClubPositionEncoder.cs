using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

public class ClubPositionEncoder : MonoBehaviour
{
    public GameObject golfClub;
    public GameObject golfCourse;

    public TcpServer _server;

    Vector3 clubPosition;
    Vector3 clubRotation;

    float currTime;
    readonly float timeOut = 0.1f;

    private void Start()
    {
        clubPosition = new Vector3();
        clubRotation = new Vector3();
        currTime = 0;
    }

    private void Update()
    {
        clubPosition = golfClub.transform.position - golfCourse.transform.position;
        clubRotation = golfClub.transform.eulerAngles - golfCourse.transform.eulerAngles;

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

        dataString =  clubPosition.x.ToString("#.0000") + ","
                    + clubPosition.y.ToString("#.0000") + ","
                    + clubPosition.z.ToString("#.0000") + ","
                    + clubRotation.x.ToString("#.0000") + ","
                    + clubRotation.y.ToString("#.0000") + ","
                    + clubRotation.z.ToString("#.0000") + ",";

        _server.Publish(dataString);
    }
}
