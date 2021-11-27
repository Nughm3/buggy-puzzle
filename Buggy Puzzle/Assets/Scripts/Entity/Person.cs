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

        if (Input.GetKeyDown(KeyCode.Z) && inPlayerRange && !CodeMenu.menuOpened && !BugMenu.menuOpened) {
            if (DialogueManager.inDialogue) FindObjectOfType<DialogueManager>().EndDialogue();
            else {
                if (BugManager.bug == "Scary") {
                    int random = Random.Range(0,2);
                    if (random == 0) FindObjectOfType<DialogueManager>().TriggerDialogue(dialogue);
                    else FindObjectOfType<DialogueManager>().TriggerDialogue("You don't look familiar...");
                }
                else FindObjectOfType<DialogueManager>().TriggerDialogue(dialogue);
            }
        }
    }
}