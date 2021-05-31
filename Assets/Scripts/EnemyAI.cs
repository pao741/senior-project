using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    Transform mainTarget;
    public Animator animator;

    public float speed = 100f;
    public float nextWayPointDistance = 3f;

    public Transform enemyGFX;

    public int maxHealth = 1000;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject deathEffect;

    public float knockbackPower = 100f;
    public float knockbackDuration = 0.3f;
    public bool disableMovement = false;

    public float attackRange = 1.6f;
    bool isAttacking = false;
    bool takingDamage = false;

    float takingDamageTimer = 0;
    float targetTimer = 0;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool isDead = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        mainTarget = target;

        InvokeRepeating("UpdatePath", 0f, .1f);
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && !isDead)
        {

            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        if (takingDamageTimer <= Time.time) // takingDamage timer
        {
            takingDamage = false;
        }

        if (targetTimer <= Time.time) // aggro timer
        {
            target = mainTarget;
        }

        if (path == null)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (!disableMovement)
        { 
            rb.AddForce(force);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }
        
        setAnimation(force);
        float far = Vector3.Distance(target.position, rb.position);
        /*Debug.Log(far);*/
        if (far <= attackRange && !takingDamage)
        {
            // Calling attack animation
            animator.Play("Enemy1_attack");
        }
    }

    void setAnimation(Vector2 force)
    {
        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        animator.SetFloat("Speed", force.sqrMagnitude);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Projectile")
        {
            //float timer = 0;
            if (rb != null)
            {
                /*Debug.Log("Hit");*/
                disableMovement = true;
                /*Vector2 difference = (other.transform.position - rb.transform.position).normalized;
                Debug.Log(difference);
                rb.AddForce(-difference * knockbackPower);*/

            }
        }
        /*if (other.gameObject.tag == "Player" && isAttacking)
        {
            //float timer = 0;
            if (other != null)
            {
                // do something
                Player player = other.gameObject.GetComponent<Player>();
                //player.TakeDamage(40);
            }
            else
            {
                *//*Debug.Log("is null");*//*
            }
        }
        if (other.gameObject.tag == "Portal" && isAttacking)
        {
            if (other != null)
            {
                // do something
                *//*Debug.Log("Attacking portal");*//*
                Portal portal = other.gameObject.GetComponent<Portal>();
                portal.TakeDamage(40);
            }
            else
            {
                *//*Debug.Log("is null");*//*
            }
        }

        disableMovement = false;*/

    }

    public void knockBack(Vector3 from)
    {
        /*float timer = 0f;*/
        float knockbackPower = 1000f;

        disableMovement = true;
        /*while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 difference = (from - rb.transform.position).normalized;
            rb.AddForce(-difference * knockbackPower);
        }*/
        Vector2 difference = (from - rb.transform.position).normalized;
        rb.AddForce(-difference * knockbackPower);
        /*rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;*/
        disableMovement = false;
    }

    public void TakeDamage(int damage, Transform source)
    {
        takingDamage = true;

        SetTarget(source);
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth); // set health bar to current health

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.Play("Enemy1_damaged");
            //animator.SetTrigger("Damaged");
        }
        takingDamageTimer = Time.time + 1f;
        /*Debug.Log(takingDamageTimer);*/

    }

    void Die()
    {
        isDead = true;
        animator.Play("Enemy1_death");
        /*Instantiate(deathEffect, transform.position, Quaternion.identity);*/
        Debug.Log("It fucking dies");
        GetComponent<Collider2D>().enabled = false;
        healthBar.Destroy(); //Destroy(healthBar);
        gameObject.tag = "Corpse";

        Destroy(gameObject, 10f);
    }

    public void Attack()
    {

        // find direction -> move to that direction -> check for collision
        Vector2 difference = (target.position - rb.transform.position).normalized;
        float attackPower = 2000f;

        rb.AddForce(difference * attackPower);
    }

    public void SetAttacking(bool state)
    {
        isAttacking = state;
    }

    public void SetDisableMovement(bool state)
    {
        disableMovement = state;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        targetTimer = Time.time + 30f;
        Debug.Log("Setting new target");
    }
}
