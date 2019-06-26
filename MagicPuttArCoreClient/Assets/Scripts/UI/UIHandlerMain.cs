﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandlerMain : MonoBehaviour
{
    [SerializeField] 
    GameObject MainScreenCanvas = default;

    [SerializeField]
    GameObject InfoScreenCanvas = default;

    [SerializeField]
    GameObject PlaceCourseCanvas = default;

    [SerializeField]
    ImageTracker imageTracker = default;

    public enum UIState { MAIN, INFO, PLACECOURSE};
    public UIState uiState;


    // Start is called before the first frame update
    void Start()
    {
        SetState(UIState.MAIN);
    }

    public void SetState(UIState newState)
    {
        uiState = newState;

        switch (uiState)
        {
            case UIState.MAIN:
                MainScreenCanvas.SetActive(true);
                InfoScreenCanvas.SetActive(false);
                PlaceCourseCanvas.SetActive(false);
                imageTracker.SetTrackingOff();
                break;
            case UIState.INFO:
                imageTracker.SetTrackingOff();
                MainScreenCanvas.SetActive(false);
                InfoScreenCanvas.SetActive(true);
                PlaceCourseCanvas.SetActive(false);
                break;
            case UIState.PLACECOURSE:
                MainScreenCanvas.SetActive(false);
                InfoScreenCanvas.SetActive(false);
                PlaceCourseCanvas.SetActive(true);
                imageTracker.SetTrackingOn();
                break;
            default:
                MainScreenCanvas.SetActive(true);
                InfoScreenCanvas.SetActive(false);
                PlaceCourseCanvas.SetActive(false);
                imageTracker.SetTrackingOff();
                break;
        }
    }
}
