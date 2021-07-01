using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : PauseMenu
{
    public GameObject deathMenu;
    static GameObject staticDeathMenu;

    void Start()
    {
        staticDeathMenu = deathMenu;
    }

    public void Restart()
    {
        //Destroy(GameObject.Find("/Canvas(Clone)/"));
        //Destroy(GameObject.Find("/Player(Clone)"));

        LevelLoader levelLoader = GameObject.Find("/Canvas(Clone)/").GetComponent<LevelLoader>();
        levelLoader.LoadNextLevel();
    }

    public static void SetDeathMenuActive()
    {
        staticDeathMenu.SetActive(true);
    }
}
