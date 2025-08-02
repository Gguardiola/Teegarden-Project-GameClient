using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{ 
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    [SerializeField]
    PlayerUI playerUI;
    
    public void AddItem(string item, Sprite sprite)
    {
        int slotIndex = playerUI.UIAddItemToInventorySlot(sprite);
        if (slotIndex != -1)
        {
            inventory.Add(item, slotIndex);
        }

    }
    
    public String GetItem(String itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            return itemName;
        }

        return null;
    }

    public bool DeleteItem(String itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            
            playerUI.UIRemoveItemFromInventorySlot(inventory.GetValueOrDefault(itemName, -1));
            inventory.Remove(itemName);
            return true;
        }
        else
        {
            return false;
        }
    }
}
