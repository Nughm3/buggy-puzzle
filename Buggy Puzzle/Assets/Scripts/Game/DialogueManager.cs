using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshPro nameText;
    public TextMeshPro dialogueText;
    public GameObject dialogueSprites;
    public bool inDialogue = false;
    bool inSentence = false;
    bool skipSentence = false;

    Queue<string> sentences;
    Queue<string> names;

    void Awake() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {

        inDialogue = true;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        Debug.Log(name);

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
            if (!skipSentence) yield return new WaitForSeconds(0.015f);
        }
        skipSentence = false;
        inSentence = false;
    }

    void EndDialogue() {
        inDialogue = false;
        nameText.text = "";
        dialogueText.text = "";
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && !inSentence) DisplayNextSentence();
        if (Input.GetKeyDown(KeyCode.X) && inSentence) skipSentence = true;

        if (inDialogue) dialogueSprites.SetActive(true);
        else dialogueSprites.SetActive(false);
    }
}
