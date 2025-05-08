using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    List<String> inventory = new List<String>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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
            Debug.LogError("Item not found in inventory");
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
            Debug.LogError("Item not found in inventory");
            return false;
        }
    }
}
