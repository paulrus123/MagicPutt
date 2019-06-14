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