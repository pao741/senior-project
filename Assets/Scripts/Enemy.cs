using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;

    public float knockbackPower = 100;
    public float knockbackDuration = 1;


    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.tag == "Projectile")
        {
            
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float timer = 0;

            if (rb != null) 
            {
                while (knockbackDuration < timer)
                {
                    timer += Time.deltaTime;
                    /*Vector2 difference = (other.transform.position - rb.transform.position).normalized;*/
                    Vector2 difference = (other.transform.position - rb.transform.position);
                    Debug.Log(difference);
                    rb.AddForce(-difference * knockbackPower);
                }

            }
        }

    }
}
