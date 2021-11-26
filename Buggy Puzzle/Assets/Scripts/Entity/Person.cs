using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public string dialogue;
    public Vector2 tilePos;
    bool inPlayerRange = false;

    void Update() {
        if (Mathf.Abs(tilePos.x - Player.tilePos.x) < 1.1f && Mathf.Abs(tilePos.y - Player.tilePos.y) < 1.1f) {
            inPlayerRange = true;
        }
        else {
            if (inPlayerRange) FindObjectOfType<DialogueManager>().EndDialogue();
            inPlayerRange = false;
        }

        if (Input.GetKeyDown(KeyCode.Z) && inPlayerRange) {
            if (DialogueManager.inDialogue) FindObjectOfType<DialogueManager>().EndDialogue();
            else FindObjectOfType<DialogueManager>().TriggerDialogue(dialogue);
        }
    }
}