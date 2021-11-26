using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource damage;

    public void PlaySound(string name) {
        if (name == "damage") damage.Play();
    }
}