using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    string timerText;
    TextMeshPro timerTextUI;
    int[] timerMinutes = {5, 7};
    string extra0;
    public bool stopTimer = false;
    public static int minutes, seconds;

    void Awake() {
        timerTextUI = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    public void Reset() {
        timerText = "";
    }

    void Update() {
        if (seconds < 10) extra0 = "0";
        else extra0 = "";
        timerText = minutes + ":" + extra0 + seconds;
        timerTextUI.text = timerText;
    }

    public IEnumerator ReduceTimer() {
        while (minutes > 0 || seconds > 0) {
            seconds -= 1;
            if (seconds < 0) {
                minutes -= 1;
                seconds = 59;
            }
            if (stopTimer) break;
            else yield return new WaitForSeconds(1);
        }
        if (!stopTimer) {
            FindObjectOfType<Player>().Death("time");
        }
        yield return null;
    }

    public void StopTimer() {
        stopTimer = true;
    }

    public void SetTimer(int level) {
        transform.position = new Vector3(8f, 4.7f, 0);

        minutes = timerMinutes[level-1];
        seconds = 0;
    }
}
