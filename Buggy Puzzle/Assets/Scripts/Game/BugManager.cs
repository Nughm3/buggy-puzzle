using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class BugManager : MonoBehaviour
{
    public GameObject bugMenu;

    public static string bug;
    public static int damageMultiplier;
    public static int speedMultiplier;
    string[] bugList = {"Speed", "Scary", "Sneak"};

    void Update() {
        if (bug == "Speed") {
            damageMultiplier = 2;
            speedMultiplier = 2;
        }
    }

    void OnEnable() {
        Reset();
    }

    public IEnumerator BugTimer() {
        yield return new WaitForSeconds(10);
            if (Player.isAlive) {
                string currentBug = bug;
                Reset();
                while (true) {
                    bug = bugList[Random.Range(0, bugList.Length)];
                    if (bug != currentBug) break;
                }
                if (bug == "Sneak") StartCoroutine(FindObjectOfType<Player>().SneakPulse());
                bugMenu.SetActive(true);
            }
        while (true) {
            yield return new WaitForSeconds(15);
            if (Player.isAlive && !WinMenu.menuOpened && !DeathMenu.menuOpened) {
                string currentBug = bug;
                Reset();
                while (true) {
                    bug = bugList[Random.Range(0, bugList.Length)];
                    if (bug != currentBug) break;
                }
                if (bug == "Sneak") StartCoroutine(FindObjectOfType<Player>().SneakPulse());
                bugMenu.SetActive(true);
            }
        }
    }

    public void Reset() {
        bug = "None";
        damageMultiplier = 1;
        speedMultiplier = 1;
    }

    void Awake() {
        Reset();
    }
}