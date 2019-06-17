using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip golfBallHitClub = default;
    [SerializeField]
    AudioClip golfBallHitWall = default;
    [SerializeField]
    AudioClip golfBallInHole = default;
    [SerializeField]
    AudioSource audioSource = default;

    public void PlayGolfBallHitClub()
    {
        audioSource.PlayOneShot(golfBallHitClub);
    }

    public void PlayGolfBallHitWall()
    {
        audioSource.PlayOneShot(golfBallHitWall);
    }

    public void PlayGolfBallInHole()
    {
        audioSource.PlayOneShot(golfBallInHole);
    }

}
