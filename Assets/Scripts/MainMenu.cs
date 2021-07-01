using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    LevelLoader levelLoader;

    void Start()
    {
        levelLoader = new LevelLoader();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(Random.Range(1, 5));
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
