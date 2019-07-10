using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSelectionHandler : MonoBehaviour
{

    [SerializeField]
    Transform _camera = default;

    [SerializeField]
    float minScale = 0.1f;

    [SerializeField]
    float maxScale = 1.2f;

    [SerializeField]
    float animationSpeed = 2.0f;

    private float currentScale = 0.1f;

    Vector3 localScale;

    SpriteRenderer sprite;

    public enum States { OFF, TURNING_ON, ON, TURNING_OFF};
    public States state;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        currentScale = minScale;
        localScale = new Vector3(currentScale , currentScale, currentScale);
        SetState(States.OFF);
    }

    void Update()
    {
        transform.LookAt(_camera);

        switch (state)
        {
            case States.TURNING_ON:
                currentScale += Time.deltaTime * animationSpeed;
                SetLocalScale();
                if (currentScale >= maxScale)
                {
                    SetState(States.ON);
                }
                break;
            case States.TURNING_OFF:
                currentScale -= Time.deltaTime * animationSpeed;
                SetLocalScale();
                if (currentScale <= minScale)
                {
                    SetState(States.OFF);
                }
                break;
            default:
                break;
        }
    }

    public void TurnOn()
    {
        if ((state == States.OFF) || (state == States.TURNING_OFF))
        {
            SetState(States.TURNING_ON);
        }
    }

    public void TurnOff()
    {
        if ((state == States.ON) || (state == States.TURNING_ON))
        {
            SetState(States.TURNING_OFF);
        }
    }

    private void OnDisable()
    {
        SetState(States.OFF);
    }


    void SetState(States newState)
    {
        state = newState;
        switch(state)
        {
            case States.OFF:
                sprite.enabled = false;
                currentScale = minScale;
                SetLocalScale();
                break;
            case States.ON:
                sprite.enabled = true;
                currentScale = maxScale;
                SetLocalScale();
                break;
            default:
                sprite.enabled = true;
                break;           
        }
    }

    void SetLocalScale()
    {
        localScale.x = currentScale;
        localScale.y = currentScale;
        localScale.z = currentScale;
        transform.localScale = localScale;
    }
}
