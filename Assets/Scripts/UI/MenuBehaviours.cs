using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviours : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
        Time.timeScale = 1;
    }

    public void Resume(GameObject canvas)
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
