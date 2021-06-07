using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    Transform mainTarget;
    public Transform thisEnemy;
    public Animator animator;
    public LayerMask enemyLayer;

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
    Vector3 roamDirection;
    float distanceFromRoamPosition;

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
        if (!isDead)
        {
            if (isRoaming && seeker.IsDone() && !reachedRoamingPath)
            {
                /*Roam*/
                if (!hasDestination)
                {
                    hasDestination = true;
                    while (true)
                    {
                        Vector3 randomDir = (Vector3)Random.insideUnitCircle.normalized;
                        roamDirection = thisEnemy.position + randomDir;
                        roamPosition = thisEnemy.position + randomDir * 5f;

                        RaycastHit2D hit = Physics2D.Raycast(thisEnemy.position, randomDir, 5f); 

                        /*Debug.DrawRay(thisEnemy.position, randomDir * 5f, Color.white, 4f);
                        Debug.Log(hit.collider.tag);*/
                        if (hit == null)
                        {
                            //Debug.Log("Path found");
                            break;
                        }
                        else // Doesn't hit
                        {
                            //Debug.Log("Path not found");
                            if (hit.collider.tag == "Walls") // if i remove this line the game crash idk why
                            {
                                
                            }
                        }
                    }
                }
                seeker.StartPath(thisEnemy.position, roamPosition, OnPathComplete);
            }
            else if (!isRoaming && seeker.IsDone())
            {
                seeker.StartPath(thisEnemy.position, target.position, OnPathComplete);
            }
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
            disableMovement = false;
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
            //rb.AddForce(force);
            rb.velocity = direction * 2f;
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
            Collider2D hitObject = Physics2D.OverlapCircle(roamPosition, 1f, enemyLayer);
            if (hitObject != null && hitObject.transform == thisEnemy)
            {
                StopRigidBody();
                animator.Play("Enemy1_idle");
                reachedRoamingPath = true;
                float seconds = Random.Range(3.0f, 8.0f);
                roamTimer = Time.time + seconds;
                disableMovement = true;
            }

            /*distanceFromRoamPosition = Vector2.Distance(thisEnemy.position, roamPosition);
            Debug.Log(distanceFromRoamPosition);
            if (distanceFromRoamPosition < 1f) //  && reachedRoamingPath == false check if waypoint is reached
            {
                StopRigidBody();
                reachedRoamingPath = true;
                float seconds = Random.Range(3.0f, 6.0f);
                roamTimer = Time.time + 3f;
                disableMovement = true;
            }*/
        }


        float far = Vector3.Distance(target.position, rb.position);

        if (far <= 2f && isRoaming) // check if can attack
        {
            reachedRoamingPath = false;
            isRoaming = false;
            disableMovement = false;
        }

        if (far <= attackRange && !takingDamage && !isRoaming) // check if can attack
        {
            // Calling attack animation
            animator.Play("Enemy1_attack");
        }
    }

    /*void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(roamPosition, 1f);
    }*/

    void setAnimation(Vector2 force) // set and flip animation
    {
        float differenceX = thisEnemy.position.x - roamPosition.x;
        if (force.x >= 0.01f && differenceX < 0.01)
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
