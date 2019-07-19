using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Scorecard : MonoBehaviour
{
    [SerializeField]
    GameObject card = default;

    private MLInputController _controller;


    // Start is called before the first frame update
    void Start()
    {
        _controller = MLInput.GetController(MLInput.Hand.Left);
    }

    // Update is called once per frame
    void Update()
    {

        if (_controller.Touch1PosAndForce.z > 0.0f)
        {
            card.SetActive(true);
        }
        else
        {
            card.SetActive(false);
        }
    }
}
