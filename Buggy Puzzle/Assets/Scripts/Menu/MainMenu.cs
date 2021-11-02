using UnityEngine;
using System.IO;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshPro playButton;
    public TextMeshPro quitButton;

    public TextMeshProUGUI quitYesButton;
    public TextMeshProUGUI quitNoButton;

    public GameObject levelMenu;
    public GameObject quitConfirm;

    int selectedOption = 0;
    int quitConfirmSelectedOption = 1;
    int options = 2;

    bool quitConfirmOpened = false;

    void Awake() {
        UpdateSelection();
    }

    public void UpdateSelection() {
        TextMeshPro[] buttons = {playButton, quitButton};

        foreach (TextMeshPro button in buttons) {
            button.color = Color.black;
            button.fontStyle = FontStyles.Normal;
        }
        buttons[selectedOption].color = Color.green;
        buttons[selectedOption].fontStyle = FontStyles.Bold;

        TextMeshProUGUI[] quitButtons = {quitYesButton, quitNoButton};

        foreach (TextMeshProUGUI button in quitButtons) {
            button.color = Color.black;
            button.fontStyle = FontStyles.Normal;
        }
        quitButtons[selectedOption].color = Color.green;
        quitButtons[selectedOption].fontStyle = FontStyles.Bold;
    }

    void SelectUp() {
        if (selectedOption > 0) selectedOption--;
        else selectedOption = options - 1;
    }

    void SelectDown() {
        if (selectedOption < options - 1) selectedOption++;
        else selectedOption = 0;
    }

    void Confirm() {
        switch(selectedOption) {
            case 0:
                Play();
                return;
            case 1:
                Quit();
                return;
        }
    }

    void Play() {
        levelMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void Quit() {
        quitConfirmSelectedOption = 1;
        quitConfirmOpened = true;
    }

    void QuitConfirm() {
        if (Input.GetKeyDown(KeyCode.X)) quitConfirmOpened = false;

        if (Input.GetKeyDown(KeyCode.Z) && quitConfirmSelectedOption == 0) Application.Quit();
        if (Input.GetKeyDown(KeyCode.Z) && quitConfirmSelectedOption == 1) quitConfirmOpened = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
            if (quitConfirmSelectedOption == 0) quitConfirmSelectedOption = 1;
            else quitConfirmSelectedOption = 0;
        }
        quitConfirm.SetActive(true);
    }

    void Update() {
        if (!quitConfirmOpened) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) SelectUp();
            else if (Input.GetKeyDown(KeyCode.DownArrow)) SelectDown();
            if (Input.GetKeyDown(KeyCode.Z)) Confirm();
            quitConfirm.SetActive(false);
        }
        else QuitConfirm();

        UpdateSelection();
    }
}