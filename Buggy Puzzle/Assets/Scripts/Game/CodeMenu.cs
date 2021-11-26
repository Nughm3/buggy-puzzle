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
    string[] codeArray = new string[4];
    public static bool codeIsReversed = false;
    public static bool menuOpened;

    void Awake()
    {
        UpdateSelection();
        foreach (int num in GameManager.code) {
            Debug.Log(num);
        }
    }

    public void Show() {
        gameObject.SetActive(true);
        transform.position = new Vector3(FindObjectOfType<Camera>().myPos.x, FindObjectOfType<Camera>().myPos.y, 0);
        menuOpened = true;
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
        gameObject.SetActive(false);
        menuOpened = false;
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
            if (codeInputIndex == 4) {
                int loopIndex = 0;
                foreach (char temp in codeInput) {
                    codeArray[loopIndex] = temp.ToString();
                    loopIndex += 1;
                }
                int correctDigits = 0;
                for (int i = 0; i < 4; i++) {
                    if (!codeIsReversed) {
                        if (codeArray[i] == GameManager.code[i].ToString()) correctDigits += 1;
                    }
                    else {
                        if (codeArray[i] == GameManager.code[3-i].ToString()) correctDigits += 1;
                    }
                }
                if (correctDigits >= 4) {
                    Debug.Log("yay");
                    FindObjectOfType<Timer>().StopTimer();
                }
                else {
                    if (Timer.minutes < 1) {
                        Timer.seconds = 0;
                        Unconfirm();
                    }
                    else Timer.minutes -= 1;
                }
            }
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
