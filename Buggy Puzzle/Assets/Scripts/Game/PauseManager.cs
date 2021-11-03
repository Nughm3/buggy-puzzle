using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;

    void Update()
    {
        UpdateSelection();
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    void UpdateSelection()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void Pause()
    {
        isPaused = !isPaused;
    }
}
