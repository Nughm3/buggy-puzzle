using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class StartTimer : MonoBehaviour
{
    TextMeshPro startText;
    float waitTime = 0.5f;
    public static bool timerRunning = false;

    public IEnumerator Timer() {
        timerRunning = true;
        startText = gameObject.GetComponent<TextMeshPro>();
        while (FindObjectOfType<Fade>().GetComponent<SpriteRenderer>().color.a > 0) {
            yield return null;
        }
        startText.text = "3";
        yield return new WaitForSeconds(waitTime);
        startText.text = "2";
        yield return new WaitForSeconds(waitTime);
        startText.text = "1";
        yield return new WaitForSeconds(waitTime);
        startText.text = "Start!";
        Player.allowMove = true;
        FindObjectOfType<Timer>().stopTimer = false;
        StartCoroutine(FindObjectOfType<Timer>().ReduceTimer());
        yield return new WaitForSeconds(waitTime/2);
        startText.text = "";
        timerRunning = false;
    }
}