using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    GameObject[] enemies;

    public Portal portal;

    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public bool roomCleared = false;


    private int numWaves = 2;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnFromAllPoints();
        numWaves--;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // input to summon enemy
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoints = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoints].position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.T)) // input to summon enemy
        {
            SpawnFromAllPoints();
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length == 0 && numWaves > 0)
        {
            // spawn more enemy
            Debug.Log(numWaves);
            SpawnFromAllPoints();
            numWaves--;
        }

        if(enemies.Length == 0 && numWaves == 0)
        {
            Debug.Log(enemies.Length);
            Debug.Log(numWaves);
            roomCleared = true;
            portal.SetInteractable(true);
            // activate portal
        }
        else // needed this other wise it would go into if for some reason
        {
            roomCleared = false;
            portal.SetInteractable(false);
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
