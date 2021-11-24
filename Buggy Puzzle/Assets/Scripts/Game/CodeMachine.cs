using UnityEngine;

public class CodeMachine : MonoBehaviour
{
    Vector2[] spawnPoints = {new Vector2(17.2f,-9.6f), new Vector2(-6,0)};

    public void Spawn(int level) {
        transform.position = new Vector3(spawnPoints[level - 1][0],spawnPoints[level - 1][1], 0);
    }
}
