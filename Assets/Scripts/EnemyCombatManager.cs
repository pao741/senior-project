using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatManager : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject bulletPrefab;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public bool attacking = false;
    public float bulletForce = 20f;

    Vector3 prevPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            // this is called multiple time since attacking will be set true for 3 frame for consistency
            Attack(); 
        }
    }

    public void Attack() {
    
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        foreach (Collider2D collider in hitObjects)
        {

            if (collider.tag == "Player"){
                // despite being called multiple time, player are granted iframe
                // to prevent player taking damage too rapidly
                int attackDamage = 40;
                Player player = collider.GetComponent<Player>();
                if (player)
                {
                    player.TakeDamage(attackDamage);
                }
            }
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 difference = (prevPlayerPosition - rb.transform.position).normalized;
        rb.AddForce(difference * bulletForce, ForceMode2D.Impulse);
    }

    public void SetAttackingPosition()
    {
        prevPlayerPosition = Player.GetPosition();
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void SetAttacking(bool state)
    {
        attacking = state;
    }
}
