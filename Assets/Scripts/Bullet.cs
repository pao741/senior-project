using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D rb;
    public GameObject hitEffect;
    public float maxVelocity = 11f;
    public int damage = 40;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity); /// set max velocity
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyAI enemy = collision.collider.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.SetIsRoaming(false);
            enemy.TakeDamageWithoutAnimation(20, Player.playerTransform);

        }

        GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
        Destroy(gameObject);

    }
}
