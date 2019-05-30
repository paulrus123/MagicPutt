using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubPositionHandler : MonoBehaviour
{
    void ReceiveNewPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    void ReceiveNewRotation(Vector3 rotation)
    {
        transform.localEulerAngles = rotation;
    }
}
