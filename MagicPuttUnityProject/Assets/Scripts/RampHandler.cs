using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampHandler : MonoBehaviour
{

    [SerializeField]
    GameObject rampObject = default;

    RampRequest rampRequestMsg;

    public Vector3 eulerAngles;
    public Vector3 position;
    public bool isPlaced;
    public int index = 0;


    // Start is called before the first frame update
    void Start()
    {
        rampRequestMsg = new RampRequest();
    }

    // Update is called once per frame
    void Update()
    {
        //Update Ramp 
        if(rampRequestMsg.requestPickup == true)
        {
            rampObject.SetActive(false);
        }
        else if(rampRequestMsg.requestPlacement == true)
        {
            rampObject.SetActive(true);
            rampObject.transform.localPosition = rampRequestMsg.position;
            rampObject.transform.localEulerAngles = rampRequestMsg.eulerAngles;
        }

        //Set Values for RampPose
        eulerAngles = rampObject.transform.localEulerAngles;
        position = rampObject.transform.localPosition;
        isPlaced = rampObject.activeSelf;
    }


    void OnReceiveRampRequest(string msg)
    {
        rampRequestMsg = JsonUtility.FromJson<RampRequest>(msg);
    }
}
