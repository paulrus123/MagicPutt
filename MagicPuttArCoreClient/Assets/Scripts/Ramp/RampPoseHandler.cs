using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampPoseHandler : MonoBehaviour
{
    RampPose rampPoseMsg;
    public int rampIndex;

    [SerializeField]
    GameObject rampObject = default;

    [SerializeField]
    GameObject rampObjectSelectable = default;

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
        rampObject.SetActive(rampPoseMsg.isPlaced);
        rampObject.transform.localPosition = rampPoseMsg.position;
        rampObject.transform.localEulerAngles = rampPoseMsg.eulerAngles;

        rampObjectSelectable.SetActive(cameraRaycast.RaycastHitObjectType == CameraRaycast.ObjectType.RAMP ? true : false);
    }
}
