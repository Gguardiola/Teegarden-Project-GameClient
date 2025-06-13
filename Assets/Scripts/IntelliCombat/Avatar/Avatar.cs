using UnityEngine;

public class Avatar
{
    private float health = 100;
    private float maxHealth = 100;
    private float energy = 100;
    private float maxEnergy = 100;
    public bool isAlive = true;
    public bool isStunned = false;
    public bool isPoisoned = false;
    public bool isShielded = false;
    
    private string name = "player";
    
    public Avatar(string name = "player")
    {
        this.name = name;
    }
    
    public float GetHealth() { return health; }
    public float GetMaxHealth() { return maxHealth; }
    public float GetEnergy() { return energy; }
    public float GetMaxEnergy() { return maxEnergy; }
    public string GetName() { return name; }

    
    public void SetName(string newName) => name = newName;
    public void SetHealth(int value) => health = Mathf.Clamp(value, 0, maxHealth);
    public void SetMaxHealth(int value) => maxHealth = Mathf.Clamp(value, 0, int.MaxValue);
    public void SetEnergy(int value) => energy = Mathf.Clamp(value, 0, maxEnergy);
    public void SetMaxEnergy(int value) => maxEnergy = Mathf.Clamp(value, 0, int.MaxValue);
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isAlive = false;
            health = 0;
        }
    }
    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void UseEnergy(float amount)
    {
        energy -= amount;
        if (energy < 0)
        {
            energy = 0;
        }
    }
    public void RegenerateEnergy(float amount)
    {
        energy += amount;
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
    }
}
