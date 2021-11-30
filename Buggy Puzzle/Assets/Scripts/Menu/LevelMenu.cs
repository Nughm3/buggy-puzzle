using UnityEngine;
using TMPro;

public class LevelMenu : MonoBehaviour
{

    public TextMeshPro levelButton1;
    public TextMeshPro levelButton2;
    public TextMeshPro levelButton3;
    public TextMeshPro levelButton4;
    public TextMeshPro levelButton5;
    public TextMeshPro levelButton6;
    public TextMeshPro levelButton7;
    public TextMeshPro levelButton8;
    public TextMeshPro levelButton9;
    public TextMeshPro levelButton10;
    public TextMeshPro backButton;

    public SpriteRenderer[] buttons;

    public GameObject mainMenu;
    public GameObject menu;
    public GameObject game;

    public static int selectedOption = 0;
    int options = 11;

    void Awake()
    {
        UpdateSelection();
        ColorButtons();
    }

    void OnEnable() {
        UpdateSelection();
    }

    public void UpdateSelection()
    {
        TextMeshPro[] buttons = { levelButton1, levelButton2, levelButton3, levelButton4, levelButton5, levelButton6, levelButton7, levelButton8, levelButton9, levelButton10, backButton };
        foreach (TextMeshPro button in buttons) { button.color = Color.black; }
        buttons[selectedOption].color = Color.green;
    }

    void SelectUp()
    {
        if (selectedOption == 10) selectedOption = 7;
        else if (selectedOption > 4) selectedOption -= 5;
    }

    void SelectDown()
    {
        if (selectedOption > 4 && selectedOption != 10) selectedOption = 10;
        else if (selectedOption < 5) selectedOption += 5;
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

    void Confirm()
    {
        if (selectedOption == 10) Back();
        else if (selectedOption + 1 <= FindObjectOfType<PlayerData>().level) StartCoroutine(FindObjectOfType<GameManager>().Play(selectedOption + 1));
        else FindObjectOfType<AudioManager>().PlaySound("fail");
    }

    public void Back()
    {
        selectedOption = 0;
        Unconfirm();
    }

    void Unconfirm()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (MenuManager.allowInput)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) SelectUp();
            else if (Input.GetKeyDown(KeyCode.DownArrow)) SelectDown();
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) SelectLeft();
            else if (Input.GetKeyDown(KeyCode.RightArrow)) SelectRight();
            if (Input.GetKeyDown(KeyCode.Z)) Confirm();
            else if (Input.GetKeyDown(KeyCode.X)) Unconfirm();
        }
        UpdateSelection();
        ColorButtons();
    }

    void ColorButtons() {
        int level = FindObjectOfType<PlayerData>().level;
        for (int i = 0; i < level-1; i++) buttons[i].color = new Color(0,0.8f,0);
        if (level <= 10) buttons[level-1].color = new Color(1,1,0);
        for (int i = level; i < 10; i++) buttons[i].color = new Color(1,0,0);
    }
}