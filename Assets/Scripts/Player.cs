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
    static AimHandler aimHandler;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;

        shootingManagerGameObject = gameObject.transform.GetChild(0).gameObject;
        meleeManagerGameObject = gameObject.transform.GetChild(1).gameObject;

        shootingManager = shootingManagerGameObject.GetComponent<Shooting>();
        meleeManager = meleeManagerGameObject.GetComponent<PlayerCombatManager>();

        //healthBar = healthBarObject.GetComponent<HealthBar>(); 
        healthBarObject = GameObject.Find("/Canvas(Clone)/InGameUI/HealthBar/");
        //healthBarObject = GameObject.Find("/Canvas(Clone)/InGameUI/HealthBar/");
        healthBar = healthBarObject.GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //EditorUtility.SetDirty(yourScript);
        //EditorSceneManager.MarkSceneDirty();

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
}
