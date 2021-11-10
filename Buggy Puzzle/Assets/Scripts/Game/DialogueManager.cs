using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshPro dialogueText;
    public bool inDialogue = false;
    bool inSentence = false;

    Queue<string> sentences;

    void Awake() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {

        inDialogue = true;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        string currentDialogueText = "";
        inSentence = true;
        foreach (char letter in sentence.ToCharArray()) {
            if (letter.ToString() == "`") currentDialogueText += "<br>";
            else currentDialogueText += letter;
            dialogueText.text = currentDialogueText;
            yield return new WaitForSeconds(0.015f);
        }
        inSentence = false;
    }

    void EndDialogue() {
        inDialogue = false;
        dialogueText.text = "";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && !inSentence) DisplayNextSentence();
    }
}
