using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        _audioSource.Stop();
        audioSource.Play();
    }
    public void ExitHandler()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
