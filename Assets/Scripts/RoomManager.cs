using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    GameObject[] enemies;

    public Portal portal;

    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public bool roomCleared = false;

    int numWaves = 0;

    static bool c = false;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnFromAllPoints();
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            SpawnPlayer();
        }
        else
        {
            Player.ResetPosition();
        }
        //numWaves--;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0 && numWaves > 0)
        {
            // spawn more enemy
            SpawnFromAllPoints();
            numWaves--;
        }else if(enemies.Length == 0 && numWaves == 0)
        {
            roomCleared = true;
            portal.SetInteractable(true);
            setRoomCleared();
            // activate portal
        }
        /*else // needed this other wise it would go into if for some reason
        {
            roomCleared = false;
            portal.SetInteractable(false);
        }*/

    }

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
    }

    void SpawnFromAllPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randEnemy], spawnPoints[i].position, Quaternion.identity);
        }
        int stageCleared = Player.stageCleared;
        for (int i = 0; i < stageCleared; i++)
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randPos = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randPos].position, Quaternion.identity);
        }
    }

    public static bool getIsCleared()
    {
        return c;
    }

    void setRoomCleared()
    {
        c = true;
    }
}
