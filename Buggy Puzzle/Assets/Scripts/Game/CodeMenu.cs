using UnityEngine;
using System.Collections;
using TMPro;

public class CodeMenu : MonoBehaviour
{
    public TextMeshPro[] buttonsList;
    public TextMeshPro codeText;

    int selectedOption = 0;
    int options = 13;
    string codeInput = "____";
    int codeInputIndex = 0;

    void Awake()
    {
        UpdateSelection();
    }

    void OnEnable() {
        Player.allowMove = false;
    }

    public void UpdateSelection()
    {
        codeText.text = codeInput;

        foreach (TextMeshPro button in buttonsList)
        {
            button.color = Color.black;
            button.fontStyle = FontStyles.Normal;
        }
        buttonsList[selectedOption].color = Color.green;
        buttonsList[selectedOption].fontStyle = FontStyles.Bold;
    }

    void SelectLeft()
    {
        if (selectedOption > 0) selectedOption--;
        else selectedOption = options - 1;
    }

    void SelectRight()
    {
        if (selectedOption < options - 1) selectedOption++;
        else selectedOption = 0;
    }

    void SelectUp() {
        if (selectedOption > 2) {
            if (selectedOption == 12) selectedOption = 10;
            else selectedOption -= 3;
        }
        else selectedOption = 12;
    }

    void SelectDown() {
        if (selectedOption < 9) selectedOption += 3;
        else if (selectedOption < 12) selectedOption = 12;
        else selectedOption = 1;
    }

    void Confirm()
    {
        if (selectedOption < 12) ChooseButton(selectedOption);
        else Unconfirm();
    }

    void Unconfirm()
    {
        selectedOption = 0;
        codeInputIndex = 0;
        codeInput = "____";
        if (!Player.inMove) Player.allowMove = true;
        gameObject.SetActive(false);
    }

    void ChooseButton(int number) {
        if ((selectedOption < 9 || selectedOption == 10) && codeInputIndex < 4) {
            var tempCode = codeInput;
            tempCode = tempCode.Remove(codeInputIndex, 1);
            if (selectedOption != 10) tempCode = tempCode.Insert(codeInputIndex, (selectedOption + 1).ToString());
            else tempCode = tempCode.Insert(codeInputIndex, "0");
            codeInput = tempCode;
            codeInputIndex += 1;
        }
        if (selectedOption == 9) {
            Debug.Log("Confirm");
        }
        if (selectedOption == 11 && codeInputIndex > 0) {
            var tempCode = codeInput;
            tempCode = tempCode.Remove(codeInputIndex - 1, 1);
            tempCode = tempCode.Insert(codeInputIndex - 1, "_");
            codeInput = tempCode;
            codeInputIndex -= 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SelectLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow)) SelectRight();
        else if (Input.GetKeyDown(KeyCode.UpArrow)) SelectUp();
        else if (Input.GetKeyDown(KeyCode.DownArrow)) SelectDown();
        if (Input.GetKeyDown(KeyCode.Z)) Confirm();
        else if (Input.GetKeyDown(KeyCode.X)) Unconfirm();
        UpdateSelection();
    }
}
