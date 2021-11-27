using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BugMenu : MonoBehaviour
{
    public TextMeshPro[] info;
    public SpriteRenderer fade;
    public GameObject instruction;

    public static bool menuOpened = false;
    bool allowInput = false;
    bool done = false;

    public IEnumerator Show() {
        while (fade.color.a < 0.5f) {
            foreach (TextMeshPro text in info) {text.color += new Color(0, 0, 0, 0.08f);}
            fade.color += new Color(0, 0, 0, 0.04f);
            yield return null;
        }
        for (int i = 0; i < 20; i++) yield return null;
        instruction.SetActive(true);
        allowInput = true;

        while (!done) yield return null;
        done = false;

        instruction.SetActive(false);
        while (fade.color.a > 0) {
            foreach (TextMeshPro text in info) {text.color -= new Color(0, 0, 0, 0.16f);}
            fade.color -= new Color(0, 0, 0, 0.08f);
            yield return null;
        }
        Reset();
        gameObject.SetActive(false);
    }

    void OnEnable() {
        Reset();
        SetText(BugManager.bug);
        menuOpened = true;
        Time.timeScale = 0f;
        StartCoroutine(Show());
    }

    void SetText(string bug) {
        info[0].text = bug;

        if (bug == "Speed") {
            info[1].text = "x2 Speed";
            info[2].text = "x2 Damage Taken";
        }
        if (bug == "Scary") {
            info[1].text = "Enemies run away";
            info[2].text = "50% for NPCs to ignore you";
        }
        if (bug == "Sneak") {
            info[1].text = "Enemies can't see you";
            info[2].text = "Hard to see yourself";
        }

        int random = Random.Range(0,5);
        if (random == 0) info[3].text = "What the-?";
        if (random == 1) info[3].text = "Huh?";
        if (random == 2) info[3].text = "What's happening?";
        if (random == 3) info[3].text = "Woah!";
        if (random == 4) info[3].text = "What's this?";
    }

    public void Reset() {
        allowInput = false;
        foreach (TextMeshPro text in info) {text.color = new Color(text.color[0], text.color[1], text.color[2], 0);}
        fade.color = new Color(1, 1, 1, 0);
        Time.timeScale = 1f;
        menuOpened = false;
        instruction.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && allowInput) done = true;
        transform.position = new Vector3(FindObjectOfType<Camera>().transform.position.x, FindObjectOfType<Camera>().transform.position.y, 0);

        if (WinMenu.menuOpened || DeathMenu.menuOpened) gameObject.SetActive(false);
    }
}