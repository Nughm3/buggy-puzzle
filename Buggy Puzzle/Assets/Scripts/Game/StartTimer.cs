using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class StartTimer : MonoBehaviour
{
    TextMeshPro startText;

    public IEnumerator Timer() {
        startText = gameObject.GetComponent<TextMeshPro>();
        while (FindObjectOfType<Fade>().GetComponent<SpriteRenderer>().color.a > 0) {
            yield return null;
        }
        startText.text = "3";
        yield return new WaitForSeconds(0.8f);
        startText.text = "2";
        yield return new WaitForSeconds(0.8f);
        startText.text = "1";
        yield return new WaitForSeconds(0.8f);
        startText.text = "Start!";
        Player.allowMove = true;
        yield return new WaitForSeconds(0.4f);
        startText.text = "";
    }
}