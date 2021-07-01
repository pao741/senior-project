using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    public WaypointerHandler waypointerHandler;
    public int maxHealth = 1000;
    int currentHealth;
    //public Transform portalGFX;

    public HealthBar healthBar;

    public float interactingRange = 1f;

    public GameObject interactingMessage;
    public TextMeshProUGUI actionText;
    public string message = "use the portal";

    private bool interactable = false;

    bool invulnerable = false;
    float damageCooldownTimer = 1.35f;
    float nextDamageTime = 0f;

    Rigidbody2D rb;
    static Transform portalTransform;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        portalTransform = GetComponent<Portal>().transform;
    }

    void Update()
    {
        if (nextDamageTime <= Time.time) // takingDamage timer
        {
            invulnerable = false;
        }
        if (CheckPlayerInRange() && interactable)
        {

            //actionText.text = message;
            if (Input.GetKeyDown(KeyCode.E))
            {
                int sceneToLoad = 3;

                DontDestroyOnLoad(GameObject.Find("Player(Clone)"));
                DontDestroyOnLoad(GameObject.Find("Canvas(Clone)"));


                LevelLoader levelLoader = GameObject.Find("/Canvas(Clone)/").GetComponent<LevelLoader>();
                levelLoader.LoadNextLevel();
                levelLoader.StartLevel();
                //SceneManager.LoadScene(sceneToLoad);
                //SceneManager.MoveGameObjectToScene(GameObject.Find("Player"), sceneToLoad)
            }
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

            invulnerable = true;
        }

    }

    void Destroy()
    {
        /*Instantiate(deathEffect, transform.position, Quaternion.identity);*/
        //Debug.Log("The portal fucking explodes");
        Destroy(gameObject);
    }

    public void SetInteractable(bool state)
    {
        gameObject.SetActive(state);
        interactable = state;
        //waypointerHandler.setActiveWaypoint(state);
    }

    public bool CheckPlayerInRange()
    {
        float distanceFromPlayer = Vector3.Distance(Player.GetPosition(), transform.position);
        return distanceFromPlayer <= interactingRange;
    }

    public static Vector3 GetPosition()
    {
        return portalTransform.position;
    }
}
