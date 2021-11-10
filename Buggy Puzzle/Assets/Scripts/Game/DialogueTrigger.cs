using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public void TriggerDialogue(string dialogue) {
        if (!FindObjectOfType<DialogueManager>().inDialogue) FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
