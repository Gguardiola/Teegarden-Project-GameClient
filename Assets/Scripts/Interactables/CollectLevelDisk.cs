using System;
using UnityEngine;

public class CollectLevelDisk : Interactable
{
    private PlayerInventory playerInventory;
    public string collectableItemName;
    private Sprite diskSprite;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        diskSprite = Resources.Load<Sprite>("UI/Inventory/UI_disk_inventory_slot");
    }

    protected override void Interact()
    {
        playerInventory.AddItem(collectableItemName, diskSprite);
        Destroy(gameObject);
    }
}
