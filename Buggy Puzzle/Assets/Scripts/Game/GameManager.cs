using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;

    public static int[] code;

    public IEnumerator Play(int level)
    {
        MenuManager.allowInput = false;
        yield return StartCoroutine(FindObjectOfType<Fade>().FadeOut());
        FindObjectOfType<LevelMenu>().Back();
        game.SetActive(true);
        menu.SetActive(false);
        MenuManager.allowInput = true;

        GenerateCode(level);
        FindObjectOfType<Camera>().ResetCamera();
        Camera.allowCheckScroll = true;
        Player.tilePos = new Vector2(1,5);
        FindObjectOfType<Player>().Spawn(level);
        FindObjectOfType<LevelLoader>().LoadLevel(level);
        FindObjectOfType<EnemySpawner>().SpawnEnemies(level);
        FindObjectOfType<PersonSpawner>().AssignDialogue(code);
        FindObjectOfType<PersonSpawner>().SpawnPeople(level);
        yield return StartCoroutine(FindObjectOfType<Fade>().FadeIn());
    }

    void GenerateCode(int level) {
        if (level <= 2) code = new int[4];
        else code = new int[5];

        for (int i = 0; i < code.Length; i++) {
            code[i] = Random.Range(0,10);
        }
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