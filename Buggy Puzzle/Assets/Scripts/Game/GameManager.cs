using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;

    public IEnumerator Play(int level)
    {
        MenuManager.allowInput = false;
        yield return StartCoroutine(FindObjectOfType<Fade>().FadeOut());
        FindObjectOfType<LevelMenu>().Back();
        game.SetActive(true);
        menu.SetActive(false);
        MenuManager.allowInput = true;

        FindObjectOfType<Camera>().ResetCamera();
        Camera.allowCheckScroll = true;
        FindObjectOfType<Player>().Spawn(level);
        FindObjectOfType<LevelLoader>().LoadLevel(level);
        FindObjectOfType<EnemySpawner>().SpawnEnemies(level);
        FindObjectOfType<PersonSpawner>().SpawnPeople(level);
        yield return StartCoroutine(FindObjectOfType<Fade>().FadeIn());
    }

    void Awake()
    {
        CheckForSave();
        FindObjectOfType<PlayerData>().Load();
    }

    void CheckForSave()
    {
        string path = Application.persistentDataPath + "/saveFile.dat";
        if (!File.Exists(path))
        {
            FindObjectOfType<PlayerData>().CreateSave();
        }
    }
}