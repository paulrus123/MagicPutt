using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField]
    float maxRayDistance = 10.0f;

    public enum ObjectType {RAMP, NOTHING,PLACEABLESURFACE };
    public ObjectType RaycastHitObjectType;

    public Vector3 hitPoint;


    // Start is called before the first frame update
    void Start()
    {
        RaycastHitObjectType = ObjectType.NOTHING;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxRayDistance)) 
        {
            hitPoint = hit.point;
            if (hit.collider.tag == "Ramp")
            {
                RaycastHitObjectType = ObjectType.RAMP;
                return;
            }
            else if(hit.collider.tag == "PlaceableSurface")
            {
                RaycastHitObjectType = ObjectType.PLACEABLESURFACE;
                return;
            }
        }
        RaycastHitObjectType = ObjectType.NOTHING;
    }
}
