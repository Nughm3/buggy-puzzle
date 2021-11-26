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
        if (Input.GetKeyDown(KeyCode.Escape) && !Camera.inScroll && !CodeMenu.menuOpened && !StartTimer.timerRunning && !Camera.inScroll && !DeathMenu.menuOpened) Pause();
        Debug.Log(Camera.inScroll + " " + CodeMenu.menuOpened + " " + StartTimer.timerRunning + " " + Camera.inScroll + " " + DeathMenu.menuOpened);
        pauseMenu.transform.position = new Vector3(FindObjectOfType<Camera>().myPos.x, FindObjectOfType<Camera>().myPos.y, 0);
    }

    void UpdateSelection()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            PauseMenu.menuOpened = true;
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            PauseMenu.menuOpened = false;
            Time.timeScale = 1f;
        }
    }

    void Pause()
    {
        isPaused = !isPaused;
    }
}
