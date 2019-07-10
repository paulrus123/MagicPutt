using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    public float maxRayDistance = 10.0f;
    public enum ObjectType { RAMP, NOTHING, PLACEABLESURFACE };
    public ObjectType RaycastHitObjectType = ObjectType.NOTHING;

    [SerializeField]
    RampInventoryHandler rampInventoryHandler = default;
    [SerializeField]
    InventorySelectionHandler inventorySelectionHandler = default;


    // Start is called before the first frame update
    void Start()
    {
        RaycastHitObjectType = ObjectType.NOTHING;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                RaycastHit hit;
                // Create a particle if hit
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Ramp")
                    {
                        RaycastHitObjectType = ObjectType.RAMP;
                        if(inventorySelectionHandler.currentInventoryType == InventorySelectionHandler.InventoryTypes.HAND)
                        {
                            rampInventoryHandler.TryPickupRampFromTouch();
                        }
                        return;
                    }
                    else if (hit.collider.tag == "PlaceableSurface")
                    {
                        RaycastHitObjectType = ObjectType.PLACEABLESURFACE;
                        return;
                    }
                }
                RaycastHitObjectType = ObjectType.NOTHING;
            }
        }
    }
}
