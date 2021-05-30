using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : PauseMenu
{

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
