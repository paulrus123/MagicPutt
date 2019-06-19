using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLCameraFollower : MonoBehaviour
{
    [SerializeField]
    GameObject MLCam = default;

    // Update is called once per frame
    void Update()
    {
        transform.position = MLCam.transform.position;
        transform.eulerAngles = MLCam.transform.eulerAngles;
    }
}
