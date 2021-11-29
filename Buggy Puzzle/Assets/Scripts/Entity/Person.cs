using UnityEngine;

public class Person : MonoBehaviour
{
    public string dialogue;
    public Vector2 tilePos;
    bool inPlayerRange = false;

    Animator animator;
    int animationState = 5;

    void Update() {
        if (Mathf.Abs(tilePos.x - Player.tilePos.x) <= 1 && Mathf.Abs(tilePos.y - Player.tilePos.y) <= 1) {
            if (!inPlayerRange) {
                ZIndicator.pos = transform.position + new Vector3(0, 0.9f, 0);
                ZIndicator.show = true;
            }
            inPlayerRange = true;
        }
        else {
            if (inPlayerRange) {
                FindObjectOfType<DialogueManager>().EndDialogue();
                animationState = 5;
                ZIndicator.show = false;
            }
            inPlayerRange = false;
        }

        if (Input.GetKeyDown(KeyCode.Z) && inPlayerRange && !CodeMenu.menuOpened && !BugMenu.menuOpened) {
            if (DialogueManager.inDialogue) {
                FindObjectOfType<DialogueManager>().EndDialogue();
                animationState = 5;
            } 
            else {
                SetDirection();
                if (BugManager.bug == "Scary") {
                    int random = Random.Range(0,10);
                    if (random < 7) FindObjectOfType<DialogueManager>().TriggerDialogue(dialogue);
                    else FindObjectOfType<DialogueManager>().TriggerDialogue("You don't look familiar...");
                }
                else FindObjectOfType<DialogueManager>().TriggerDialogue(dialogue);
            }
        }

        animator.SetInteger("State", animationState);
    }

    void SetDirection() {
        if (Player.tilePos == tilePos) animationState = 5;
        else if (Player.tilePos.y < tilePos.y && Player.tilePos.x >= tilePos.x) animationState = 1;
        else if (Player.tilePos.y > tilePos.y && Player.tilePos.x <= tilePos.x) animationState = 2;
        else if (Player.tilePos.x < tilePos.x && Player.tilePos.y <= tilePos.y) animationState = 3;
        else animationState = 4;
    }

    void Start() {
        animator = GetComponent<Animator>();
    }
}