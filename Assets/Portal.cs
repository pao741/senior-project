using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;

    public int maxHealth = 1000;
    public int currentHealth;
    //public Transform portalGFX;

    public HealthBar healthBar;

    public float interactingRange = 1f;

    public GameObject interactingMessage;
    private string message = "use the portal";

    private bool interactable = false;

    Rigidbody2D rb;
    //public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (interactable)
        {
            float distanceFromPlayer = Vector3.Distance(player.position, rb.position);
            if (distanceFromPlayer <= interactingRange)
            {
                interactingMessage.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(0);
                }
            }
            else
            {
                interactingMessage.SetActive(false);
            }
            // go to next stage
        }
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

    public void SetInteractable(bool state)
    {
        interactable = state;
    }
}
