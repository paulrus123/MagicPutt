using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class VideoRecorder : MonoBehaviour
{
    bool _isCapturing;
    private const string _validFileFormat = ".mp4";

    private const float _minRecordingTime = 1.0f;
    private string _captureFilePath;

    private float _captureStartTime;

    Image _recordingIcon;

    private void HandleOnBumperDown(byte controller_id, MLInputControllerButton button)
    {
        if (button == MLInputControllerButton.Bumper)
        {
            if(!_isCapturing)
            {
                StartRecording();
            }
            else
            {
                StopRecording();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _recordingIcon = GetComponent<Image>();
        MLInput.OnControllerButtonDown += HandleOnBumperDown;
        _isCapturing = true;
    }

    void StartRecording()
    {
        string fileName = "MacicPutt" + System.DateTime.Now.ToString("MM_dd_yyyy__HH_mm_ss") + _validFileFormat;
        StartCapture(fileName);
        _recordingIcon.enabled = true;
    }

    void StopRecording()
    {
        try {
            MLCamera.StopVideoCapture();
        }
        catch
        {
            Debug.Log("Could not stop recording.. ");
        }
        _isCapturing = false;
        _recordingIcon.enabled = false;
    }

    public void StartCapture(string fileName)
    {
        if (!_isCapturing && MLCamera.IsStarted)
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

            MLResult result = MLCamera.StartVideoCapture(pathName);
            if (result.IsOk)
            {
                _isCapturing = true;
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
}
