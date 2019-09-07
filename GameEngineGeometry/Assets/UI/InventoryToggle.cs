﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryToggle : MonoBehaviour
{
    public static CollectableData collectable; //The current collectable in range.
    public static bool InventoryOpened = false; //Is the inventory opened or not.

    public GameObject InventoryObject; //Reference to the inventory object.
    public GameObject InventoryCamera; //Reference to the inventory camera.
    public GameObject GameCamera; //Reference to the game camera.

    public GameObject PickupTooltip; //Reference to the pickup tooltip.
    public Text pickupName; //Reference to the pickup text on the pickup tooltip.
    
    private bool InventoryToggled = false; //If the inventory toggled or not.

	void Start ()
    {
        //Set the static variables back to their default states.
        InventoryOpened = false;
        collectable = null;
    }

    void Update()
    {
        //If there is a valid tooltip the pickup tooltip object will enable itself or disable itself.
        PickupTooltip.SetActive(collectable != null);

        //If there is a collectable object.
        if (collectable != null)
        {
            //Set the pickup text to include the items name.
            pickupName.text = "Press E to pick up [1] " + collectable.itemName;

            //If the user presses E.
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Iterate through each inventory slot.
                for (int i = 0; i < 30; i++)
                {
                    //If the inventory slot is empty and the slot isn't part of the crafting window.
                    if (InventorySlot.inventorySlots[i].ItemName == "NA" && InventorySlot.inventorySlots[i].transform.parent.name != "CraftingWindow")
                    {
                        //Grab the slot and set the empty slot to a slot with the items data.
                        InventorySlot slot = InventorySlot.inventorySlots[i];

                        slot.ItemName = collectable.itemName;
                        slot.ItemFlavor = collectable.itemFlavor;
                        slot.ItemIcon = collectable.itemIcon;

                        slot.sprite.sprite = collectable.itemIcon;

                        //Destroy the collectable object.
                        Destroy(collectable._gameObject);
                        //Remove the static reference to the destroy object.
                        collectable = null;
                        //Break out of this loop.
                        break;
                    }
                }
            }
        } //Otherwise clear the pickup text.
        else pickupName.text = "";

        //If the tab key is pressed.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //Toggle the inventory.
            InventoryToggled = !InventoryToggled;
            InventoryOpened = InventoryToggled;
        }

        if(InventoryToggled)
        {
            //If the inventory is toggled on, display the inventory.
            InventoryObject.SetActive(true);
            InventoryCamera.SetActive(true);
            GameCamera.SetActive(false);
        }
        else
        {
            //If the inventory is toggled off then show the game and hide the inventory.
            InventoryObject.SetActive(false);
            InventoryCamera.SetActive(false);
            GameCamera.SetActive(true);
        }
    }
}
