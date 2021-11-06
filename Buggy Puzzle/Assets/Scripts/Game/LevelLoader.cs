using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{

    public void LoadLevel(int level) {
        var level1 = GameObject.FindGameObjectsWithTag("Level1");
        var level2 = GameObject.FindGameObjectsWithTag("Level2");
        HideAllLevels();
        //Only show the level I need or smth i gtg
    }

    void HideAllLevels() {
        var level1 = GameObject.FindGameObjectsWithTag("Level1");
        var level2 = GameObject.FindGameObjectsWithTag("Level2");
        foreach (GameObject item in level1) {item.SetActive(false);}
    }
}