using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int[,] level1 = new int[11, 20] {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };

    int[,] level2 = new int[11, 20] {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };

    float posx;
    float posy;
    float tileSize = 0.8f;
    public GameObject enemyPrefab;
    Vector2 spawnPos;
    List<GameObject> currentEnemies = new List<GameObject>();

    public IEnumerator SpawnEnemies(int level) {
        int[,] currentLevel = new int[11, 20];
        if (level == 1) currentLevel = level1;
        if (level == 2) currentLevel = level2;

        for (int y = 0; y < currentLevel.GetLength(0); y += 1) {
            for (int x = 0; x < currentLevel.GetLength(1); x += 1) {
                if (currentLevel[y, x] == 1) {
                    posx = -7.6f + (x * tileSize);
                    posy = 4 - (y * tileSize);
                    spawnPos = new Vector2(posx, posy);
                    currentEnemies.Add(Instantiate(enemyPrefab, spawnPos, transform.rotation));
                }
            }
        }
        yield return new WaitForFixedUpdate();
    }

    public void RemoveEnemies() {
        foreach (GameObject enemy in currentEnemies) {
            Destroy(enemy);
        }
        currentEnemies.Clear();
    }
}
