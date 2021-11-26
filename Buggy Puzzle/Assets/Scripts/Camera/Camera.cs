using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public static bool inScroll = false;
    Vector3 cameraGridPos = new Vector3(0,0,-10);
    public Vector3 myPos = new Vector3(0, 0, -10);
    float moveX;
    float moveY;
    float moveSpeed = 0.4f;
    int moveAmount;
    bool inScrollArea = false;
    public static bool allowCheckScroll = false;
    public GameObject timer;

    void Update() {
        if (allowCheckScroll) CheckScroll();
    }

    void CheckScroll() {
        if (FindObjectOfType<Player>().myPos.x - transform.position.x < -8.39f || FindObjectOfType<Player>().myPos.x - transform.position.x > 8.39f || FindObjectOfType<Player>().myPos.y - transform.position.y < -4.79f || FindObjectOfType<Player>().myPos.y - transform.position.y > 4.79f) {
            if (!inScrollArea) {
                Player.cameraIsMoving = true;
                if (FindObjectOfType<Player>().myPos.y - transform.position.y > 4.79f) StartCoroutine(ScrollCamera(Enums.Direction.Up));
                if (FindObjectOfType<Player>().myPos.y - transform.position.y < -4.79f) StartCoroutine(ScrollCamera(Enums.Direction.Down));
                if (FindObjectOfType<Player>().myPos.x - transform.position.x < -8.39f) StartCoroutine(ScrollCamera(Enums.Direction.Left));
                if (FindObjectOfType<Player>().myPos.x - transform.position.x > 8.39f) StartCoroutine(ScrollCamera(Enums.Direction.Right));
            }
            inScrollArea = true;
        }
        else {
            inScrollArea = false;
        }
    }

    public void ResetCamera() {
        Player.cameraIsMoving = false;
        inScroll = false;
        transform.position = new Vector3(0,0,-10);
        myPos = new Vector3(0, 0, -10);
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public IEnumerator ScrollCamera(Enums.Direction dir) {
        if (!inScroll) {
            inScroll = true;
            Player.allowMove = false;
            moveX = 0;
            moveY = 0;
            if (dir == Enums.Direction.Up) {
                cameraGridPos += new Vector3(0, 1, 0);
                moveY = moveSpeed;
            }
            if (dir == Enums.Direction.Down) {
                cameraGridPos += new Vector3(0, -1, 0);
                moveY = -moveSpeed;
            }
            if (dir == Enums.Direction.Left) {
                cameraGridPos += new Vector3(-1, 0, 0);
                moveX = -moveSpeed;
            }
            if (dir == Enums.Direction.Right) {
                cameraGridPos += new Vector3(1, 0, 0);
                moveX = moveSpeed;
            }
            if (dir == Enums.Direction.Up || dir == Enums.Direction.Down) moveAmount = 24;
            if (dir == Enums.Direction.Left || dir == Enums.Direction.Right) moveAmount = 42;
            for (int i = 0; i < moveAmount; i++) {
                transform.position += new Vector3(moveX, moveY, 0);
                timer.transform.position = new Vector3(transform.position.x + 7.9f, transform.position.y + 4.6f, 0);
                yield return new WaitForSeconds(Time.deltaTime * 0.75f);
            }
            inScroll = false;
            myPos = transform.position;
            Player.allowMove = true;
            Player.cameraIsMoving = false;
            StartCoroutine(FindObjectOfType<Player>().Move(dir));
        }
    }
}
