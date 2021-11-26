using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshPro dialogueText;
    public GameObject dialogueUI;

    string sentence;
    public static bool inDialogue = false;

    public void TriggerDialogue(string dialogue) {
        if (!inDialogue) StartDialogue(dialogue);
    }

    public void StartDialogue(string dialogue) {

        inDialogue = true;
        sentence = dialogue;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        if (FindObjectOfType<Player>().myPos.y > 0.5f) dialogueUI.transform.position = new Vector3(0, -3.8f, 0) + new Vector3(FindObjectOfType<Camera>().transform.position.x, FindObjectOfType<Camera>().transform.position.y, 0);
        else dialogueUI.transform.position = new Vector3(0, 3.8f, 0) + new Vector3(FindObjectOfType<Camera>().transform.position.x, FindObjectOfType<Camera>().transform.position.y, 0);
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        string currentDialogueText = "";
        foreach (char letter in sentence.ToCharArray()) {
            if (letter.ToString() == "`") currentDialogueText += "<br>";
            else currentDialogueText += letter;
            dialogueText.text = currentDialogueText;
            yield return null;
        }
    }

    public void EndDialogue() {
        StopAllCoroutines();
        inDialogue = false;
        dialogueText.text = "";
    }

    void Update() {
        if (inDialogue) dialogueUI.SetActive(true);
        else dialogueUI.SetActive(false);
    }
}
