using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sources;

    void Awake() {
        sources[1].Play();
    }

    public void PlaySound(string name) {
        if (name == "damage") sources[0].Play();
    }
}