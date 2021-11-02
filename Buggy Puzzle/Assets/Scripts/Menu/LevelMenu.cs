using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    
    public TextMeshPro levelButton1;
    public TextMeshPro backButton;

    public GameObject mainMenu;
    public GameObject menu;
    public GameObject game;

    int selectedOption = 0;
    int options = 2;

    void Awake() {
        UpdateSelection();
    }

    public void UpdateSelection() {
        TextMeshPro[] buttons = {levelButton1, backButton};
        foreach (TextMeshPro button in buttons) {button.color = Color.black;}
        buttons[selectedOption].color = Color.green;
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
                StartCoroutine(FindObjectOfType<GameManager>().Play(selectedOption + 1));
                return;
            case 1:
                Back();
                return;
        }
    }

    void Back() {
        selectedOption = 0;
        Unconfirm();
    }

    void Unconfirm() {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void Update() {
        if (MenuManager.allowInput) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) SelectUp();
            else if (Input.GetKeyDown(KeyCode.DownArrow)) SelectDown();
            if (Input.GetKeyDown(KeyCode.Z)) Confirm();
            else if (Input.GetKeyDown(KeyCode.X)) Unconfirm();
        }
        UpdateSelection();
    }
}