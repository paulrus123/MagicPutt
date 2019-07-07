using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RampInventoryHandler : MonoBehaviour
{
    public RampRequest rampRequestMsg;
    RampPose rampPoseMsg;

    [SerializeField]
    Button rampButton = default;

    [SerializeField]
    CameraRaycast cameraRaycast = default;

    bool isInInventory = false;

    public enum States { RAMP_PLACED, REQUESTING_PICKUP, PICKED_UP,REQUESTING_PLACEMENT};
    public States state;

    private void Start()
    {
        state = States.RAMP_PLACED;
        rampPoseMsg = new RampPose();
        rampRequestMsg = new RampRequest();
        MqttClientHandler.OnRampPoseRecieved += ReceiveRampPose;
    }

    //If ramp is in inventory, then place it
    public bool TryPlaceRamp()
    {
        if (!isInInventory)
        {
            Debug.Log("No inventory to place");
            return false;
        }
        else
        {
            ChangeState(States.REQUESTING_PLACEMENT);
            return true;
        }
    }

    //If ramp is not already in the inventory and the camera is raycasting at the ramp, then pick it up
    public bool TryPickupRamp()
    {
        if ((isInInventory) || (cameraRaycast.RaycastHitObjectType != CameraRaycast.ObjectType.RAMP))
        {
            return false;
        }
        else
        {
            ChangeState(States.REQUESTING_PICKUP);
            return true;
        }
    }

    public void ReceiveRampPose(string msg)
    {
        rampPoseMsg = JsonUtility.FromJson<RampPose>(msg);

        switch(state)
        {
            case States.RAMP_PLACED:
            case States.REQUESTING_PICKUP:
                if (rampPoseMsg.isPlaced == false)
                    ChangeState(States.PICKED_UP);
                break;
            case States.REQUESTING_PLACEMENT:
            case States.PICKED_UP:
                if (rampPoseMsg.isPlaced == true)
                    ChangeState(States.RAMP_PLACED);
                break;
            default:
                break;
        }
    }

    public void ChangeState(States newState)
    {
        if(newState != state)
        {
            state = newState;
            switch (state)
            {
                case States.REQUESTING_PICKUP:
                    rampRequestMsg.requestPickup = true;
                    rampRequestMsg.requestPlacement = false;
                    break;
                case States.REQUESTING_PLACEMENT:
                    rampRequestMsg.requestPickup = false;
                    rampRequestMsg.requestPlacement = true;
                    break;
                case States.PICKED_UP:
                case States.RAMP_PLACED:
                default:
                    rampRequestMsg.requestPickup = false;
                    rampRequestMsg.requestPlacement = false;
                    break;
            }

            //If no ramps in inventory then don't let user select ramp icon
            rampButton.interactable = isInInventory;

        }
    }
}
