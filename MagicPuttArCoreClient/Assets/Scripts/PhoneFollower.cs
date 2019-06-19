using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneFollower : MonoBehaviour
{
    public GameObject phone;

    // Update is called once per frame
    void Update()
    {
        transform.position = phone.transform.position;
        transform.eulerAngles = phone.transform.localEulerAngles;
    }
}
