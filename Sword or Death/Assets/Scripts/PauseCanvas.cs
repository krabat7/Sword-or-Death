using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvas : MonoBehaviour
{
    public void ContinueHandler()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartHandler()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ExitHandler()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
