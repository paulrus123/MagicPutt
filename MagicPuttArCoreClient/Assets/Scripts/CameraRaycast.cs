using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField]
    float maxRayDistance = 2.0f;

    public enum ObjectType {RAMP, NOTHING };
    public ObjectType RaycastHitObjectType;


    // Start is called before the first frame update
    void Start()
    {
        RaycastHitObjectType = ObjectType.NOTHING;
    }

    // Update is called once per frame
    void Update()
    {
        //Only hit layer 8
        int layermask = 1 << 8;
        layermask = -layermask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxRayDistance, layermask)) 
        {
            if(hit.collider.tag == "Ramp")
            {
                RaycastHitObjectType = ObjectType.RAMP;
                return;
            }
        }
        RaycastHitObjectType = ObjectType.NOTHING;
    }
}
