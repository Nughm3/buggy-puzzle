using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    float speed = 0.05f;
    public static bool inMove = false;
    public static bool allowMove = true;
    public static bool cameraIsMoving = false;
    public Vector3 myPos = new Vector3(-6.8f, 0, 0);
    public static Vector2 tilePos;
    public Vector3[] spawnPoints = { new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0) };
    RaycastHit2D moveRay;
    public GameObject tiles;
    public GameObject borderTiles;
    LayerMask tileMask;

    public void Spawn(int level)
    {
        transform.position = spawnPoints[level - 1];
        myPos = transform.position;
    }

    void FixedUpdate()
    {
        if (transform.position.x - FindObjectOfType<Camera>().myPos.x < -8.39f || transform.position.x - FindObjectOfType<Camera>().myPos.x > 8.39f || transform.position.y - FindObjectOfType<Camera>().myPos.y < -4.79f || transform.position.y - FindObjectOfType<Camera>().myPos.y > 4.79f) cameraIsMoving = true;
        if (!inMove && !cameraIsMoving)
        {
            if (Input.GetKey(KeyCode.UpArrow)) StartCoroutine(Move(Enums.Direction.Up));
            else if (Input.GetKey(KeyCode.DownArrow)) StartCoroutine(Move(Enums.Direction.Down));
            else if (Input.GetKey(KeyCode.LeftArrow)) StartCoroutine(Move(Enums.Direction.Left));
            else if (Input.GetKey(KeyCode.RightArrow)) StartCoroutine(Move(Enums.Direction.Right));
        }
    }

    public IEnumerator Move(Enums.Direction dir)
    {
        if (allowMove)
        {
            inMove = true;
            int[] movePixels = { 1, 2, 2, 3, 3, 2, 2, 1 };
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
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(0.02f);
                myPos = transform.position;
            }
            inMove = false;
        }
    }

    public void Reset()
    {
        StopAllCoroutines();
        inMove = false;
    }

    void Awake() {
        tileMask = LayerMask.GetMask("Tiles");
    }
}