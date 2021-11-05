using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public TextMeshPro continueButton;
    public TextMeshPro quitButton;

    public GameObject menu;
    public GameObject game;

    int selectedOption = 0;
    int options = 2;

    void Awake()
    {
        UpdateSelection();
    }

    public void UpdateSelection()
    {
        TextMeshPro[] buttons = { continueButton, quitButton };

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
                Continue();
                return;
            case 1:
                Quit();
                return;
        }
    }

    void Unconfirm()
    {
        PauseManager.isPaused = false;
    }

    void Continue()
    {
        PauseManager.isPaused = false;
    }

    void Quit()
    {
        selectedOption = 0;
        PauseManager.isPaused = false;
        Time.timeScale = 1f;
        menu.SetActive(true);
        game.SetActive(false);
        gameObject.SetActive(false);

        FindObjectOfType<EnemySpawner>().RemoveEnemies();
        FindObjectOfType<Player>().Reset();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) SelectUp();
        else if (Input.GetKeyDown(KeyCode.DownArrow)) SelectDown();
        if (Input.GetKeyDown(KeyCode.Z)) Confirm();
        else if (Input.GetKeyDown(KeyCode.X)) Unconfirm();
        UpdateSelection();
    }
}
