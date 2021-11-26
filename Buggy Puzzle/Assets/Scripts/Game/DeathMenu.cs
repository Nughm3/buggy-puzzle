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

    void Awake()
    {
        UpdateSelection();
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

    public void Show() {
        gameObject.SetActive(true);
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
                Retry();
                return;
            case 1:
                Quit();
                return;
        }
    }

    void Retry()
    {
        
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
        if (Input.GetKeyDown(KeyCode.UpArrow)) SelectUp();
        else if (Input.GetKeyDown(KeyCode.DownArrow)) SelectDown();
        if (Input.GetKeyDown(KeyCode.Z)) Confirm();
        UpdateSelection();
    }
}
