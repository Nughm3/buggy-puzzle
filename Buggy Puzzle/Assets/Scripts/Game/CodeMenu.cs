using UnityEngine;
using System.Collections;
using TMPro;

public class CodeMenu : MonoBehaviour
{
    public TextMeshPro[] buttonsList;
    public TextMeshPro codeText;
    public GameObject tickOff;
    public GameObject tickOn;
    public GameObject winMenu;

    int selectedOption = 0;
    int options = 13;
    string codeInput;
    int codeInputIndex = 0;
    string[] codeArray = new string[4];
    public static int codeModifier = 0;
    int[] ascendingCode;
    public static bool menuOpened;

    void Awake()
    {
        UpdateSelection();
    }

    void OnEnable() {
        codeInput = "";
        foreach (int i in GameManager.code) codeInput += "_";
        foreach (int i in GameManager.code) Debug.Log(i);
        Debug.Log(codeModifier);
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

        if (selectedOption == 9) {
            tickOn.SetActive(true);
            tickOff.SetActive(false);
        }
        else {
            tickOff.SetActive(true);
            tickOn.SetActive(false);
        }
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
        gameObject.SetActive(false);
        menuOpened = false;
    }

    void ChooseButton(int number) {
        if ((selectedOption < 9 || selectedOption == 10) && codeInputIndex < GameManager.code.Length) {
            var tempCode = codeInput;
            tempCode = tempCode.Remove(codeInputIndex, 1);
            if (selectedOption != 10) tempCode = tempCode.Insert(codeInputIndex, (selectedOption + 1).ToString());
            else tempCode = tempCode.Insert(codeInputIndex, "0");
            codeInput = tempCode;
            codeInputIndex += 1;
        }
        if (selectedOption == 9) {
            if (codeInputIndex == GameManager.code.Length) {
                int loopIndex = 0;
                foreach (char temp in codeInput) {
                    codeArray[loopIndex] = temp.ToString();
                    loopIndex += 1;
                }
                int correctDigits = 0;
                if (codeModifier >= 2) {
                    ascendingCode = new int[] {GameManager.code[0],GameManager.code[1],GameManager.code[2],GameManager.code[3]};
                    for (int j = 0; j <= ascendingCode.Length - 2; j++) {
                        for (int i = 0; i <= ascendingCode.Length - 2; i++) {
                            if (ascendingCode[i] > ascendingCode[i + 1]) {
                                int temp = ascendingCode[i + 1];
                                ascendingCode[i + 1] = ascendingCode[i];
                                ascendingCode[i] = temp;
                            }
                        }
                    }
                }
                for (int i = 0; i < GameManager.code.Length; i++) {
                    if (codeModifier == 0) {
                        if (codeArray[i] == GameManager.code[i].ToString()) correctDigits += 1;
                    }
                    else if (codeModifier == 1) {
                        if (codeArray[i] == GameManager.code[(GameManager.code.Length - 1)-i].ToString()) correctDigits += 1;
                    }
                    else if (codeModifier == 2) {
                        if (codeArray[i] == ascendingCode[i].ToString()) correctDigits += 1;
                    }
                    else if (codeModifier == 3) {
                        if (codeArray[i] == ascendingCode[(GameManager.code.Length - 1)-i].ToString()) correctDigits += 1;
                    }
                }
                Debug.Log(correctDigits);
                if (correctDigits >= GameManager.code.Length) {
                    winMenu.SetActive(true);
                    Unconfirm();
                    Player.allowMove = false;
                    FindObjectOfType<Timer>().StopTimer();
                }
                else {
                    FindObjectOfType<AudioManager>().PlaySound("fail");
                    FindObjectOfType<Timer>().Fail();
                    if (Timer.minutes < 1) {
                        Timer.seconds = 0;
                        Unconfirm();
                    }
                    else Timer.minutes -= 1;
                    Unconfirm();
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
        if (!BugMenu.menuOpened) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) SelectLeft();
            else if (Input.GetKeyDown(KeyCode.RightArrow)) SelectRight();
            else if (Input.GetKeyDown(KeyCode.UpArrow)) SelectUp();
            else if (Input.GetKeyDown(KeyCode.DownArrow)) SelectDown();
            if (Input.GetKeyDown(KeyCode.Z)) Confirm();
            else if (Input.GetKeyDown(KeyCode.X)) Unconfirm();
            UpdateSelection();
        }
    }
}
