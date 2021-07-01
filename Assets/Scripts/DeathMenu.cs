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
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public static void SetDeathMenuActive()
    {
        staticDeathMenu.SetActive(true);
    }
}
