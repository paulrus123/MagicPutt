using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

public class ClubPositionEncoder : MonoBehaviour
{
    public GameObject golfClub;
    public GameObject golfCourse;

    Vector3 clubPosition;
    Vector3 clubRotation;

    private void Start()
    {
        clubPosition = new Vector3();
        clubRotation = new Vector3();
    }

    private void FixedUpdate()
    {
        clubPosition = golfClub.transform.position - golfCourse.transform.position;
        clubRotation = golfClub.transform.eulerAngles - golfCourse.transform.eulerAngles;
    }

    static IFormatter formatter = new BinaryFormatter();

    public void Encode(NetworkStream stream)
    {
        string dataString = "";

        dataString =  clubPosition.x.ToString("#.0000") + ","
                    + clubPosition.y.ToString("#.0000") + ","
                    + clubPosition.z.ToString("#.0000") + ","
                    + clubRotation.x.ToString("#.0000") + ","
                    + clubRotation.y.ToString("#.0000") + ","
                    + clubRotation.z.ToString("#.0000");


        var bytes = System.Text.Encoding.UTF8.GetBytes(dataString);

        formatter.Serialize(stream, bytes);
    }
}
