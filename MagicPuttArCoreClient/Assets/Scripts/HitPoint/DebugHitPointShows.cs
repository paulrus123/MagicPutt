using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHitPointShows : MonoBehaviour
{

    public CameraRaycast cameraRaycast;
    public InventorySelectionHandler inventorySelectionHandler;
    public GameObject renderedObject;
    public GameObject golfCourse;

    // Update is called once per frame
    void Update()
    {
        if(inventorySelectionHandler.currentInventoryType == InventorySelectionHandler.InventoryTypes.RAMP)
        {
            renderedObject.SetActive(true);
            transform.position = cameraRaycast.hitPoint;
            transform.eulerAngles = golfCourse.transform.eulerAngles;
        }
        else
        {
            renderedObject.SetActive(false);
        }
    }
}
