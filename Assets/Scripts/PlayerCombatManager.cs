using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    
    public Animator animator;

    public Transform attackPoint;
    //public Transform player;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRange = 0.5f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    float nextSeqTime = 0f;

    public int attackSeq = 0;
    int clickCount = 0;

    float lastAttack = 0;

    public int maxDurability = 100;
    public int currentDurability;
    public string durabilityText = "100%";

    void Start()
    {
        currentDurability = maxDurability;
        UpdateText();
    }

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            attackSeq = 0;
            clickCount = 0;
        }

        if (Input.GetButtonDown("Fire1") && !PauseMenu.isPaused && !Player.isDead)
        {
            
            clickCount++;
            clickCount = Mathf.Clamp(clickCount, 0, 3);
            
            if (attackSeq == 0 && Time.time >= nextAttackTime && clickCount >= 1)
            {
                animator.SetTrigger("Attack1"); 
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (attackSeq == 1 && clickCount >= 2)
            {
                animator.SetTrigger("Attack2");
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (attackSeq == 2 && clickCount >= 3)
            {
                animator.SetTrigger("Attack3"); 
                nextAttackTime = Time.time + 1f / attackRate;
                clickCount = 0;
            }
        }
    }

    public void incrementSeq()
    {
        if(Time.time >= nextSeqTime)
        {
            attackSeq++;
            nextSeqTime = Time.time + 0.1f;
        }
    }

    void FixedUpdate()
    {
        Aim();
    }

    public void Attack()
    {
        if (Time.time > lastAttack)
        {
            lastAttack = Time.time + 0.01f;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D collider in hitEnemies)
            {
                EnemyAI enemy = collider.GetComponent<EnemyAI>();
                if (enemy)
                {
                    //enemy.TakeDamage(attackDamage, player);
                    //enemy.knockBack(player.position);
                    enemy.TakeDamage(attackDamage, Player.playerTransform);
                    enemy.knockBack(Player.getPosition());
                    currentDurability -= 5;
                }
            }
            UpdateText();
        }
    }

    /*void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }*/

    public void Refill()
    {
        currentDurability += maxDurability/2;
        if (currentDurability > maxDurability)
        {
            currentDurability = maxDurability;
        }
        //Reload();
        UpdateText();
    }

    void UpdateText()
    {
        durabilityText= currentDurability.ToString() + "%";
    }

    void Aim()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (rotationZ < -90 || rotationZ > 90)
        {
            if (attackPoint.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
            }
            else if (attackPoint.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            }
        }
    }
}
