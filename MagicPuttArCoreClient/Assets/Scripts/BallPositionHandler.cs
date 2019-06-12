using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionHandler : MonoBehaviour
{
    Vector3 _position;

    // Start is called before the first frame update
    void Start()
    {
        DataDecoder.OnBallPositionDecoded += ReceiveNewBallPosition;
    }

    void ReceiveNewBallPosition(Vector3 position)
    {
        Debug.Log("BallPosition: " + position);
        _position = position;
    }

    private void Update()
    {
        transform.localPosition = _position;
    }
}
