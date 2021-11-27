using UnityEngine;
using UnityEngine.Audio;

public class BugManager : MonoBehaviour
{
    public static string bug;
    public static int damageMultiplier;
    public static int speedMultiplier;

    void Update() {
        if (bug == "speed") {
            damageMultiplier = 2;
            speedMultiplier = 2;
        }
    }

    public void Reset() {
        bug = "none";
        damageMultiplier = 1;
        speedMultiplier = 1;
    }

    void Awake() {
        Reset();
    }
}