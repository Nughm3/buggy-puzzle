using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject game;

    public IEnumerator Play(int level) {
        Debug.Log("Started level " + level);
        yield return StartCoroutine(FindObjectOfType<Fade>().FadeOut());
        game.SetActive(true);
        menu.SetActive(false);
        yield return StartCoroutine(FindObjectOfType<Fade>().FadeIn());
    }
}