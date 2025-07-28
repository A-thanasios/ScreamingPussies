using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void PlayTutorial()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void PlayLvl1()
    {
        SceneManager.LoadSceneAsync(2);
    }

}
