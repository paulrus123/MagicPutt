using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericButtonClickHandler : MonoBehaviour
{
    public delegate void ButtonClicked();
    public event ButtonClicked OnButtonClicked;

    public void ClickButton()
    {
        if(OnButtonClicked != null)
        {
            OnButtonClicked();
        }
    }
}
