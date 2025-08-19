using System;
using UnityEngine;

public class HealPlayerInteractable : Interactable
{
    private PlayerHealth playerHealth;
    public int healthRestored = 20;

    private void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    protected override void Interact()
    {
        playerHealth.RestoreHealth(healthRestored);
        Destroy(gameObject);
    }
}
