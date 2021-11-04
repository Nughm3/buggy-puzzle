using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 0.05f;
    bool allowMove = true;
    int[] pos = {19, 5};
    float distance;
    Vector3 myPos;
    readonly float tileSize = 0.8f;
    RaycastHit2D moveRay;
    float waitMoveSpeed = 0.3f;

    bool inPlayerRange = false;
    float seePlayerRange = 6.5f;

    Enums.Direction direction;
    public GameObject alertPrefab;

    void Start()
    {
        myPos = transform.position;
        StartCoroutine(WaitMove());
    }

    void Update() {
        CheckVision();
    }

    void CheckVision() {
        RaycastHit2D lineToPlayer = Physics2D.Linecast(transform.position, FindObjectOfType<Player>().myPos);
        if (Vector3.Distance(myPos,FindObjectOfType<Player>().myPos) <= seePlayerRange && lineToPlayer.collider == null) {
            if (!inPlayerRange) {
                Instantiate(alertPrefab, transform.position + new Vector3(0,0.8f,0), transform.rotation);
            }
            inPlayerRange = true;
        }
        else inPlayerRange = false;
    }

    public IEnumerator WaitMove() {
        while (true) {
            if (inPlayerRange) {
                yield return new WaitForSeconds(0.5f);
                while (inPlayerRange) {
                    CalculateDistance();
                    yield return new WaitForSeconds(waitMoveSpeed);
                }
            }
            else yield return new WaitForFixedUpdate();
        }
    }

    void CalculateDistance() {
        float minimumDistance = Mathf.Infinity;

        moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0,0.8f,0));
        if (moveRay.collider == null) {
            myPos = transform.position + new Vector3(0,tileSize,0);
            distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
            if (distance < minimumDistance) {
                minimumDistance = distance;
                direction = Enums.Direction.Up;
            }
        }

        moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0,-0.8f,0));
        if (moveRay.collider == null) {
            myPos = transform.position - new Vector3(0,tileSize,0);
            distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
            if (distance < minimumDistance) {
                minimumDistance = distance;
                direction = Enums.Direction.Down;
            }
        }

        moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(-0.8f,0,0));
        if (moveRay.collider == null) {
            myPos = transform.position - new Vector3(tileSize,0,0);
            distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
            if (distance < minimumDistance) {
                minimumDistance = distance;
                direction = Enums.Direction.Left;
            }
        }

        moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0.8f,0,0));
        if (moveRay.collider == null) {
            myPos = transform.position + new Vector3(tileSize,0,0);
            distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
            if (distance < minimumDistance) {
                minimumDistance = distance;
                direction = Enums.Direction.Right;
            }
        }

        StartCoroutine(Move(direction));
    }

    IEnumerator Move(Enums.Direction dir)
    {
        if (allowMove)
        {
            int[] movePixels = { 1, 2, 3, 4, 3, 2, 1 };
            if (dir == Enums.Direction.Up) pos[1] -= 1;
            if (dir == Enums.Direction.Down) pos[1] += 1;
            if (dir == Enums.Direction.Left) pos[0] -= 1;
            if (dir == Enums.Direction.Right) pos[0] += 1;
            foreach (int num in movePixels)
            {
                if (dir == Enums.Direction.Up) transform.position += new Vector3(0, speed * num, 0);
                if (dir == Enums.Direction.Down) transform.position += new Vector3(0, -speed * num, 0);
                if (dir == Enums.Direction.Left) transform.position += new Vector3(-speed * num, 0, 0);
                if (dir == Enums.Direction.Right) transform.position += new Vector3(speed * num, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    public void Reset() {
        
    }
}
