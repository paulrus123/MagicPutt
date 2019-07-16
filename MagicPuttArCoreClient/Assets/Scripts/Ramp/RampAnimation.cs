using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampAnimation : MonoBehaviour
{
    Animator rampAnimator;

    private void OnEnable()
    {
        rampAnimator = GetComponent<Animator>();
        rampAnimator.speed = 1.6f;
        rampAnimator.Play("RampPickedUp");
    }

    public void AnimationFinished()
    {
        gameObject.SetActive(false);
    }
}
