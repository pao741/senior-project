using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    public float timeBetweenSpawn;
    public float startTimeBetweenSpawn;

    public GameObject dashEffect;

    void Start()
    {

    }

    void Update()
    {
        if (!PlayerMovement.isDashing)
        {
            return;
        }
        CreateEcho(dashEffect);
    }

    void CreateEcho(GameObject echo)
    {
        if (timeBetweenSpawn <= 0)
        {
            GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
            Destroy(instance, 0.2f);
            timeBetweenSpawn = startTimeBetweenSpawn;
        }
        else
        {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }
}
