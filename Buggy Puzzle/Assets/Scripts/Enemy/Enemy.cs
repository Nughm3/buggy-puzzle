using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 0.05f;
    bool inMove = false;
    bool allowMove = true;

    void Update()
    {
        if (!inMove)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                StartCoroutine(Move(Player.Direction.Up));
            }
        }
    }

    float CalcDistance(Player.Direction dir)
    {
        float dist = 0f;
        switch (dir)
        {
            default:
                break;
        }
        return dist / 0.8f;
    }

    IEnumerator Move(Player.Direction dir)
    {
        if (allowMove)
        {
            inMove = true;
            int[] movePixels = { 1, 2, 3, 4, 3, 2, 1 };
            foreach (int num in movePixels)
            {
                if (dir == Player.Direction.Up) transform.position += new Vector3(0, speed * num, 0);
                if (dir == Player.Direction.Down) transform.position += new Vector3(0, -speed * num, 0);
                if (dir == Player.Direction.Left) transform.position += new Vector3(-speed * num, 0, 0);
                if (dir == Player.Direction.Right) transform.position += new Vector3(speed * num, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.05f);
            inMove = false;
        }
    }
}
