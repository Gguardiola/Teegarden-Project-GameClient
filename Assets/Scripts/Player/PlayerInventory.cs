using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    List<String> inventory = new List<String>();
    
    public void AddItem(string item)
    {
        inventory.Add(item);
    }
    
    public String GetItem(String itemName)
    {
        if (inventory.Contains(itemName))
        {
            return itemName;
        }
        else
        {
            return null;
        }
    }

    public bool DeleteItem(String itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            return true;
        }
        else
        {
            return false;
        }
    }
}
