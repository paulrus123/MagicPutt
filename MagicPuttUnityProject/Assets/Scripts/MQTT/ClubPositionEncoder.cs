using UnityEngine;

public class ClubPositionEncoder : MonoBehaviour
{
    public GameObject golfClub;
    public GameObject golfCourse;
    public GameObject golfBall;

    Vector3 clubPosition;
    Vector3 clubRotation;
    Vector3 ballPosition;

    public MqttClientHandler mqttClientHandler;

    GolfClubPoseMessage golfClubPoseMessage;
    GolfBallPositionMessage golfBallPositionMessage;

    float currTime;
    readonly float timeOut = 0.03f;

    private void Start()
    {
        clubPosition = new Vector3();
        clubRotation = new Vector3();
        golfClubPoseMessage = new GolfClubPoseMessage();
        golfBallPositionMessage = new GolfBallPositionMessage();

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
        golfBallPositionMessage.position = golfBall.transform.localPosition;
        golfClubPoseMessage.position = golfClub.transform.localPosition;
        golfClubPoseMessage.eulerAngles = golfClub.transform.localEulerAngles;

        //Convert to JSON string
        string golfBallPositionJson = JsonUtility.ToJson(golfBallPositionMessage);
        string golfClubPoseJson = JsonUtility.ToJson(golfClubPoseMessage);

        mqttClientHandler.Publish("MagicPutt/GolfClubPose", golfClubPoseJson);
        mqttClientHandler.Publish("MagicPutt/GolfBallPosition", golfBallPositionJson);
    }
}
