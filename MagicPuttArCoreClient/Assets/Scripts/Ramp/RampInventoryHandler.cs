using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RampInventoryHandler : MonoBehaviour
{
    [SerializeField]
    Transform rampPrefabTransform = default;

    public RampRequest rampRequestMsg;
    RampPose rampPoseMsg;

    [SerializeField]
    Button rampButton = default;

    [SerializeField]
    GameObject rampObject = default;

    [SerializeField]
    GameObject rampAnimationObject = default;

    [SerializeField]
    CameraRaycast cameraRaycast = default;

    bool isInInventory = false;


    bool dirtyFlag = true; //If dirtyFlag == true then need to redo a SetActive()

    public enum States { RAMP_PLACED, REQUESTING_PICKUP, PICKED_UP, REQUESTING_PLACEMENT};
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
        if (!isInInventory || (cameraRaycast.RaycastHitObjectType != CameraRaycast.ObjectType.PLACEABLESURFACE))
        {
            Debug.Log("No inventory to place or no surface to place on");
            return false;
        }
        else
        {
            rampRequestMsg.position = rampPrefabTransform.InverseTransformPoint(cameraRaycast.hitPoint);
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

    //If ramp is not already in the inventory and the touch point is raycasting at the ramp, then pick it up
    public bool TryPickupRampFromTouch()
    {
        if (isInInventory)
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
            dirtyFlag = true; //set flag=true for GUI thread to update on next render 
            state = newState;
        }
    }

    private void Update()
    {
        if(dirtyFlag)
        {
            switch (state)
            {
                case States.REQUESTING_PICKUP:
                    rampRequestMsg.requestPickup = true;
                    rampRequestMsg.requestPlacement = false;
                    rampAnimationObject.SetActive(true);
                    rampObject.SetActive(false);
                    break;
                case States.REQUESTING_PLACEMENT:
                    rampRequestMsg.requestPlacement = true;
                    rampAnimationObject.SetActive(false);
                    rampObject.SetActive(false);
                    break;
                case States.PICKED_UP:
                    isInInventory = true;
                    rampRequestMsg.requestPickup = false;
                    rampRequestMsg.requestPlacement = false;
                    rampObject.SetActive(false);
                    break;
                case States.RAMP_PLACED:
                default:
                    rampObject.SetActive(true);
                    isInInventory = false;
                    rampRequestMsg.requestPickup = false;
                    rampRequestMsg.requestPlacement = false;
                    break;
            }
            //If no ramps in inventory then don't let user select ramp icon
            rampButton.interactable = isInInventory;
            dirtyFlag = false;
        }
    }
}
