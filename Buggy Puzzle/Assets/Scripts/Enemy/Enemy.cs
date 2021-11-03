using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 0.05f;
    bool allowMove = true;
    int[] pos = {19, 5};
    float distance;

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

        distance = Mathf.Sqrt(Mathf.Pow((Player.myPos[0]) - transform.position.x, 2) + Mathf.Pow((Player.myPos[1]) - (transform.position.y + 0.8f), 2));
        if (distance < minimumDistance) {
            direction = Enums.Direction.Up;
        }

        if (minimumDistance == Mathf.Infinity) Debug.Log("no dir");
        if (direction == Enums.Direction.Up) Debug.Log("Up");
        if (direction == Enums.Direction.Down) Debug.Log("Down");
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
                if (dir == Enums.Direction.Down) transform.position += new Vector3(0, -speed * num, 0);
                if (dir == Enums.Direction.Left) transform.position += new Vector3(-speed * num, 0, 0);
                if (dir == Enums.Direction.Right) transform.position += new Vector3(speed * num, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
