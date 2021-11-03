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
            yield return new WaitForSeconds(0.3f);
            CalculateDistance();
        }
    }

    void CalculateDistance() {
        float minimumDistance = Mathf.Infinity;

        if (pos[1]-1 >= 0) {
            if (WallManager.walls[pos[1]-1,pos[0]] == 0) {
                myPos = transform.position + new Vector3(0,tileSize,0);
                distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
                if (distance < minimumDistance) {
                    minimumDistance = distance;
                    direction = Enums.Direction.Up;
                }
            }
        }

        if (pos[1]+1 <= 10) {
            if (WallManager.walls[pos[1]+1,pos[0]] == 0) {
                myPos = transform.position - new Vector3(0,tileSize,0);
                distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
                if (distance < minimumDistance) {
                    minimumDistance = distance;
                    direction = Enums.Direction.Down;
                }
            }
        }

        if (pos[0]-1 >= 0) {
            if (WallManager.walls[pos[1],pos[0]-1] == 0) {
                myPos = transform.position - new Vector3(tileSize,0,0);
                distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
                if (distance < minimumDistance) {
                    minimumDistance = distance;
                    direction = Enums.Direction.Left;
                }
            }
        }

        if (pos[0]+1 <= 19) {
            if (WallManager.walls[pos[1],pos[0]+1] == 0) {
                myPos = transform.position + new Vector3(tileSize,0,0);
                distance = Vector3.Distance(myPos,FindObjectOfType<Player>().myPos);
                if (distance < minimumDistance) {
                    minimumDistance = distance;
                    direction = Enums.Direction.Right;
                }
            }
        }

        StartCoroutine(Move(direction));
    }

    IEnumerator Move(Enums.Direction dir)
    {
        if (allowMove)
        {
            int[] movePixels = { 1, 2, 3, 4, 3, 2, 1 };
            if (dir == Enums.Direction.Up) pos[1] -= 1;
            if (dir == Enums.Direction.Down) pos[1] += 1;
            if (dir == Enums.Direction.Left) pos[0] -= 1;
            if (dir == Enums.Direction.Right) pos[0] += 1;
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
