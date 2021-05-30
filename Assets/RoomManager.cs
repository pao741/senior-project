using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    GameObject[] enemies;

    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        /*SpawnFromAllPoints();*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // input to summon enemy
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoints = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoints].position, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.T)) // input to summon enemy
        {
            SpawnFromAllPoints();
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length == 0)
        {
            Debug.Log("No enemy left");
        }

    }

    void SpawnFromAllPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randEnemy], spawnPoints[i].position, transform.rotation);
        }
    }
}
