using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DeathMenu : PauseMenu
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
        levelLoader.LoadNextLevel();
    }

    public static void SetDeathMenuActive()
    {
        staticDeathMenu.SetActive(true);
        survivedText = GameObject.Find("/Canvas(Clone)/DeathMenu/SurvivedText").GetComponent<TextMeshProUGUI>();
        survivedText.text = "You survived for " + Player.GetStagedCleared().ToString() + " stage(s)";
    }
}
