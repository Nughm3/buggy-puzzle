using UnityEngine;
using System.Collections;

public class Hearts : MonoBehaviour
{
    public GameObject[] hearts;

    void Update() {
        for (int i = 0; i < Player.health; i++) {
            hearts[i].SetActive(true);
        }
        for (int i = Player.health; i < Player.maxHealth; i++) {
            hearts[i].SetActive(false);
        }
    }

    public void Reset() {
        transform.position = new Vector3(-8.45f, 4.6f, 0);
        foreach (GameObject heart in hearts) {
            heart.SetActive(true);
        }
    }
}
