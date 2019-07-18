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

//Published by MagicLeap
[System.Serializable]
public class RampPose
{
    public int index; //index of the ramp (if multiple)
    public bool isPlaced; //if false then ramp is not on game area (e.g. in players possesion)
    public Vector3 position;
    public Vector3 eulerAngles;
}


//Published by phone
[System.Serializable]
public class RampRequest
{
    public int index = 0; //index of the ramp
    public bool requestPickup = false;
    public bool requestPlacement = false;
    public Vector3 position;
    public Vector3 eulerAngles;
}

[System.Serializable]
public class ScoresMessage
{
    public ScoresMessage()
    {
        pars = new int[numberOfHoles];
        scores = new int[numberOfHoles];
    }
    const int numberOfHoles = 9;
    public int currentHole = 1; //index of the current hole
    public int[] pars;
    public int[] scores; 
}
