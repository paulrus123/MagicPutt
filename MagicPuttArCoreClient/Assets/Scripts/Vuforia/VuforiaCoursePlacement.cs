using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuforiaCoursePlacement : MonoBehaviour
{
    public GameObject course;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                ButtonClicked();
            }
        }
    }

    void ButtonClicked()
    {
        course.SetActive(true);
    }
}
