using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelectionHandler : MonoBehaviour
{
    public enum InventoryTypes { HAND, RAMP};
    public InventoryTypes currentInventoryType;

    [SerializeField]
    RampInventoryHandler rampInventoryHandler = default;

    [SerializeField]
    Image mainButtonImage = default;

    [SerializeField]
    GameObject rotateButton = default;

    //Not available text and icon
    [SerializeField]
    GameObject noActionAvailableIcon = default;
    [SerializeField]
    Text mainButtonText = default;

    //Icons for middle button
    [SerializeField]
    Sprite handIcon = default;
    [SerializeField]
    Sprite rampIcon = default;

    [SerializeField]
    GameObject hitPoint = default;

    // Start is called before the first frame update
    void Start()
    {
        currentInventoryType = InventoryTypes.HAND;
        mainButtonImage.sprite = handIcon;
    }

    public void SetInventoryTypeHand()
    {
        currentInventoryType = InventoryTypes.HAND;
        mainButtonImage.sprite = handIcon;
        mainButtonText.text = "Touch objects to pick up or interact with it";
    }

    public void SetInventoryTypeRamp()
    {
        currentInventoryType = InventoryTypes.RAMP;
        mainButtonImage.sprite = rampIcon;
        mainButtonText.text = "Press the ramp button to place a ramp";
    }   

    public void MainButtonClicked()
    {
        switch(currentInventoryType)
        {
            case InventoryTypes.HAND:
                if(!rampInventoryHandler.TryPickupRamp())
                {
                    noActionAvailableIcon.SetActive(true);
                }
                break;
            case InventoryTypes.RAMP:
                if (!rampInventoryHandler.TryPlaceRamp())
                {
                    noActionAvailableIcon.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if((currentInventoryType == InventoryTypes.RAMP) && (rampInventoryHandler.isInInventory))
        {
            rotateButton.SetActive(true);
            hitPoint.SetActive(true);
        }
        else
        {
            rotateButton.SetActive(false);
            hitPoint.SetActive(false);
        }
    }


    void HandClicked()
    {
        rampInventoryHandler.TryPickupRamp();
    }

    void RampClicked()
    {
        rampInventoryHandler.TryPlaceRamp();
    }
}
