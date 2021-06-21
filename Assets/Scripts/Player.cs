using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 1000;

    public HealthBar healthBar;
    public CameraShake cameraShake;
    public static bool isDead;

    bool invulnerable = false;
    float damageCooldownTimer = 1.35f;
    float nextDamageTime = 0f;

    public GameObject DeathMessage;

    public static Transform playerTransform;
    static AimHandler aimHandler;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerTransform = GetComponent<Player>().transform;
        aimHandler = GetComponent<AimHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextDamageTime <= Time.time) // takingDamage timer
        {
            invulnerable = false;
        }
    }

    public void TakeDamage(int damage)
    {
        float timer = 0;
        if (!invulnerable)
        {
            nextDamageTime = Time.time + damageCooldownTimer;
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth); // set health bar to current health
            CinemachineShake.Instance.ShakeCamera(0.5f, .15f);

            invulnerable = true;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // YOU ARE FUCKING DEAD 
        isDead = true;
        DeathMessage.SetActive(true);
    }

    public void setInvulnerable(bool cond)
    {
        invulnerable = cond;
    }

    public static Vector3 getPosition()
    {
        return playerTransform.position;
    }

    public static bool Refill()
    {
        return aimHandler.Refill();
    }
}
