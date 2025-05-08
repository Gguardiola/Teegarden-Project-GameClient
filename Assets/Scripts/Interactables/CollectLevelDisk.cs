using System;
using UnityEngine;

public class CollectLevelDisk : Interactable
{
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
    }

    protected override void Interact()
    {
        playerInventory.AddItem("Item_disk");
        Destroy(gameObject);
    }
}
