using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatManager : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public bool attacking = false;

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
                Debug.Log("Hit player");
                int attackDamage = 40;
                Player player = collider.GetComponent<Player>();
                if (player)
                {
                    player.TakeDamage(attackDamage);
                }
            }

            if (collider.tag == "Portal")
            {
                Debug.Log("Hit Portal");
                int attackDamage = 40;
                Portal portal = collider.GetComponent<Portal>();
                if (portal)
                {
                    portal.TakeDamage(attackDamage);
                }
            }
        }
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
