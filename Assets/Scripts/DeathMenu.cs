using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;
    static GameObject staticDeathMenu;
    //static string survivedText;
    static TextMeshProUGUI survivedText;

    void Start()
    {
        staticDeathMenu = deathMenu;
    }

    public void Restart()
    {
        LevelLoader levelLoader = GameObject.Find("/Canvas(Clone)/").GetComponent<LevelLoader>();

        Destroy(GameObject.Find("/Player(Clone)/"));

        staticDeathMenu.SetActive(false);
        levelLoader.LoadNextLevel();
    }

    public static void Enable()
    {
        staticDeathMenu.SetActive(true);
        survivedText = GameObject.Find("/Canvas(Clone)/DeathMenu/SurvivedText").GetComponent<TextMeshProUGUI>();
        survivedText.text = "You survived for " + Player.GetStagedCleared().ToString() + " stage(s)";
    }

    public static void Disable()
    {
        staticDeathMenu.SetActive(false);
        //survivedText = GameObject.Find("/Canvas(Clone)/DeathMenu/SurvivedText").GetComponent<TextMeshProUGUI>();
        //survivedText.text = "You survived for " + Player.GetStagedCleared().ToString() + " stage(s)";
    }

    public void ToMenu()
    {
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        LevelLoader levelLoader = GameObject.Find("/Canvas(Clone)/").GetComponent<LevelLoader>();
        levelLoader.LoadMainMenu();
    }
}
