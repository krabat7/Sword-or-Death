using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScrene : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;
    private void Start()
    {
        deathSound.Play();
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
