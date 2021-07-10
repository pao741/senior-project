using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    static int currentHealth;
    public static int maxHealth = 100;

    GameObject healthBarObject;
    static HealthBar healthBar;

    static Shooting shootingManager;
    static PlayerCombatManager meleeManager;
    static GameObject shootingManagerGameObject;
    static GameObject meleeManagerGameObject;

    public CameraShake cameraShake;
    public static bool isDead;

    static bool invulnerable = false;
    float damageCooldownTimer = 1.35f;
    float nextDamageTime = 0f;

    public GameObject DeathMessage;

    public static Transform playerTransform;
    public static int stageCleared = 0;
    static AimHandler aimHandler;


    public static bool isDashing = false;

    void Start()
    {
        isDead = false;
        stageCleared = 0;
        shootingManagerGameObject = GameObject.Find("GunAim");
        meleeManagerGameObject = GameObject.Find("MeleeAim");
        meleeManagerGameObject.SetActive(false);

        shootingManager = shootingManagerGameObject.GetComponent<Shooting>();
        meleeManager = meleeManagerGameObject.GetComponent<PlayerCombatManager>();
        healthBarObject = GameObject.Find("/Canvas(Clone)/InGameUI/HealthBar/");
        healthBar = healthBarObject.GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);

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
        if (!invulnerable && !isDashing)
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
        DeathMenu.Enable();
        //GameObject.Find("/Canvas(Clone)/DeathMenu").SetActive(true);
    }

    public static void SetInvulnerable(bool cond)
    {
        invulnerable = cond;
    }

    public static Vector3 GetPosition()
    {
        return playerTransform.position;
    }

    public static bool Refill()
    {
        return aimHandler.Refill();
    }

    public static float GetAmmoCountPercentage()
    {
        return aimHandler.GetAmmoCountPercentage();
    }

    public static float GetSwordDurabilityPercentage()
    {
        return aimHandler.GetSwordDurabilityPercentage();
    }

    public static Shooting GetShootingManager()
    {
        return shootingManager;
    }

    public static PlayerCombatManager GetMeleeManager()
    {
        return meleeManager;
    }

    public static GameObject GetShootingManagerGameObject()
    {
        return shootingManagerGameObject;
    }

    public static GameObject GetMeleeManagerGameObject()
    {
        return meleeManagerGameObject;
    }

    public static GameObject GetGameObject()
    {
        return GameObject.Find("Player(Clone)");
    }

    public static void ResetPosition()
    {
        playerTransform.position = new Vector3(0, 0, 0);
    }

    public static void AddStageCleared()
    {
        stageCleared++;
    }

    public static int GetStagedCleared()
    {
        return stageCleared;
    }

    public static int GetHealth()
    {
        return currentHealth;
    }

    public static int GetMaxHealth()
    {
        return maxHealth;
    }

    public static bool Heal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 50;
            currentHealth = (currentHealth > maxHealth) ? maxHealth : currentHealth;
            healthBar.SetHealth(currentHealth); // set health bar to current health
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SetIntangible(bool cond)
    {
        if (cond) {
            GetGameObject().layer = 12;
        }
        else
        {
            GetGameObject().layer = 8;
        }
    }
}
