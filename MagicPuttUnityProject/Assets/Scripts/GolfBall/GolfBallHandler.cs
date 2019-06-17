using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallHandler : MonoBehaviour
{
    [SerializeField]
    GolfBallAudio _audio = default;

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Wall":
                _audio.PlayGolfBallHitWall();
                break;
            case "GolfClub":
                _audio.PlayGolfBallHitClub();
                break;
            case "Hole":
                _audio.PlayGolfBallInHole();
                break;
            default:
                break;
        }
    }
}
