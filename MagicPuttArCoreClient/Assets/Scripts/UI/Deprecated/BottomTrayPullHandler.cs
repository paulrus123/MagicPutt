using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BottomTrayPullHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool buttonPressed;
    float travel;
    Vector2 startPoint;
    const float maxTravel = 400;
    public GameObject drawer;

    public void OnPointerDown(PointerEventData eventData)
    {
        startPoint = eventData.position;
        buttonPressed = true;
    }

    private void Start()
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(travel > 40)
        {
            //OPEN 
            drawer.SetActive(true);
        }
        else if(travel < -40)
        {
            //CLOSE
            drawer.SetActive(false);
        }
        buttonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed && (Input.touchCount > 0))
        {
            travel = Input.GetTouch(0).position.y - startPoint.y;
        }
    }

}
