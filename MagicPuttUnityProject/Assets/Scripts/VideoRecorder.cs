using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class VideoRecorder : MonoBehaviour
{
    private const string _validFileFormat = ".mp4";
    private const float _minRecordingTime = 1.0f;
    private string _captureFilePath;
    private float _captureStartTime;

    [SerializeField]
    GameObject ImageTrackerPrefab = default;
    GameObject ImageTrackerInstance = null;

    [SerializeField]
    Image _recordingIcon = default;

    bool _isTracking = false;
    bool _isRecording = false;

    // Start is called before the first frame update
    void Start()
    {
        MLInput.OnControllerButtonDown += HandleButtonPress;
    }

    private void HandleButtonPress(byte controller_id, MLInputControllerButton button)
    {
        if (button == MLInputControllerButton.Bumper)
        {
            StopVideoRecording(); //stops video, starts the tracking
        }
        else if(button == MLInputControllerButton.HomeTap)
        {
            StartVideoRecording(); //Records video 
        }
    }

    void StopVideoRecording()
    {
        _isRecording = false;
        //_recordingIcon.enabled = false;

        //Stop Recording
        /* ------------------------------------------------------------------------------------------------------------*/
        try
        {
            MLCamera.StopVideoCapture();
        }
        catch
        {
            Debug.Log("Could not stop recording.. ");
        }
        /* ------------------------------------------------------------------------------------------------------------*/


        //Disable Camera
        /* ------------------------------------------------------------------------------------------------------------*/
        try
        {
            MLCamera.Disconnect();
            MLCamera.Stop();
        }
        catch
        {
            Debug.Log("Could not disconnect or stop camera.. ");
        }
        /* ------------------------------------------------------------------------------------------------------------*/

        //Instantiate Tracker
        /* ------------------------------------------------------------------------------------------------------------*/
        if (!_isTracking)
        {
            ImageTrackerInstance = Instantiate(ImageTrackerPrefab);
            _isTracking = true;
        }
        /* ------------------------------------------------------------------------------------------------------------*/
    }

    public void StartVideoRecording()
    {
        //Destroy tracker
        /* ------------------------------------------------------------------------------------------------------------*/
        if (ImageTrackerInstance != null)
        {
            Destroy(ImageTrackerInstance);
            ImageTrackerInstance = null;
        }
        _isTracking = false;
        /* ------------------------------------------------------------------------------------------------------------*/

        //TODO: Fix the "Record" so that it correctly records video.
        //For now, after disabling the tracker, use Home + Bumper to record video (https://www.magicleap.care/hc/en-us/articles/360020300672-HOW-TO-Capture-Screenshot-or-Video)
        //Record();

    }

    void Record()
    {
        //Enable Camera
        /* ------------------------------------------------------------------------------------------------------------*/
        if (!_isRecording)
        {
            _isRecording = true;
            MLResult result = MLCamera.Start();
            Debug.Log(result + " " + result.Code);
            if (result.IsOk)
            {
                result = MLCamera.Connect();
            }
            else
            {
                if (result.Code == MLResultCode.PrivilegeDenied)
                {
                    Instantiate(Resources.Load("PrivilegeDeniedError"));
                }

                Debug.LogErrorFormat("Error: ImageCaptureExample failed starting MLCamera, disabling script. Reason: {0}", result);
                return;
            }
            /* ------------------------------------------------------------------------------------------------------------*/


            //Start Recording
            /* ------------------------------------------------------------------------------------------------------------*/
            string fileName = "MacicPutt" + System.DateTime.Now.ToString("MM_dd_yyyy__HH_mm_ss") + _validFileFormat;
            _recordingIcon.enabled = true;

            if (MLCamera.IsStarted)
            {
                // Check file fileName extensions
                string extension = System.IO.Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(extension) || !extension.Equals(_validFileFormat, System.StringComparison.OrdinalIgnoreCase))
                {
                    Debug.LogErrorFormat("Invalid fileName extension '{0}' passed into Capture({1}).\n" +
                        "Videos must be saved in {2} format.", extension, fileName, _validFileFormat);
                    return;
                }

                string pathName = System.IO.Path.Combine(Application.persistentDataPath, fileName);
                Debug.Log(pathName);

                result = MLCamera.StartVideoCapture(pathName);
                if (result.IsOk)
                {
                    _captureStartTime = Time.time;
                    _captureFilePath = pathName;
                }
                else
                {
                    if (result.Code == MLResultCode.PrivilegeDenied)
                    {
                        Instantiate(Resources.Load("PrivilegeDeniedError"));
                    }

                    Debug.LogErrorFormat("Error: VideoCaptureExample failed to start video capture for {0}. Reason: {1}", fileName, MLCamera.GetErrorCode().ToString());
                }
            }
            else
            {
                Debug.LogErrorFormat("Error: VideoCaptureExample failed to start video capture for {0} because '{1}' is already recording!",
                    fileName, _captureFilePath);
            }
        }
        /* ------------------------------------------------------------------------------------------------------------*/
    }
}
