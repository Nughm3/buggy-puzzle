using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    float speed = 0.05f;
    public static bool inMove = false;
    public static bool allowMove = false;
    public static bool cameraIsMoving = false;

    bool inHurt = false;
    public static int health = 5;
    public static int maxHealth = 5;
    public static bool isAlive = true;
    float speedMultiplier = 0.75f;
    
    public static Enums.Direction facingDir;
    public Vector3 myPos = new Vector3(-6.8f, 0, 0);
    public static Vector2 tilePos = new Vector2(22,17);
    public Vector3[] spawnPoints = { new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0)};

    public GameObject tiles;
    public GameObject borderTiles;
    public GameObject codeMenu;
    public GameObject deathMenu;
    LayerMask tileMask;
    RaycastHit2D moveRay;

    public void Spawn(int level)
    {
        transform.position = new Vector3(-6.8f, 0, 0);
        myPos = transform.position;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,1f);
    }

    void FixedUpdate()
    {
        if (!inMove && !cameraIsMoving && transform.position.x - FindObjectOfType<Camera>().myPos.x > -8.3f && transform.position.x - FindObjectOfType<Camera>().myPos.x < 8.3f && transform.position.y - FindObjectOfType<Camera>().myPos.y > -4.7f && transform.position.y - FindObjectOfType<Camera>().myPos.y < 4.7f)
        {
            if (Input.GetKey(KeyCode.UpArrow)) StartCoroutine(Move(Enums.Direction.Up));
            else if (Input.GetKey(KeyCode.DownArrow)) StartCoroutine(Move(Enums.Direction.Down));
            else if (Input.GetKey(KeyCode.LeftArrow)) StartCoroutine(Move(Enums.Direction.Left));
            else if (Input.GetKey(KeyCode.RightArrow)) StartCoroutine(Move(Enums.Direction.Right));
        }
    }

    public IEnumerator Move(Enums.Direction dir)
    {
        if (allowMove && !CodeMenu.menuOpened)
        {
            inMove = true;
            facingDir = dir;
            int[] movePixels = { 2, 3, 3, 3, 3, 2 };
            speedMultiplier = 0.75f;
            if (BugManager.speedMultiplier == 2) {
                movePixels = new int[] {5, 6, 5};
                speedMultiplier = 0.5f;
            }
            if (dir == Enums.Direction.Up) moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0, 0.8f, 0), tileMask);
            if (dir == Enums.Direction.Down) moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0, -0.8f, 0), tileMask);
            if (dir == Enums.Direction.Left) moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(-0.8f, 0, 0), tileMask);
            if (dir == Enums.Direction.Right) moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0.8f, 0, 0), tileMask);
            if (moveRay.collider == null)
            {
                if (dir == Enums.Direction.Up) tilePos.y -= 1;
                if (dir == Enums.Direction.Down) tilePos.y += 1;
                if (dir == Enums.Direction.Left) tilePos.x -= 1;
                if (dir == Enums.Direction.Right) tilePos.x += 1;
                foreach (int num in movePixels)
                {
                    if (dir == Enums.Direction.Up) transform.position += new Vector3(0, speed * num, 0);
                    if (dir == Enums.Direction.Down) transform.position += new Vector3(0, -speed * num, 0);
                    if (dir == Enums.Direction.Left) transform.position += new Vector3(-speed * num, 0, 0);
                    if (dir == Enums.Direction.Right) transform.position += new Vector3(speed * num, 0, 0);
                    yield return new WaitForSeconds(Time.deltaTime * speedMultiplier * (1 / PauseManager.defaultTimeScale));
                }
                yield return new WaitForSeconds(Time.deltaTime * speedMultiplier);
                myPos = transform.position;
            }
            inMove = false;
        }
    }

    void OpenCodeMenu() {
        codeMenu.GetComponent<CodeMenu>().Show();
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy" && !inHurt) {
            StartCoroutine(TakeDamage(1));
        }
    }

    IEnumerator TakeDamage(int damage) {
        FindObjectOfType<AudioManager>().PlaySound("damage");
        inHurt = true;
        health -= 1 * BugManager.damageMultiplier;
        StartCoroutine(IFrames());
        if (health < 0) health = 0;
        if (health == 0) Death("enemy");
        yield return new WaitForSeconds(1.5f);
        inHurt = false;
    }

    IEnumerator IFrames() {
        while (inHurt && BugManager.bug != "Sneak") {
            if (inHurt && BugManager.bug != "Sneak") gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,0f);
            if (!inHurt && BugManager.bug != "Sneak") gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,1f);
            yield return new WaitForSeconds(Time.deltaTime * 2f);
            if (inHurt && BugManager.bug != "Sneak") gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,1f);
            if (!inHurt && BugManager.bug != "Sneak") gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,1f);
            yield return new WaitForSeconds(Time.deltaTime * 2f);
        }
    }

    public IEnumerator SneakPulse() {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,0.25f);
        while (BugManager.bug == "Sneak") {
            while (gameObject.GetComponent<SpriteRenderer>().color.a > 0 && BugManager.bug == "Sneak") {
                gameObject.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,0.02f);
                yield return new WaitForSeconds(Time.deltaTime * 0.75f);
            }
            for (int i = 0; i < 40; i++) {
                yield return new WaitForSeconds(Time.deltaTime * 0.75f);
                if (BugManager.bug != "Sneak") break;
            }
            while (gameObject.GetComponent<SpriteRenderer>().color.a < 0.25 && BugManager.bug == "Sneak") {
                gameObject.GetComponent<SpriteRenderer>().color += new Color(0,0,0,0.015f);
                yield return new WaitForSeconds(Time.deltaTime * 0.75f);
            }
            for (int i = 0; i < 10; i++) {
                yield return new WaitForSeconds(Time.deltaTime * 0.75f);
                if (BugManager.bug != "Sneak") break;
            }
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,1f);
    }

    public void Death(string type) {
        isAlive = false;
        deathMenu.SetActive(true);
        FindObjectOfType<Timer>().StopTimer();
        gameObject.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && CodeMachine.inPlayerRange && !CodeMenu.menuOpened && !PauseMenu.menuOpened && !BugMenu.menuOpened && Timer.minutes + Timer.seconds > 0) OpenCodeMenu();
    }

    public void Reset()
    {
        StopAllCoroutines();
        tilePos = new Vector2(22,17);
        inMove = false;
        allowMove = false;
        inHurt = false;
        health = maxHealth;
        isAlive = true;
    }

    void Awake() {
        tileMask = LayerMask.GetMask("Tiles");
    }
}