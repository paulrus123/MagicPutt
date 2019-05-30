using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GolfClubHandler : MonoBehaviour
{
    public ControllerConnectionHandler _controllerConnectionHandler;
    public GameObject golfClub;


    // Update is called once per frame
    void Update()
    {
        if (_controllerConnectionHandler.IsControllerValid())
        {
            MLInputController controller = _controllerConnectionHandler.ConnectedController;
            if (controller.Type == MLInputControllerType.Control)
            {
                // For Control, raw input is enough
                golfClub.transform.position = controller.Position;
                golfClub.transform.rotation = controller.Orientation;
            }
        }
    }
}
