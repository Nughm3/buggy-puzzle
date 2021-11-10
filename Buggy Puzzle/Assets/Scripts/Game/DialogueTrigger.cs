using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public void TriggerDialogue(Dialogue dialogue) {
        if (!FindObjectOfType<DialogueManager>().inDialogue) FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
