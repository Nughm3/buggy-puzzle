using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;
    public static float defaultTimeScale;

    void Update()
    {
        UpdateSelection();
        if (Input.GetKeyDown(KeyCode.Escape) && !Camera.inScroll && !CodeMenu.menuOpened && !StartTimer.timerRunning && !Camera.inScroll && !DeathMenu.menuOpened && !WinMenu.menuOpened && !BugMenu.menuOpened) Pause();
        pauseMenu.transform.position = new Vector3(FindObjectOfType<Camera>().myPos.x, FindObjectOfType<Camera>().myPos.y, 0);

        if (BugManager.bug == "Time") defaultTimeScale = 0.75f;
        else defaultTimeScale = 1f;
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
            if (!BugMenu.menuOpened) Time.timeScale = defaultTimeScale;   
        }
    }

    void Pause()
    {
        isPaused = !isPaused;
    }
}
