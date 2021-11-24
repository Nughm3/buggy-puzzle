using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    List<List<GameObject>> levels = new List<List<GameObject>>();
    List<GameObject> level1 = new List<GameObject>();
    List<GameObject> level2 = new List<GameObject>();

    void Awake() {
        GameObject[] allGameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject item in allGameObjects) {if (item.name == "Level1") level1.Add(item);}
        foreach (GameObject item in allGameObjects) {if (item.name == "Level2") level2.Add(item);}
        levels.Add(level1);
        levels.Add(level2);
    }

    public void LoadLevel(int levelNum) {
        FindObjectOfType<CodeMachine>().Spawn(levelNum);
        FindObjectOfType<Timer>().SetTimer(levelNum);

        foreach (List<GameObject> level in levels) {
            foreach (GameObject item in level) {
                item.SetActive(false);
            }
        }
        foreach (GameObject item in levels[levelNum - 1]) {
            item.SetActive(true);
        }
    }

}