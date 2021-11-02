using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public SpriteRenderer fade;
    float fadeSpeed = 0.002f;

    public IEnumerator FadeIn() {
        while (fade.color.a > 0) {
            fade.color -= new Color(0,0,0,fadeSpeed);
            yield return null;
        }
    }

    public IEnumerator FadeOut() {
        while (fade.color.a < 1) {
            fade.color += new Color(0,0,0,fadeSpeed);
            yield return null;
        }
    }
}
