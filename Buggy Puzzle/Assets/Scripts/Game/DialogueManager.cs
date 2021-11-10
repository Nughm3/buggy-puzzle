using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshPro dialogueText;
    public GameObject dialogueUI;

    string sentence;
    public bool inDialogue = false;
    bool inSentence = false;

    public void StartDialogue(string dialogue) {

        inDialogue = true;
        sentence = dialogue;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        if (FindObjectOfType<Player>().myPos.y > 0) dialogueUI.transform.position = new Vector3(0, -3.8f, 0);
        else dialogueUI.transform.position = new Vector3(0, 3.8f, 0);
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        string currentDialogueText = "";
        inSentence = true;
        foreach (char letter in sentence.ToCharArray()) {
            if (letter.ToString() == "`") currentDialogueText += "<br>";
            else currentDialogueText += letter;
            dialogueText.text = currentDialogueText;
            yield return new WaitForSeconds(0.02f);
        }
        inSentence = false;
    }

    void EndDialogue() {
        inDialogue = false;
        dialogueText.text = "";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && !inSentence) EndDialogue();
        if (inDialogue) dialogueUI.SetActive(true);
        else dialogueUI.SetActive(false);
    }
}
