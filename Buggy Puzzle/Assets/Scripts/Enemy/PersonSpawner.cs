using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    string[,] emptyDialogue = new string[11,20] {
        {"","","","","","","","","","","","","","","","","","","",""},
        {"","","","","","","","","","","","","","","","","","","",""},
        {"","","","","","","","","","","","","","","","","","","",""},
        {"","","","","","","","","","","","","","","","","","","",""},
        {"","","","","","","","","","","","","","","","","","","",""},
        {"","","","","","","","","","","","","","","","","","","",""},
        {"","","","","","","","","","","","","","","","","","","",""},
        {"","","","","","","","","","","","","","","","","","","",""},
        {"","","","","","","","","","","","","","","","","","","",""},
        {"","","","","","","","","","","","","","","","","","","",""},
        {"","","","","","","","","","","","","","","","","","","",""}
    };
    string[,] dialogue1;

    int[,] level1 = new int[11, 20] {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };

    int[,] level2 = new int[11, 20] {
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };

    float posx;
    float posy;
    float tileSize = 0.8f;
    public GameObject personPrefab;
    GameObject instantiatedObject;
    Vector2 spawnPos;

    public void SpawnPeople(int level) {
        int[,] currentLevel = new int[11, 20];
        if (level == 1) currentLevel = level1;
        if (level == 2) currentLevel = level2;

        for (int y = 0; y < currentLevel.GetLength(0); y += 1) {
            for (int x = 0; x < currentLevel.GetLength(1); x += 1) {
                if (currentLevel[y, x] == 1) {
                    posx = -7.6f + (x * tileSize);
                    posy = 4 - (y * tileSize);
                    spawnPos = new Vector2(posx, posy);
                    instantiatedObject = Instantiate(personPrefab, spawnPos, transform.rotation);
                    instantiatedObject.GetComponent<Person>().tilePos = new Vector2(x,y);
                    if (level == 1) instantiatedObject.GetComponent<Person>().dialogue = dialogue1[y, x];
                }
            }
        }
    }

    public void RemovePeople() {
        var people = GameObject.FindGameObjectsWithTag("Person");
        foreach (var person in people) {
            Destroy(person);
        }
    }

    public void AssignDialogue(int[] code) {
        dialogue1 = emptyDialogue;

        dialogue1[5,8] = $"The third digit is {code[2]}.";
    }
}
