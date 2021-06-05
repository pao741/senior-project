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
    Vector3 attackingPosition; 

    float takingDamageTimer = 0;
    float targetTimer = 0;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool isDead = false;

    bool isRoaming = true;
    bool hasDestination = false;
    bool reachedRoamingPath = false;
    float roamTimer = 0;
    Vector3 roamPosition;

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
        if (isRoaming && seeker.IsDone() && !reachedRoamingPath)
        {
            /*Roam();*/
            if (!hasDestination)
            {
                Debug.Log("Assigning");
                hasDestination = true;
                roamPosition = rb.position + Random.insideUnitCircle.normalized * 2f;
            }
            seeker.StartPath(rb.position, roamPosition, OnPathComplete);
        }
        else if (!isRoaming && seeker.IsDone() && !isDead)
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
        if (isDead) // check if dead
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

        if (roamTimer <= Time.time && reachedRoamingPath == true)
        {
            hasDestination = false;
            reachedRoamingPath = false;
        }

        if (path == null) // check if path is null (it shoudn't)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count) // check if reach end of path
        {
            reachedEndOfPath = true;
            /*if (!isAttacking)
            {
                rb.velocity = Vector3.zero; // stop rigidbody completely if there is no more path to go
            }*/
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // calculate enemy movement
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (!disableMovement)
        { 
            rb.AddForce(force);
        }

        // check distance from waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWayPointDistance) // check if waypoint is reached
        {
            currentWaypoint++;
        }

        if (!disableMovement && !reachedRoamingPath)
        {
            setAnimation(force);
        }

        if (isRoaming)
        {
            if (Vector2.Distance(rb.position, roamPosition) < 0.3f && reachedRoamingPath == false) // check if waypoint is reached
            {
                rb.velocity = Vector3.zero;
                reachedRoamingPath = true;
                roamTimer = Time.time + 3f;
            }
        }

        float far = Vector3.Distance(target.position, rb.position);

        if (far <= attackRange && !takingDamage) // check if can attack
        {
            // Calling attack animation
            animator.Play("Enemy1_attack");
        }
    }

    void setAnimation(Vector2 force) // set and flip animation
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

    void OnCollisionEnter2D(Collision2D other) // unused for now
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

    }

    public void knockBack(Vector3 from)
    {
        /*float timer = 0f;*/
        float knockbackPower = 1000f;

        disableMovement = true;

        Vector2 difference = (from - rb.transform.position).normalized;
        rb.AddForce(-difference * knockbackPower);

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
        Vector2 difference = (attackingPosition - rb.transform.position).normalized;
        float attackPower = 2000f;

        /*rb.AddForce(difference * attackPower);*/
        rb.velocity = difference * 5f;
    }

    private void Roam()
    {
        Vector3 roamPosition = Random.insideUnitCircle.normalized;
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
    }

    public void StopRigidBody()
    {
        rb.velocity = Vector3.zero;
    }

    public void SetAttackingPosition()
    {
        attackingPosition = target.position;
    }
}
