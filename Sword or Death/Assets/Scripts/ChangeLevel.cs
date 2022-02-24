using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        _audioSource.Stop();
        audioSource.Play();
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
    public void NextLevelHandler()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.buildIndex);
        SceneManager.LoadScene(scene.buildIndex + 1);
        Time.timeScale = 1f;
    }
}
