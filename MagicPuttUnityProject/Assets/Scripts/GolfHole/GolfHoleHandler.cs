using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHoleHandler : MonoBehaviour
{
    [SerializeField]
    GolfBallAudio _audio = default;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GolfBall")
        {
            _audio.PlayGolfBallInHole();
        }

    }
}
