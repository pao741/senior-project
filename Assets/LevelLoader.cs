using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void StartLevel()
    {
    }

    void Update()
    {
        /*if (Input.GetButtonDown("Jump"))
        {
            LoadNextLevel();
        }*/
    }

    public void LoadNextLevel()
    {
        int nextStage = Random.Range(1, 5);
        StartCoroutine(LoadLevel(nextStage));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetBool("Starting", true);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        //yield return new WaitForSeconds(transitionTime);

        transition.SetBool("Starting", false);
    }
}
