using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    float speed = 0.05f;
    bool inMove = false;
    bool allowMove = true;
    int[] pos = {10,5};
    public Vector3 myPos = new Vector3(0.4f, 0, 0);

    void FixedUpdate()
    {
        if (!inMove)
        {
            if (Input.GetKey(KeyCode.UpArrow)) StartCoroutine(Move(Enums.Direction.Up));
            else if (Input.GetKey(KeyCode.DownArrow)) StartCoroutine(Move(Enums.Direction.Down));
            else if (Input.GetKey(KeyCode.LeftArrow)) StartCoroutine(Move(Enums.Direction.Left));
            else if (Input.GetKey(KeyCode.RightArrow)) StartCoroutine(Move(Enums.Direction.Right));
        }
    }

    IEnumerator Move(Enums.Direction dir)
    {
        if (allowMove)
        {
            inMove = true;
            int[] targetPos = {pos[0], pos[1]};
            int[] movePixels = { 1, 2, 3, 4, 3, 2, 1 };
            if (dir == Enums.Direction.Up) targetPos[1] -= 1;
            if (dir == Enums.Direction.Down) targetPos[1] += 1;
            if (dir == Enums.Direction.Left) targetPos[0] -= 1;
            if (dir == Enums.Direction.Right) targetPos[0] += 1;
            if (targetPos[0] >= 0 && targetPos[0] < 20 && targetPos[1] >= 0 && targetPos[1] < 11) {
                if (WallManager.walls[targetPos[1],targetPos[0]] != 1) {
                    foreach (int num in movePixels)
                    {
                        if (dir == Enums.Direction.Up) transform.position += new Vector3(0, speed * num, 0);
                        if (dir == Enums.Direction.Down) transform.position += new Vector3(0, -speed * num, 0);
                        if (dir == Enums.Direction.Left) transform.position += new Vector3(-speed * num, 0, 0);
                        if (dir == Enums.Direction.Right) transform.position += new Vector3(speed * num, 0, 0);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.03f);
                    pos = targetPos;
                    myPos = transform.position;
                }
            }
            inMove = false;
        }
    }
}