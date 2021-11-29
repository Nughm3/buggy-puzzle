using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class StartTimer : MonoBehaviour
{
    public TextMeshPro timerText;
    public TextMeshPro codeText;
    public SpriteRenderer fade;
    public GameObject instruction;
    public GameObject tutorial;
    float waitTime = 0.5f;
    public static bool timerRunning = false;
    bool digitsShown = false;

    public IEnumerator ShowDigits() {
        timerRunning = true;
        if (GameManager.currentLevel == 1) yield return StartCoroutine(ShowTutorial());
        codeText.text = "The code is\n" + GameManager.code.Length + " digits long";
        while (FindObjectOfType<Fade>().GetComponent<SpriteRenderer>().color.a > 0) {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        while (fade.color.a < 0.7f)
        {
            codeText.color += new Color(0, 0, 0, 0.08f);
            fade.color += new Color(0, 0, 0, 0.06f);
            yield return new WaitForFixedUpdate();
        }
        instruction.SetActive(true);
        digitsShown = true;
    }

    IEnumerator ShowTutorial() {
        tutorial.SetActive(true);
        while (!Input.GetKeyDown(KeyCode.Z)) {
            yield return null;
        }
        tutorial.SetActive(false);
    }

    IEnumerator Timer() {
        digitsShown = false;
        instruction.SetActive(false);
        while (fade.color.a > 0 || codeText.color.a > 0)
        {
            codeText.color -= new Color(0, 0, 0, 0.16f);
            fade.color -= new Color(0, 0, 0, 0.12f);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.2f);
        timerText.text = "3";
        FindObjectOfType<AudioManager>().PlaySound("tick");
        yield return new WaitForSeconds(waitTime);
        timerText.text = "2";
        FindObjectOfType<AudioManager>().PlaySound("tick");
        yield return new WaitForSeconds(waitTime);
        timerText.text = "1";
        FindObjectOfType<AudioManager>().PlaySound("tick");
        yield return new WaitForSeconds(waitTime);
        timerText.text = "Start!";
        FindObjectOfType<AudioManager>().PlaySound("tick2");
        Player.allowMove = true;
        FindObjectOfType<Timer>().stopTimer = false;
        StartCoroutine(FindObjectOfType<Timer>().ReduceTimer());
        StartCoroutine(FindObjectOfType<BugManager>().BugTimer());
        yield return new WaitForSeconds(waitTime/2);
        timerText.text = "";
        timerRunning = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && digitsShown) StartCoroutine(Timer());
    }

    void Awake() {
        fade.color = new Color(1, 1, 1, 0f);
        codeText.color = new Color(1, 1, 1, 0f);
    }
}