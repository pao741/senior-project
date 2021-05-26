using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 1000;
    public int currentHealth;
    //public Transform portalGFX;

    public HealthBar healthBar;
    //public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy();
        }

    }

    void Destroy()
    {
        /*Instantiate(deathEffect, transform.position, Quaternion.identity);*/
        Debug.Log("The portal fucking explodes");
        Destroy(gameObject);
    }
}
