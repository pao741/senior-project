using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 1000;

    public HealthBar healthBar;
    bool invulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player taking damage");
        Debug.Log(damage);
        healthBar.SetHealth(currentHealth); // set health bar to current health

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // YOU ARE FUCKING DEAD 
    }

    public void setInvulnerable(bool cond)
    {
        invulnerable = cond;
        Debug.Log(invulnerable);
    }
}
