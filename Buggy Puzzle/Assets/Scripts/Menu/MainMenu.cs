using UnityEngine;
using System.Collections;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public TextMeshPro playButton;
    public TextMeshPro quitButton;

    public TextMeshPro quitYesButton;
    public TextMeshPro quitNoButton;

    public GameObject levelMenu;
    public GameObject quitConfirm;

    public TextMeshPro creditsText;

    int selectedOption = 0;
    int quitConfirmSelectedOption = 1;
    int options = 2;

    bool quitConfirmOpened = false;

    void Awake()
    {
        UpdateSelection();
    }

    void OnEnable() {
        creditsText.text = "By Nughm3 and AdminTroller";
        StartCoroutine(BuggyCredit());
    }

    IEnumerator BuggyCredit() {
        yield return new WaitForSeconds(Random.Range(4f,8f));
        creditsText.text = "sprites by mi_gusta";
        yield return new WaitForSeconds(0.69f);
        creditsText.text = "By Nughm3 and AdminTroller";
        StartCoroutine(BuggyCredit());
    }

    public void UpdateSelection()
    {
        if (!quitConfirmOpened)
        {
            TextMeshPro[] buttons = { playButton, quitButton };
            foreach (TextMeshPro button in buttons) { button.color = Color.black; }
            buttons[selectedOption].color = Color.green;
        }
        else
        {
            TextMeshPro[] quitButtons = { quitYesButton, quitNoButton };
            foreach (TextMeshPro button in quitButtons) { button.color = Color.black; }
            quitButtons[quitConfirmSelectedOption].color = Color.green;
        }
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
                Play();
                return;
            case 1:
                Quit();
                return;
        }
    }

    void Play()
    {
        levelMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void Quit()
    {
        quitConfirmSelectedOption = 1;
        quitConfirmOpened = true;
    }

    void QuitConfirm()
    {
        if (Input.GetKeyDown(KeyCode.X)) quitConfirmOpened = false;

        if (Input.GetKeyDown(KeyCode.Z) && quitConfirmSelectedOption == 0) Application.Quit();
        if (Input.GetKeyDown(KeyCode.Z) && quitConfirmSelectedOption == 1) quitConfirmOpened = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (quitConfirmSelectedOption == 0) quitConfirmSelectedOption = 1;
            else quitConfirmSelectedOption = 0;
        }
        quitConfirm.SetActive(true);
    }

    void Update()
    {
        if (MenuManager.allowInput)
        {
            if (!quitConfirmOpened)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow)) SelectUp();
                else if (Input.GetKeyDown(KeyCode.DownArrow)) SelectDown();
                if (Input.GetKeyDown(KeyCode.Z)) Confirm();
                quitConfirm.SetActive(false);
            }
            else QuitConfirm();
        }

        UpdateSelection();
    }
}