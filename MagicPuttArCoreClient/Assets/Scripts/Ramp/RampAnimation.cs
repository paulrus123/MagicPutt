using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampAnimation : MonoBehaviour
{
    Animator rampAnimator;

    private void OnEnable()
    {
        rampAnimator = GetComponent<Animator>();
        rampAnimator.Play("RampPickedUp");
    }

    public void AnimationFinished()
    {
        gameObject.SetActive(false);
    }
}
