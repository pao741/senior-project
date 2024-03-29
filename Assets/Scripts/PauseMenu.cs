using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ToMenu()
    {
        Destroy(GameObject.Find("/Player(Clone)/"));
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        LevelLoader levelLoader = GameObject.Find("/Canvas(Clone)/").GetComponent<LevelLoader>();
        levelLoader.LoadMainMenu();
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quit");
        Application.Quit();
    }
}
