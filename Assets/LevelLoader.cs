using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    void Update()
    {
        /*if (Input.GetButtonDown("Jump"))
        {
            LoadNextLevel();
        }*/
    }

    public void LoadNextLevel()
    {
        //  StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(0);
    }
}