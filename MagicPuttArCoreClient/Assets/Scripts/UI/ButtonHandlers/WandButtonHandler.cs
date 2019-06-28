using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandButtonHandler : MonoBehaviour
{
    [SerializeField]
    GameObject noActionAvailableIcon = default;

    public enum WandState { NOACTIONPOSSIBLE };
    public WandState wandState;

    private void Start()
    {
        wandState = WandState.NOACTIONPOSSIBLE;
    }

    public void ButtonPressed()
    {
        if(wandState == WandState.NOACTIONPOSSIBLE)
        {
            noActionAvailableIcon.SetActive(true);
        }
    }
}
