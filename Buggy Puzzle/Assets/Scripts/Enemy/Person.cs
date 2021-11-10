using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public string dialogue;

    void Awake() {
        
    }

    void Update() {
        // if (Input.GetKeyDown(KeyCode.K)) FindObjectOfType<DialogueTrigger>().TriggerDialogue(dialogue);
    }
}