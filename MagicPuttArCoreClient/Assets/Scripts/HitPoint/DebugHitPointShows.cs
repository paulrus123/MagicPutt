using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHitPointShows : MonoBehaviour
{

    public CameraRaycast cameraRaycast;
    public InventorySelectionHandler inventorySelectionHandler;
    public GameObject renderedObject;
    public GameObject golfCourse;
    public RampInventoryHandler rampInventoryHandler;

    // Update is called once per frame
    void Update()
    {
        if((inventorySelectionHandler.currentInventoryType == InventorySelectionHandler.InventoryTypes.RAMP) && (cameraRaycast.RaycastHitObjectType == CameraRaycast.ObjectType.PLACEABLESURFACE))
        {

            renderedObject.SetActive(true);
            transform.position = cameraRaycast.hitPoint;
            transform.localEulerAngles = rampInventoryHandler.rampRequestMsg.eulerAngles;
        }
        else
        {
            renderedObject.SetActive(false);
        }
    }
}
