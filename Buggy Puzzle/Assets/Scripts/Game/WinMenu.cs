using UnityEngine;
using System.Collections;
using TMPro;

public class WinMenu : MonoBehaviour
{
    public TextMeshPro nextButton;
    public TextMeshPro retryButton;
    public TextMeshPro quitButton;
    public TextMeshPro healthText;
    public TextMeshPro timeText;
    public GameObject pauseMenu;
    public GameObject bugManager;

    int selectedOption = 0;
    int options = 3;
    public static bool menuOpened;
    public static int level;
    bool allowInput = true;

    void Awake()
    {
        UpdateSelection();
    }

    void OnEnable() {
        menuOpened = true;
        FindObjectOfType<AudioManager>().PlaySound("victory");
        BugMenu.menuOpened = false;
        BugManager.bug = "None";
        bugManager.SetActive(false);
        string extra0 = "";
        if (60 - Timer.seconds < 10) extra0 = "0";
        healthText.text = Player.health.ToString() + "/" + Player.maxHealth.ToString();
        timeText.text = ((Timer.timerMinutes[level-1] - 1) - Timer.minutes).ToString() + ":" + extra0 + (60 - Timer.seconds).ToString();

        if (FindObjectOfType<PlayerData>().level < level + 1) FindObjectOfType<PlayerData>().level = level + 1;
        FindObjectOfType<PlayerData>().Save();
    }

    public void UpdateSelection()
    {
        TextMeshPro[] buttons = { nextButton, retryButton, quitButton };

        foreach (TextMeshPro button in buttons)
        {
            button.color = Color.black;
            button.fontStyle = FontStyles.Normal;
        }
        buttons[selectedOption].color = Color.green;
        buttons[selectedOption].fontStyle = FontStyles.Bold;
    }

    void SelectUp()
    {
        if (selectedOption > 0) selectedOption--;
        else selectedOption = options - 1;
    }

    void SelectDown()
    {
        if (selectedOption < options - 1) selectedOption++;
        else selectedOption = 0;
    }

    void Confirm()
    {
        switch (selectedOption)
        {
            case 0:
                StartCoroutine(Next());
                return;
            case 1:
                StartCoroutine(Retry());
                return;
            case 2:
                Quit();
                return;
        }
    }

    IEnumerator Next() {
        if (level == 10) {
            Quit();
            yield return null;
        }
        else {
            allowInput = false;
            yield return StartCoroutine(FindObjectOfType<Fade>().FadeOut());
            pauseMenu.SetActive(true);
            FindObjectOfType<PauseMenu>().Quit();
            FindObjectOfType<Fade>().fade.color = new Color(0, 0, 0, 1);
            FindObjectOfType<GameManager>().Retry(level + 1);
            menuOpened = false;
            allowInput = true;
            gameObject.SetActive(false);
        }
        
    }

    IEnumerator Retry()
    {
        allowInput = false;
        yield return StartCoroutine(FindObjectOfType<Fade>().FadeOut());
        selectedOption = 0;
        pauseMenu.SetActive(true);
        FindObjectOfType<PauseMenu>().Quit();
        FindObjectOfType<Fade>().fade.color = new Color(0, 0, 0, 1);
        FindObjectOfType<GameManager>().Retry(level);
        menuOpened = false;
        allowInput = true;
        gameObject.SetActive(false);
    }

    void Quit()
    {
        selectedOption = 0;
        pauseMenu.SetActive(true);
        FindObjectOfType<PauseMenu>().Quit();
        menuOpened = false;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (allowInput) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) SelectUp();
            else if (Input.GetKeyDown(KeyCode.DownArrow)) SelectDown();
            if (Input.GetKeyDown(KeyCode.Z)) Confirm();
            UpdateSelection();
        }
        transform.position = new Vector3(FindObjectOfType<Camera>().transform.position.x, FindObjectOfType<Camera>().transform.position.y, 0);
    }
}
