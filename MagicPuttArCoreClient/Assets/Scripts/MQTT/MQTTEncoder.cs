using UnityEngine;

public class MQTTEncoder : MonoBehaviour
{
    public GameObject phone;
    
    public MqttClientHandler mqttClientHandler;

    public RampInventoryHandler rampInventoryHandler;

    PhonePoseMessage phonePoseMessage;

    float currTime;
    readonly float timeOut = 0.03f;

    private void Start()
    {
        phonePoseMessage = new PhonePoseMessage();
        currTime = 0;
    }

    private void Update()
    {

        currTime += Time.deltaTime;
        if(currTime >timeOut)
        {
            currTime = 0;
            Send();
        }
    }

    public void Send()
    {
        //Populate messages
        phonePoseMessage.position = phone.transform.localPosition;
        phonePoseMessage.eulerAngles = phone.transform.localEulerAngles;

        //Convert to JSON string
        string phonePoseJson = JsonUtility.ToJson(phonePoseMessage);
        string rampRequestJson = JsonUtility.ToJson(rampInventoryHandler.rampRequestMsg);

        //PublishJson
        mqttClientHandler.Publish("MagicPutt/PhonePose", phonePoseJson);
        mqttClientHandler.Publish("MagicPutt/RampRequest", rampRequestJson);
    }
}
