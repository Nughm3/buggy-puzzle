using UnityEngine;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    List<List<GameObject>> levels = new List<List<GameObject>>();
    List<GameObject> level1 = new List<GameObject>();
    List<GameObject> level2 = new List<GameObject>();
    List<GameObject> level3 = new List<GameObject>();
    List<GameObject> level4 = new List<GameObject>();
    List<GameObject> level5 = new List<GameObject>();
    List<GameObject> level6 = new List<GameObject>();
    List<GameObject> level7 = new List<GameObject>();
    List<GameObject> level8 = new List<GameObject>();
    List<GameObject> level9 = new List<GameObject>();
    List<GameObject> level10 = new List<GameObject>();
    
    void Awake() {
        GameObject[] allGameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject item in allGameObjects) {if (item.name == "Level1") level1.Add(item);}
        foreach (GameObject item in allGameObjects) {if (item.name == "Level2") level2.Add(item);}
        foreach (GameObject item in allGameObjects) {if (item.name == "Level3") level3.Add(item);}
        foreach (GameObject item in allGameObjects) {if (item.name == "Level4") level4.Add(item);}
        foreach (GameObject item in allGameObjects) {if (item.name == "Level5") level5.Add(item);}
        foreach (GameObject item in allGameObjects) {if (item.name == "Level6") level6.Add(item);}
        foreach (GameObject item in allGameObjects) {if (item.name == "Level7") level7.Add(item);}
        foreach (GameObject item in allGameObjects) {if (item.name == "Level8") level8.Add(item);}
        foreach (GameObject item in allGameObjects) {if (item.name == "Level9") level9.Add(item);}
        foreach (GameObject item in allGameObjects) {if (item.name == "Level10") level10.Add(item);}
        levels.Add(level1);
        levels.Add(level2);
        levels.Add(level3);
        levels.Add(level4);
        levels.Add(level5);
        levels.Add(level6);
        levels.Add(level7);
        levels.Add(level8);
        levels.Add(level9);
        levels.Add(level10);
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