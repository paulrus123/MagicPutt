﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonHanlder : MonoBehaviour
{
    [SerializeField]
    UIHandlerMain uiHandler = default;

    public void ButtonPressed()
    {
        uiHandler.SetState(UIHandlerMain.UIState.MAIN);
    }
}