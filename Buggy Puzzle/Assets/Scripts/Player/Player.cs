using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    float speed = 0.05f;
    bool inMove = false;
    bool allowMove = true;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }

    void Update()
    {
        if (!inMove)
        {
            if (Input.GetKey(KeyCode.UpArrow)) StartCoroutine(Move(Direction.Up));
            if (Input.GetKey(KeyCode.DownArrow)) StartCoroutine(Move(Direction.Down));
            if (Input.GetKey(KeyCode.LeftArrow)) StartCoroutine(Move(Direction.Left));
            if (Input.GetKey(KeyCode.RightArrow)) StartCoroutine(Move(Direction.Right));
        }
    }

    IEnumerator Move(string dir)
    {
        if (allowMove)
        {
            inMove = true;
            int[] movePixels = { 1, 2, 3, 4, 3, 2, 1 };
            foreach (int num in movePixels)
            {
                if (dir == Direction.Up) transform.position += new Vector3(0, speed * num, 0);
                if (dir == Direction.Down) transform.position += new Vector3(0, -speed * num, 0);
                if (dir == Direction.Left) transform.position += new Vector3(-speed * num, 0, 0);
                if (dir == Direction.Right) transform.position += new Vector3(speed * num, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.05f);
            inMove = false;
        }
    }
}