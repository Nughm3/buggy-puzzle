using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public bool allowNextDialogue = true;

    void Start() {
        StartCoroutine(waitFight1());
    }

    public void TriggerDialogue(Dialogue dialogue) {
        if (!FindObjectOfType<DialogueManager>().inDialogue) FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    IEnumerator waitFight1() {
        yield return new WaitForSeconds(1);
        StartCoroutine(Fight1());
    }

    IEnumerator Fight1() {
        Dialogue[] enemy1 = {dialogue1,dialogue2,dialogue3};

        foreach (Dialogue dialogue in enemy1) {
            TriggerDialogue(dialogue);
            allowNextDialogue = false;
            yield return new WaitUntil(() => allowNextDialogue);
        }
        FindObjectOfType<DialogueManager>().inDialogue = true;
        Debug.Log("ok you win");
    }
}
