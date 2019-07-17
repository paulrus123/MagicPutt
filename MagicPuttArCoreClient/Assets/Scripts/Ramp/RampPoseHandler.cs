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
    RingSelectionHandler ringSelection = default;

    [SerializeField]
    CameraRaycast cameraRaycast = default;

    [SerializeField]
    InventorySelectionHandler inventorySelectionHandler = default;

<<<<<<< HEAD
=======
    [SerializeField]
    UIHandlerMain uIHandlerMain = default;

>>>>>>> Bugfixes: selectable icon only appear on hand, ball renders above ground plane, change size of occlusion area
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

<<<<<<< HEAD
        if((cameraRaycast.RaycastHitObjectType == CameraRaycast.ObjectType.RAMP) && (inventorySelectionHandler.currentInventoryType == InventorySelectionHandler.InventoryTypes.HAND))
=======
        if((cameraRaycast.RaycastHitObjectType == CameraRaycast.ObjectType.RAMP) 
            && (inventorySelectionHandler.currentInventoryType == InventorySelectionHandler.InventoryTypes.HAND)
            && (uIHandlerMain.uiState == UIHandlerMain.UIState.MAIN))
>>>>>>> Bugfixes: selectable icon only appear on hand, ball renders above ground plane, change size of occlusion area
        {
            ringSelection.TurnOn();
        }
        else
        {
            ringSelection.TurnOff();
        }
    }
}
