using UnityEngine;
using System.Collections;
using TMPro;

public class DeathMenu : MonoBehaviour
{
    public TextMeshPro retryButton;
    public TextMeshPro quitButton;
    public GameObject pauseMenu;

    int selectedOption = 0;
    int options = 2;
    public static bool menuOpened;
    public static int level;
    bool allowInput = true;

    void Awake()
    {
        UpdateSelection();
    }

    void OnEnable() {
        menuOpened = true;
    }

    public void UpdateSelection()
    {
        TextMeshPro[] buttons = { retryButton, quitButton };

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
                StartCoroutine(Retry());
                return;
            case 1:
                Quit();
                return;
        }
    }

    IEnumerator Retry()
    {
        allowInput = false;
        yield return StartCoroutine(FindObjectOfType<Fade>().FadeOut());
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
