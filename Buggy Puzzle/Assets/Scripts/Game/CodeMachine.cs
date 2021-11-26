using UnityEngine;

public class CodeMachine : MonoBehaviour
{
    public static bool inPlayerRange = false;
    Vector2[] spawnPoints = {new Vector2(17.2f,-9.6f), new Vector2(16.4f,9.6f)};

    public void Spawn(int level) {
        transform.position = new Vector3(spawnPoints[level - 1][0],spawnPoints[level - 1][1], 0);
    }

    void Update() {
        if (Player.isAlive) {
            if (Vector3.Distance(transform.position, FindObjectOfType<Player>().transform.position) < 2f) inPlayerRange = true;
            else inPlayerRange = false;
        }
    }
}