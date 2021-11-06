using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    float speed = 0.05f;
    bool inMove = false;
    bool allowMove = true;
    public Vector3 myPos = new Vector3(-6.8f, 0, 0);
    public Vector3[] spawnPoints = { new Vector3(-6.8f, 0, 0), new Vector3(-6.8f, 0, 0) };
    RaycastHit2D moveRay;

    public void Spawn(int level)
    {
        transform.position = spawnPoints[level - 1];
        myPos = transform.position;
    }

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

    public IEnumerator Move(Enums.Direction dir)
    {
        if (allowMove)
        {
            inMove = true;
            int[] movePixels = { 1, 2, 3, 4, 3, 2, 1 };
            if (dir == Enums.Direction.Up) moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0, 0.8f, 0));
            if (dir == Enums.Direction.Down) moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0, -0.8f, 0));
            if (dir == Enums.Direction.Left) moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(-0.8f, 0, 0));
            if (dir == Enums.Direction.Right) moveRay = Physics2D.Linecast(transform.position, transform.position + new Vector3(0.8f, 0, 0));
            if (moveRay.collider == null)
            {
                foreach (int num in movePixels)
                {
                    if (dir == Enums.Direction.Up) transform.position += new Vector3(0, speed * num, 0);
                    if (dir == Enums.Direction.Down) transform.position += new Vector3(0, -speed * num, 0);
                    if (dir == Enums.Direction.Left) transform.position += new Vector3(-speed * num, 0, 0);
                    if (dir == Enums.Direction.Right) transform.position += new Vector3(speed * num, 0, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(0.03f);
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
}