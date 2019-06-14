using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GolfCourseVisualizer : MonoBehaviour
{

    public MLImageTrackerBehavior _trackerBehavior;
    public GameObject golfCourse;
    public GameObject golfBall;
    public GameObject recordingText;

    bool trackingAllowed = true;


    // Start is called before the first frame update
    void Start()
    {
        MLInput.OnTriggerDown += HandleOnTriggerDown;
        MLInput.OnControllerButtonDown += HandleOnBumperDown;
    }

    private void HandleOnTriggerDown(byte controllerId, float triggerValue)
    {
        if (!trackingAllowed)
            return;
        if (!_trackerBehavior.IsTracking)
            return;

        golfCourse.SetActive(true);
        golfCourse.transform.position = _trackerBehavior.transform.position;
        golfCourse.transform.eulerAngles = new Vector3(0, _trackerBehavior.transform.eulerAngles.y, 0);

        golfBall.transform.position = new Vector3(_trackerBehavior.transform.position.x, _trackerBehavior.transform.position.y + 0.5f, _trackerBehavior.transform.position.z);
    }

    private void HandleOnBumperDown(byte controller_id, MLInputControllerButton button)
    {
        //if (button == MLInputControllerButton.Bumper)
        //{
        //    trackingAllowed = !trackingAllowed;
        //    if (trackingAllowed)
        //    {
        //        MLCamera.StartVideoCapture("/documents/magicPuttCapture" + DateTime.Now.ToFileTime());
        //    }
        //    else
        //        MLCamera.StopVideoCapture();

        //    recordingText.SetActive(trackingAllowed);
        //}
    }
}
