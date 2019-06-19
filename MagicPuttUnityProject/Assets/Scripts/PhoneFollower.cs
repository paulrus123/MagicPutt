using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneFollower : MonoBehaviour
{
    PhonePoseMessage pose;

    // Start is called before the first frame update
    void Start()
    {
        MqttClientHandler.OnPhonePoseReceived += MqttClientHandler_OnPhonePoseReceived;
    }

    void MqttClientHandler_OnPhonePoseReceived(string msg)
    {
        pose = JsonUtility.FromJson<PhonePoseMessage>(msg);
    }
     

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = pose.position;
        transform.localEulerAngles = pose.eulerAngles;
    }
}
