using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;
    public GameObject levelMenu;
    public TextMeshPro tempFPS;

    public static int[] code;
    public static int currentLevel;

    public IEnumerator Play(int level)
    {
        currentLevel = level;
        MenuManager.allowInput = false;
        yield return StartCoroutine(FindObjectOfType<Fade>().FadeOut());
        game.SetActive(true);
        menu.SetActive(false);
        MenuManager.allowInput = true;
        DeathMenu.level = level;
        WinMenu.level = level;

        GenerateCode(level);
        FindObjectOfType<Camera>().ResetCamera();
        Camera.allowCheckScroll = true;
        FindObjectOfType<Player>().Spawn(level);
        FindObjectOfType<LevelLoader>().LoadLevel(level);
        FindObjectOfType<EntitySpawner>().AssignDialogue(code);
        FindObjectOfType<EntitySpawner>().SpawnEntities(level);
        StartCoroutine(FindObjectOfType<StartTimer>().ShowDigits());
        StartCoroutine(FindObjectOfType<Fade>().FadeIn());
    }

    void GenerateCode(int level) {
        if (level <= 2) code = new int[4];
        else code = new int[5];

        if (level == 1) {
            for (int i = 0; i < code.Length; i++) {
                code[i] = Random.Range(0,10);
            }
            CodeMenu.codeIsReversed = false;
        }
        if (level == 2) {
            for (int i = 0; i < code.Length; i++) {
                code[i] = Random.Range(0,10);
            }
            int temp = Random.Range(0,2);
            if (temp == 0) CodeMenu.codeIsReversed = false;
            else CodeMenu.codeIsReversed = true;
        }
    }

    void Awake()
    {
        SetResolution();
        CheckForSave();
        FindObjectOfType<PlayerData>().Load();
    }

    void SetResolution() {
        Screen.SetResolution(1920,1080,true);
        Application.targetFrameRate = 60;
    }

    void CheckForSave()
    {
        string path = Application.persistentDataPath + "/saveFile.dat";
        if (!File.Exists(path))
        {
            FindObjectOfType<PlayerData>().CreateSave();
        }
    }

    public void Retry(int level) {
        StartCoroutine(Play(level));
    }
}