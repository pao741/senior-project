using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject playerGameObject;

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
        //playerGameObject = GameObject.Find("Player(Clone)");
        //DontDestroyOnLoad(playerGameObject)
        StartCoroutine(LoadLevel(3));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(0);
    }

    //public static void LoadLevel(itn )
}
