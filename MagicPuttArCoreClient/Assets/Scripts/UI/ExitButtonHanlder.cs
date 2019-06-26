﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonHanlder : MonoBehaviour
{
    [SerializeField]
    UIHandlerMain uiHandler = default;

    [SerializeField]
    ImageTracker imageTracker = default;

    public void ButtonPressed()
    {
        imageTracker.SetTrackingOff();
        uiHandler.SetState(UIHandlerMain.UIState.MAIN);
    }
}
