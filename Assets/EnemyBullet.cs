using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject hitEffect;
    public float maxVelocity = 5f;
    public int damage = 40;

    //Collider2D collider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //collider = GetComponent<Collider2D>();
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity); /// set max velocity

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            //Physics2D.IgnoreCollision(collision.collider, collider);
        }
        else
        {
            if (collision.collider.tag == "Player")
            {
                Player player = collision.collider.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(damage);

                }
            }

            GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }



    }
}
