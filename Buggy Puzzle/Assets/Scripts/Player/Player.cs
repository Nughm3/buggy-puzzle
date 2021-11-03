using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    float speed = 0.05f;
    bool inMove = false;
    bool allowMove = true;

    void Update() {
        if (!inMove) {
            if (Input.GetKey(KeyCode.UpArrow)) StartCoroutine(Move("up"));
            if (Input.GetKey(KeyCode.DownArrow)) StartCoroutine(Move("down"));
            if (Input.GetKey(KeyCode.LeftArrow)) StartCoroutine(Move("left"));
            if (Input.GetKey(KeyCode.RightArrow)) StartCoroutine(Move("right"));
        }
    }

    IEnumerator Move(string dir) {
        if (allowMove) {
            inMove = true;
            int[] movePixels = {1,2,3,4,3,2,1};
            foreach (int num in movePixels) {
                if (dir == "up") transform.position += new Vector3(0,speed*num,0);
                if (dir == "down") transform.position += new Vector3(0,-speed*num,0);
                if (dir == "left") transform.position += new Vector3(-speed*num,0,0);
                if (dir == "right") transform.position += new Vector3(speed*num,0,0);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.05f);
            inMove = false;
        }
    }
}