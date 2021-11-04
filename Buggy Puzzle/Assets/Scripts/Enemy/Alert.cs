using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    void Start() {
        StartCoroutine(ShowTimer());
    }

    IEnumerator ShowTimer() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
