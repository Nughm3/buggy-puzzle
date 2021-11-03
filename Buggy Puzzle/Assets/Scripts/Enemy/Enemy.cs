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

    Enums.Direction direction;

    void Start()
    {
        StartCoroutine(WaitMove());
    }

    IEnumerator WaitMove() {
        while (true) {
            yield return new WaitForSeconds(1f);
            CalculateDistance();
        }
    }

    void CalculateDistance() {
        float minimumDistance = Mathf.Infinity;

        myPos = transform.position + new Vector3(0,tileSize,0);
        distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
        if (distance < minimumDistance) {
            minimumDistance = distance;
            direction = Enums.Direction.Up;
        }
        Debug.Log("Up: " + distance);

        myPos = transform.position - new Vector3(0,tileSize,0);
        distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
        if (distance < minimumDistance) {
            minimumDistance = distance;
            direction = Enums.Direction.Down;
        }
        Debug.Log("Down: " + distance);

        myPos = transform.position - new Vector3(tileSize,0,0);
        distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
        if (distance < minimumDistance) {
            minimumDistance = distance;
            direction = Enums.Direction.Left;
        }
        Debug.Log("Left: " + distance);

        myPos = transform.position + new Vector3(tileSize,0,0);
        distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
        if (distance < minimumDistance) {
            minimumDistance = distance;
            direction = Enums.Direction.Right;
        }
        Debug.Log("Right: " + distance);

        StartCoroutine(Move(direction));
    }

    IEnumerator Move(Enums.Direction dir)
    {
        if (allowMove)
        {
            int[] movePixels = { 1, 2, 3, 4, 3, 2, 1 };
            foreach (int num in movePixels)
            {
                if (dir == Enums.Direction.Up) transform.position += new Vector3(0, speed * num, 0);
                else if (dir == Enums.Direction.Down) transform.position += new Vector3(0, -speed * num, 0);
                else if (dir == Enums.Direction.Left) transform.position += new Vector3(-speed * num, 0, 0);
                else if (dir == Enums.Direction.Right) transform.position += new Vector3(speed * num, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
