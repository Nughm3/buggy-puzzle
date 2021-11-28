using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public string dialogue;
    public Vector2 tilePos;
    bool inPlayerRange = false;

    Animator animator;
    int animationState;

    void Update() {
        if (Mathf.Abs(tilePos.x - Player.tilePos.x) < 1.1f && Mathf.Abs(tilePos.y - Player.tilePos.y) < 1.1f) {
            if (!inPlayerRange) {
                ZIndicator.pos = transform.position + new Vector3(0, 0.8f, 0);
                ZIndicator.show = true;
            }
            inPlayerRange = true;
        }
        else {
            if (inPlayerRange) {
                FindObjectOfType<DialogueManager>().EndDialogue();
                ZIndicator.show = false;
            }
            inPlayerRange = false;
        }

        if (Input.GetKeyDown(KeyCode.Z) && inPlayerRange && !CodeMenu.menuOpened && !BugMenu.menuOpened) {
            if (DialogueManager.inDialogue) FindObjectOfType<DialogueManager>().EndDialogue();
            else {
                if (BugManager.bug == "Scary") {
                    int random = Random.Range(0,10);
                    if (random < 7) FindObjectOfType<DialogueManager>().TriggerDialogue(dialogue);
                    else FindObjectOfType<DialogueManager>().TriggerDialogue("You don't look familiar...");
                }
                else FindObjectOfType<DialogueManager>().TriggerDialogue(dialogue);
            }
        }
    }
}