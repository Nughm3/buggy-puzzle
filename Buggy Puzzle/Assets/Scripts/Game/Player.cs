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
    public static int health = 3;
    
    public Vector3 myPos = new Vector3(-6.8f, 0, 0);
    public static Vector2 tilePos = new Vector2(22,17);
    public Vector3[] spawnPoints = { new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0) };

    public GameObject tiles;
    public GameObject borderTiles;
    public GameObject codeMenu;
    LayerMask tileMask;
    RaycastHit2D moveRay;

    public void Spawn(int level)
    {
        transform.position = spawnPoints[level - 1];
        myPos = transform.position;
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
            int[] movePixels = { 2, 3, 3, 3, 3, 2 };
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
                    yield return new WaitForSeconds(Time.deltaTime * 0.75f);
                }
                yield return new WaitForSeconds(Time.deltaTime * 0.75f);
                myPos = transform.position;
            }
            inMove = false;
        }
    }

    void OpenCodeMenu() {
        codeMenu.GetComponent<CodeMenu>().Show();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy" && !inHurt) {
            StartCoroutine(TakeDamage(1));
        }
    }

    IEnumerator TakeDamage(int damage) {
        Debug.Log("ow");
        inHurt = true;
        health -= 1;
        if (health < 0) health = 0;
        yield return new WaitForSeconds(2);
        inHurt = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z) && CodeMachine.inPlayerRange && !CodeMenu.menuOpened && !PauseMenu.menuOpened) OpenCodeMenu();
    }

    public void Reset()
    {
        StopAllCoroutines();
        inMove = false;
        allowMove = false;
    }

    void Awake() {
        tileMask = LayerMask.GetMask("Tiles");
    }
}