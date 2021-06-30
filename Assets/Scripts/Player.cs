using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 100;

    GameObject healthBarObject;
    HealthBar healthBar;

    static Shooting shootingManager;
    static PlayerCombatManager meleeManager;
    static GameObject shootingManagerGameObject;
    static GameObject meleeManagerGameObject;

    public CameraShake cameraShake;
    public static bool isDead;

    bool invulnerable = false;
    float damageCooldownTimer = 1.35f;
    float nextDamageTime = 0f;

    public GameObject DeathMessage;

    public static Transform playerTransform;
    public static int stageCleared = 0;
    static AimHandler aimHandler;

    void Start()
    {
        isDead = false;

        shootingManagerGameObject = gameObject.transform.GetChild(0).gameObject;
        meleeManagerGameObject = gameObject.transform.GetChild(1).gameObject;

        shootingManager = shootingManagerGameObject.GetComponent<Shooting>();
        meleeManager = meleeManagerGameObject.GetComponent<PlayerCombatManager>();

        healthBarObject = GameObject.Find("/Canvas(Clone)/InGameUI/HealthBar/");
        healthBar = healthBarObject.GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        AssetDatabase.SaveAssets();
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
}
