using UnityEngine;
using System.Collections;

public class BugManager : MonoBehaviour
{
    public GameObject bugMenu;

    public static string bug;
    public static int damageMultiplier;
    public static int speedMultiplier;
    string[] bugList = {"Speed", "Scary", "Sneak", "Time", "Confuse"};

    void Update() {
        if (bug == "Speed") {
            damageMultiplier = 2;
            speedMultiplier = 2;
        }
        else {
            damageMultiplier = 1;
            speedMultiplier = 1;
        }
    }

    void OnEnable() {
        Reset();
    }

    void OnDisable() {
        Reset();
    }

    public IEnumerator BugTimer() {
        yield return new WaitForSeconds(15 * PauseManager.defaultTimeScale);
            if (Player.isAlive && !WinMenu.menuOpened && !DeathMenu.menuOpened) {
                string currentBug = bug;
                Reset();
                while (true) {
                    bug = bugList[Random.Range(0, bugList.Length)];
                    if (bug != currentBug) break;
                }
                if (Player.isAlive && !WinMenu.menuOpened && !DeathMenu.menuOpened) {
                    FindObjectOfType<AudioManager>().PlaySound("bug");
                    if (bug == "Sneak") StartCoroutine(FindObjectOfType<Player>().SneakPulse());
                    bugMenu.SetActive(true);
                }
            }
        while (true) {
            yield return new WaitForSeconds(20 * PauseManager.defaultTimeScale);
            if (Player.isAlive && !WinMenu.menuOpened && !DeathMenu.menuOpened) {
                string currentBug = bug;
                Reset();
                while (true) {
                    bug = bugList[Random.Range(0, bugList.Length)];
                    if (bug != currentBug) break;
                }
                if (Player.isAlive && !WinMenu.menuOpened && !DeathMenu.menuOpened) {
                    FindObjectOfType<AudioManager>().PlaySound("bug");
                    if (bug == "Sneak") StartCoroutine(FindObjectOfType<Player>().SneakPulse());
                    bugMenu.SetActive(true);
                }
            }
        }
    }

    public void Reset() {
        bug = "None";
    }

    void Awake() {
        Reset();
    }
}