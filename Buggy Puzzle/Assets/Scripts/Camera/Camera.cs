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
    float waitTime = 0.01f;

    void Update() {
        if (cameraGridPos == new Vector3(0,0,-10) && FindObjectOfType<Player>().myPos.y >= 4.75f) {
            StartCoroutine(ScrollCamera(Enums.Direction.Up));
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
            for (int i = 0; i < 48; i++) {
                transform.position += new Vector3(moveX, moveY, 0);
                yield return new WaitForSecondsRealtime(waitTime);
            }
            inScroll = false;
        }
    }
}
