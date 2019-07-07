using UnityEngine;

public class ClubPositionEncoder : MonoBehaviour
{
    public GameObject trackedImage;
    public GameObject golfClub;
    public GameObject golfCourse;
    public GameObject golfBall;
    public GameObject magicLeapCamera;
    public RampHandler rampHandler;
    
    public MqttClientHandler mqttClientHandler;

    GolfClubPoseMessage golfClubPoseMessage;
    GolfBallPositionMessage golfBallPositionMessage;
    MagicLeapCameraPoseMessage cameraPoseMessage;
    RampPose rampPoseMsg;

    float currTime;
    readonly float timeOut = 0.03f;

    private void Start()
    {
        rampPoseMsg = new RampPose();
        golfClubPoseMessage = new GolfClubPoseMessage();
        golfBallPositionMessage = new GolfBallPositionMessage();
        cameraPoseMessage = new MagicLeapCameraPoseMessage();

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

        cameraPoseMessage.position = magicLeapCamera.transform.localPosition;
        cameraPoseMessage.eulerAngles = magicLeapCamera.transform.localEulerAngles;

        rampPoseMsg.index = rampHandler.index;
        rampPoseMsg.isPlaced = rampHandler.isPlaced;
        rampPoseMsg.position = rampHandler.position;
        rampPoseMsg.eulerAngles = rampHandler.eulerAngles;

        //Convert to JSON string
        string golfBallPositionJson = JsonUtility.ToJson(golfBallPositionMessage);
        string golfClubPoseJson = JsonUtility.ToJson(golfClubPoseMessage);
        string cameraPositionJson = JsonUtility.ToJson(cameraPoseMessage);
        string rampPoseJson = JsonUtility.ToJson(rampPoseMsg);

        mqttClientHandler.Publish("MagicPutt/GolfClubPose", golfClubPoseJson);
        mqttClientHandler.Publish("MagicPutt/GolfBallPosition", golfBallPositionJson);
        mqttClientHandler.Publish("MagicPutt/CameraPose", cameraPositionJson);
        mqttClientHandler.Publish("MagicPutt/RampPose", rampPoseJson);
    }
}
