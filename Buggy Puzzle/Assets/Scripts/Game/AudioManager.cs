using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sources;

    void Awake() {
        sources[1].Play();
    }

    public void PlaySound(string name) {
        if (name == "damage") sources[0].Play();
        if (name == "victory") StartCoroutine(Victory());
        if (name == "fail") sources[3].Play();
        if (name == "tick") {
            sources[4].pitch = 1;
            sources[4].Play();
        }
        if (name == "tick2") {
            sources[4].pitch = 2;
            sources[4].Play();
        }
        if (name == "bug") sources[5].Play();
        if (name == "death") sources[6].Play();
    }

    IEnumerator Victory() {
        sources[1].Pause();
        sources[2].Play();
        yield return new WaitWhile(()=> sources[2].isPlaying);
        sources[1].UnPause();
    }

    void Update() {
        if (PauseManager.isPaused || WinMenu.menuOpened || DeathMenu.menuOpened || BugMenu.menuOpened) sources[1].volume = 0.5f;
        else sources[1].volume = 0.9f;
    }
}