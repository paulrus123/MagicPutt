using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairButtonHandler : MonoBehaviour
{
    [SerializeField]
    UIHandlerMain handler = default;

    // Update is called once per frame
    public void ButtonPressed()
    {
        if(handler.uiState == UIHandlerMain.UIState.MAIN)
        {
            handler.SetState(UIHandlerMain.UIState.PLACECOURSE);
        }
        else if(handler.uiState == UIHandlerMain.UIState.PLACECOURSE)
        {
            handler.SetState(UIHandlerMain.UIState.MAIN);
        }
    }
}
