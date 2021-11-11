using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public string dialogue;
    public Vector2 tilePos;
    bool inPlayerRange = false;

    void Update() {

        if (Mathf.Abs(tilePos.x - Player.tilePos.x) < 1.1f && Mathf.Abs(tilePos.y - Player.tilePos.y) < 1.1f && !Player.inMove) {
            inPlayerRange = true;
        }
        else inPlayerRange = false;

        if (Input.GetKeyDown(KeyCode.Z) && inPlayerRange) FindObjectOfType<DialogueManager>().TriggerDialogue(dialogue);
    }
}