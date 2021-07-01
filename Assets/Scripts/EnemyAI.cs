using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform thisEnemy;
    public Animator animator;
    public LayerMask enemyLayer;
    public GameObject batteryPrefab;
    public GameObject healthPlusPrefab;

    public float speed = 100f;
    public float nextWayPointDistance = 3f;

    public Transform enemyGFX;

    public int maxHealth = 1000;
    public int currentHealth;

    public HealthBar healthBar;

    public float knockbackPower = 100f;
    public float knockbackDuration = 0.3f;
    public bool disableMovement = false;

    public float attackRange = 1.6f;
    bool isAttacking = false;
    bool takingDamage = false;
    Vector3 attackingPosition; 

    float takingDamageTimer = 0;
    //float targetTimer = 0;

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
    Vector3 Headingto;

    float distanceFromRoamPosition;

    Seeker seeker;
    Rigidbody2D rb;
    Collider2D collider;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        float seconds = Random.Range(1.0f, 8.0f);
        roamTimer = Time.time + seconds;

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

                        
                        if (hit == null)
                        {
                            break;
                        }
                        else if (hit.collider.tag == "Walls")
                        {

                        }
                    }
                }
                Headingto = roamPosition;
                seeker.StartPath((Vector2)thisEnemy.position, (Vector2)Headingto, OnPathComplete);

            }
            else if (!isRoaming && seeker.IsDone())
            {
                //seeker.StartPath(thisEnemy.position, target.position, OnPathComplete);
                Headingto = Player.GetPosition();
                seeker.StartPath(thisEnemy.position, Headingto, OnPathComplete);
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
            //disableMovement = false;
            takingDamage = false;
            animator.SetBool("Damaged", false);
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

        float far = Vector3.Distance(Player.GetPosition(), thisEnemy.position);
        
        if (far <= 5f && isRoaming) // check if can attack (Must come before check for the end of path)
        {
            isRoaming = false;
            reachedRoamingPath = false;
            disableMovement = false;
        }

        if (far <= attackRange && !takingDamage && !isRoaming) // check if can attack 
        {
            RaycastHit2D hit = Physics2D.Raycast(thisEnemy.position, -(thisEnemy.position - Player.GetPosition()).normalized, 5f);
            //Debug.DrawRay(thisEnemy.position, -(thisEnemy.position - Player.getPosition()).normalized * attackRange, Color.white, 4f);
            if(hit.collider.tag == "Player")
            {
                animator.SetBool("Attack", true);
            }
        }

        // THIS IF STATEMENT IS VERY WEIRD (sometimes code below it doesn't even get to run)
        // Index out of bound will happen if this chunk is moved to the end of the Update()
        // vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
        
        if (currentWaypoint >= path.vectorPath.Count) // check if reach end of path
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

        

        // calculate enemy movement
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (!disableMovement && !isAttacking)
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
                //animator.Play("Enemy1_idle");
                //animator.Play("Enemy1_idle");
                reachedRoamingPath = true;
                float seconds = Random.Range(3.0f, 8.0f);
                roamTimer = Time.time + seconds;
                disableMovement = true;
            }
        }
        
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(Headingto, 1f);
        //Gizmos.DrawWireSphere(thisEnemy.position, 1f);
    }

    void setAnimation(Vector2 force) // set and flip animation
    {
        //float differenceX = Mathf.Abs(rb.position.x - Headingto.x);
        //Debug.Log(differenceX);

        if (force.x >= 10f)
        //if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -10f)
        //else if (force.x <= -0.01f)
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
                //Vector2 otherPosition = other.transform.position;
                
                //isRoaming = false;

                //TakeDamageWithoutAnimation(40, Player.playerTransform);

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

        //rb.velocity = -difference * 5f;

        disableMovement = false;
    }

    public void TakeDamage(int damage, Transform source)
    {
        takingDamage = true;
        //disableMovement = true;
        //SetTarget(source);
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth); // set health bar to current health

        // turn to where damage is coming from
        if (rb.position.x > source.position.x)  // from left
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else // from right
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            //animator.Play("Enemy1_damaged");
            animator.SetBool("Damaged", true);
        }
        takingDamageTimer = Time.time + 1f;
        /*Debug.Log(takingDamageTimer);*/

    }

    public void TakeDamageWithoutAnimation(int damage, Transform source)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth); // set health bar to current health
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        StopRigidBody();
        animator.SetTrigger("Death");
        gameObject.layer = 12; // Set enemy layer to 12 (DeadEnemy layer) to disable rigidbody interaction 
        healthBar.Destroy();
        gameObject.tag = "Corpse";
        DropBattery();
        DropHealth();
        Destroy(gameObject, 10f);
    }

    void DropBattery()
    {
        float ammoPercentage = Player.GetAmmoCountPercentage();
        float durabilityPercentage = Player.GetSwordDurabilityPercentage();
        if(ammoPercentage == 0 && durabilityPercentage == 0)
        {
            GameObject battery = Instantiate(batteryPrefab, thisEnemy.position, thisEnemy.rotation);
        }
        else
        {
            float lower = (ammoPercentage < durabilityPercentage) ? ammoPercentage : durabilityPercentage;
            float random = Random.Range(0f, 1f);
            float dropProbability = 0.35f * (1 - lower); // max drop change of 35% 
            if (random <= dropProbability)
            {
                GameObject battery = Instantiate(batteryPrefab, thisEnemy.position, thisEnemy.rotation);
            }
        }
    }

    void DropHealth()
    {
        float percent = Player.GetHealth() / Player.GetMaxHealth(); //
        float random = Random.Range(0f, 1f);
        float dropProbability = 0.6f * (1 - percent); // max drop change of 50% (though max chance can only be achieve by having 0 health)
        if (random <= dropProbability)
        {
            GameObject healthPlus = Instantiate(healthPlusPrefab, thisEnemy.position, thisEnemy.rotation);
        }
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

    public void StopRigidBody()
    {
        rb.velocity = Vector3.zero;
    }

    public void SetAttackingPosition()
    {
        //attackingPosition = target.position;
        attackingPosition = Player.GetPosition();
    }

    public void SetAnimatorAttack(bool cond)
    {
        animator.SetBool("Attack", cond);
    }

    public Collider2D getCollider()
    {
        return collider;
    }

    public void SetIsRoaming(bool cond)
    {
        isRoaming = cond;
    }
}
