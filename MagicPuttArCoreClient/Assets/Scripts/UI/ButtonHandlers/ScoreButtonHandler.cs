using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreButtonHandler : MonoBehaviour
{
    [SerializeField]
    UIHandlerMain uiHandler = default;

    public void ButtonPressed()
    {
        uiHandler.SetState(UIHandlerMain.UIState.SCORECARD);
    }
}
