using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsButtonHandler : MonoBehaviour
{
    [SerializeField]
    GameObject itemsPopup = default;

    void Start()
    {
        itemsPopup.SetActive(false);
    }

    public void ButtonPressed()
    {
        itemsPopup.SetActive(!itemsPopup.activeSelf);
    }
}
