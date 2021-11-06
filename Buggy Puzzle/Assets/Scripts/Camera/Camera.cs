using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public static bool inScroll = false;
    Vector3 cameraGridPos = new Vector3(0,0,-10);
    float moveX;
    float moveY;
    float moveSpeed = 0.2f;
    float waitTime = 0.006f;
    int moveAmount;
    bool inScrollArea = false;
    public static bool allowCheckScroll = false;

    void Update() {
        if (allowCheckScroll) CheckScroll();
    }

    void CheckScroll() {
        if (FindObjectOfType<Player>().myPos.x - transform.position.x < -8.39f || FindObjectOfType<Player>().myPos.x - transform.position.x > 8.39f || FindObjectOfType<Player>().myPos.y - transform.position.y < -4.79f || FindObjectOfType<Player>().myPos.y - transform.position.y > 4.79f) {
            if (!inScrollArea) {
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
        transform.position = new Vector3(0,0,-10);
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public IEnumerator ScrollCamera(Enums.Direction dir) {
        if (!inScroll) {
            inScroll = true;
            Time.timeScale = 0f;
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
            if (dir == Enums.Direction.Up || dir == Enums.Direction.Down) moveAmount = 48;
            if (dir == Enums.Direction.Left || dir == Enums.Direction.Right) moveAmount = 84;
            for (int i = 0; i < moveAmount; i++) {
                transform.position += new Vector3(moveX, moveY, 0);
                yield return new WaitForSecondsRealtime(waitTime);
            }
            inScroll = false;
            StartCoroutine(FindObjectOfType<Player>().Move(dir));
        }
    }
}
