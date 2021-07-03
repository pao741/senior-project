using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : MonoBehaviour
{
    public float timeBetweenSpawn;
    float startTimeBetweenSpawn;

    public GameObject dashEffect;

    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerMovement.isDashing)
        {
            CreateEcho(dashEffect);
        }
    }

    void CreateEcho(GameObject echo)
    {
        if (timeBetweenSpawn <= 0)
        {            
            GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
            Destroy(instance, 1f);
            timeBetweenSpawn = startTimeBetweenSpawn;
        }
        else
        {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }
}
