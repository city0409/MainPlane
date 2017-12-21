using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot paused, unpaused;
    [SerializeField]
    private CanvasGroup pauseGroup;
    private bool isPaused = true;

    private void Start()
    {
        Pause();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseGroup.alpha = isPaused ? 1 : 0;
        pauseGroup.interactable = isPaused ? true : false;
        pauseGroup.blocksRaycasts = isPaused ? true : false;
        Lowpass();
    }

    private void Lowpass()
    {
        if (Time.timeScale == 0)
        {
            paused.TransitionTo(0.01f);
        }
        else
        {
            unpaused.TransitionTo(0.01f);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 
    }

    private void DisPlay() { }
}
