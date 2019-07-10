using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampPoseHandler : MonoBehaviour
{
    RampPose rampPoseMsg;
    public int rampIndex;

    [SerializeField]
    GameObject rampPlacement = default;

    [SerializeField]
    GameObject rampObject = default;

    [SerializeField]
    RingSelectionHandler ringSelection = default;

    [SerializeField]
    CameraRaycast cameraRaycast = default;

    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        rampPoseMsg = new RampPose();
        MqttClientHandler.OnRampPoseRecieved += ReceiveMessage;
    }

    void ReceiveMessage(string msg)
    {
        rampPoseMsg = JsonUtility.FromJson<RampPose>(msg);
    }

    private void Update()
    {
        rampPlacement.transform.localPosition = rampPoseMsg.position;
        rampPlacement.transform.localEulerAngles = rampPoseMsg.eulerAngles;

        if(cameraRaycast.RaycastHitObjectType == CameraRaycast.ObjectType.RAMP)
        {
            ringSelection.TurnOn();
        }
        else
        {
            ringSelection.TurnOff();
        }
    }
}
