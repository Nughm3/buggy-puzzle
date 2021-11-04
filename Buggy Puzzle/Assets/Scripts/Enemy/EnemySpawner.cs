using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    Vector2 spawnPos = new Vector2(7.6f,0f);

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy() {
        yield return new WaitForSeconds(1f);
        while (true) {
            Instantiate(enemyPrefab, spawnPos, transform.rotation);
            yield return new WaitForSeconds(3f);
            break;
        }
        
    }
}
