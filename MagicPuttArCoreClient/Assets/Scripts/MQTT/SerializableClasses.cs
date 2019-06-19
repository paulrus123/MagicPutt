using UnityEngine;

[System.Serializable]
public class GolfBallPositionMessage
{
    public Vector3 position;
}

[System.Serializable]
public class GolfClubPoseMessage
{
    public Vector3 position;
    public Vector3 eulerAngles;
}

[System.Serializable]
public class MagicLeapCameraPoseMessage
{
    public Vector3 position;
    public Vector3 eulerAngles;
}

[System.Serializable]
public class PhonePoseMessage
{
    public Vector3 position;
    public Vector3 eulerAngles;
}
