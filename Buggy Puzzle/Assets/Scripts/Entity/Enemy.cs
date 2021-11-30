using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 0.05f;
    int spinDir;
    bool allowMove = true;
    bool inMove = false;
    float distance;
    Vector3 myPos;
    readonly float tileSize = 0.8f;
    RaycastHit2D moveRay;
    RaycastHit2D moveRaySafe;
    float waitMoveSpeed = 0.3f;
    bool runSpawnAlert = false;
    bool playerIsLooking = false;
    int speedDivider = 1;

    bool inPlayerRange = false;
    float seePlayerRange = 10f;

    Enums.Direction minDirection;
    Enums.Direction maxDirection;
    public GameObject alertPrefab;
    GameObject myAlert;
    Animator animator;

    LayerMask tileMask;
    LayerMask safeMask;
    public GameObject safeTiles;

    void Start()
    {
        StartCoroutine(WaitMove());
        myPos = transform.position;
        animator = GetComponent<Animator>();
        spinDir = Random.Range(0,2);
    }

    void Update() {
        if (Player.isAlive) CheckVision();
        else {
            animator.SetInteger("State", 6);
            transform.Rotate(0,0,800*Time.deltaTime * (spinDir - 0.5f)*2);
        }
        if (inMove) Destroy(myAlert);

        if (Player.isAlive) {
            if (Player.facingDir == Enums.Direction.Up && transform.position.y > FindObjectOfType<Player>().transform.position.y) playerIsLooking = true;
            else if (Player.facingDir == Enums.Direction.Down && transform.position.y < FindObjectOfType<Player>().transform.position.y) playerIsLooking = true;
            else if (Player.facingDir == Enums.Direction.Left && transform.position.x < FindObjectOfType<Player>().transform.position.x) playerIsLooking = true;
            else if (Player.facingDir == Enums.Direction.Right && transform.position.x > FindObjectOfType<Player>().transform.position.x) playerIsLooking = true;
            else playerIsLooking = false;
        }

        if (BugManager.bug == "Confuse") speedDivider = 3;
        else speedDivider = 1;
    }

    void CheckVision() {
        RaycastHit2D lineToPlayer = Physics2D.Linecast(transform.position, FindObjectOfType<Player>().myPos, tileMask);
        RaycastHit2D LineToPlayerSafe = Physics2D.Linecast(transform.position, FindObjectOfType<Player>().myPos, safeMask);
        if (Vector3.Distance(myPos,FindObjectOfType<Player>().myPos) <= seePlayerRange && lineToPlayer.collider == null && LineToPlayerSafe.collider == null && !(BugManager.bug == "Sneak" && !DialogueManager.inDialogue)) {
            if (runSpawnAlert) {
                animator.SetInteger("State", 5);
                FindObjectOfType<AudioManager>().PlaySound("alert");
                if (myAlert != null) Destroy(myAlert);
                myAlert = Instantiate(alertPrefab, transform.position + new Vector3(0,0.9f,0), transform.rotation);
                StartCoroutine(WaitAlert());
                runSpawnAlert = false;
            }
        }
        else {
            if (inPlayerRange) {
                Destroy(myAlert);
                animator.SetInteger("State", 0);
            }
            StopCoroutine(WaitAlert());
            inPlayerRange = false;
            runSpawnAlert = true;
        }
    }

    IEnumerator WaitAlert() {
        yield return new WaitForSeconds(Time.deltaTime * 25);
        inPlayerRange = true;
    }

    public IEnumerator WaitMove() {
        while (true) {
            if (inPlayerRange && !inMove) {
                while (inPlayerRange) {
                    if (Player.isAlive) CalculateDistance();
                    yield return new WaitForSeconds((waitMoveSpeed + Random.Range(-0.1f,0.1f)) * speedDivider);
                }
            }
            else yield return new WaitForFixedUpdate();
        }
    }

    void CalculateDistance() {
        float minimumDistance = Mathf.Infinity;
        float maximumDistance = Mathf.NegativeInfinity;

        moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0,0.8f,0), tileMask);
        moveRaySafe = Physics2D.Linecast(transform.position, transform.position + new Vector3(0,0.8f,0), safeMask);
        if (moveRay.collider == null && moveRaySafe.collider == null) {
            myPos = transform.position + new Vector3(0,tileSize,0);
            distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
            if (distance < minimumDistance) {
                minimumDistance = distance;
                minDirection = Enums.Direction.Up;
            }
            if (distance > maximumDistance) {
                maximumDistance = distance;
                maxDirection = Enums.Direction.Up;
            }
        }

        moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0,-0.8f,0), tileMask);
        moveRaySafe = Physics2D.Linecast(transform.position, transform.position + new Vector3(0,-0.8f,0), safeMask);
        if (moveRay.collider == null && moveRaySafe.collider == null) {
            myPos = transform.position - new Vector3(0,tileSize,0);
            distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
            if (distance < minimumDistance) {
                minimumDistance = distance;
                minDirection = Enums.Direction.Down;
            }
            if (distance > maximumDistance) {
                maximumDistance = distance;
                maxDirection = Enums.Direction.Down;
            }
        }

        moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(-0.8f,0,0), tileMask);
        moveRaySafe = Physics2D.Linecast(transform.position, transform.position + new Vector3(-0.8f,0,0), safeMask);
        if (moveRay.collider == null && moveRaySafe.collider == null) {
            myPos = transform.position - new Vector3(tileSize,0,0);
            distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
            if (distance < minimumDistance) {
                minimumDistance = distance;
                minDirection = Enums.Direction.Left;
            }
            if (distance > maximumDistance) {
                maximumDistance = distance;
                maxDirection = Enums.Direction.Left;
            }
        }

        moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0.8f,0,0), tileMask);
        moveRaySafe = Physics2D.Linecast(transform.position, transform.position + new Vector3(0.8f,0,0), safeMask);
        if (moveRay.collider == null && moveRaySafe.collider == null) {
            myPos = transform.position + new Vector3(tileSize,0,0);
            distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
            if (distance < minimumDistance) {
                minimumDistance = distance;
                minDirection = Enums.Direction.Right;
            }
            if (distance > maximumDistance) {
                maximumDistance = distance;
                maxDirection = Enums.Direction.Right;
            }
        }

        if (BugManager.bug == "Scary" && playerIsLooking) StartCoroutine(Move(maxDirection));
        else StartCoroutine(Move(minDirection));
    }

    IEnumerator Move(Enums.Direction dir)
    {
        if (allowMove)
        {
            inMove = true;
            int[] movePixels = { 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1 };
            if (dir == Enums.Direction.Up) animator.SetInteger("State", 1);
            if (dir == Enums.Direction.Down) animator.SetInteger("State", 2);
            if (dir == Enums.Direction.Left) animator.SetInteger("State", 3);
            if (dir == Enums.Direction.Right) animator.SetInteger("State", 4);
            foreach (int num in movePixels)
            {
                if (dir == Enums.Direction.Up) transform.position += new Vector3(0, speed * num, 0);
                if (dir == Enums.Direction.Down) transform.position += new Vector3(0, -speed * num, 0);
                if (dir == Enums.Direction.Left) transform.position += new Vector3(-speed * num, 0, 0);
                if (dir == Enums.Direction.Right) transform.position += new Vector3(speed * num, 0, 0);
                yield return new WaitForSeconds(Time.deltaTime * 0.75f);
            }
            inMove = false;
        }
    }

    void Awake() {
        tileMask = LayerMask.GetMask("Tiles");
        safeMask = LayerMask.GetMask("Safe Area");
    }
}