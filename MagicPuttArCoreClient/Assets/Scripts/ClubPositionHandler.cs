using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubPositionHandler : MonoBehaviour
{
    private void Start()
    {
        DataDecoder.OnPositionDecoded += ReceiveNewPosition;
        DataDecoder.OnRotationDecoded += ReceiveNewRotation;
    }

    Vector3 _position;
    Vector3 _rotation;

    private void Update()
    {
        transform.localPosition = _position * transform.localScale.x;
        transform.localEulerAngles = _rotation;

    }


    void ReceiveNewPosition(Vector3 position)
    {
        Debug.Log("Position: " + position);
        _position = position;
    }

    void ReceiveNewRotation(Vector3 rotation)
    {
        Debug.Log("Rotation: " + rotation);
        _rotation = rotation;
    }
}
