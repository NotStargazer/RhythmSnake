using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    LinkedList.TransformLinkedList snake = new LinkedList.TransformLinkedList();

    Vector3 direction = new Vector3(0, -1);

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            snake.Append(child);
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("Up"))
        {
            if (snake[0].position + new Vector3(0, 1) != snake[1].position)
            {
                direction = new Vector3(0, 1);
            }
        }
        if (Input.GetButtonDown("Down"))
        {
            if (snake[0].position + new Vector3(0, -1) != snake[1].position)
            {
                direction = new Vector3(0, -1);
            }
        }
        if (Input.GetButtonDown("Left"))
        {
            if (snake[0].position + new Vector3(-1, 0) != snake[1].position)
            {
                direction = new Vector3(-1, 0);
            }
        }
        if (Input.GetButtonDown("Right"))
        {
            if (snake[0].position + new Vector3(1, 0) != snake[1].position)
            {
                direction = new Vector3(1, 0);
            }
        }
    }

    public bool MoveSnake()
    {
        foreach (Transform item in snake)
        {
            if (item.position == snake[0].position + direction)
            {
                return false;
            }
        }

        snake.MoveHead(direction);

        return true;
    }

    public void Grow()
    {
        Transform newBody = Instantiate(snake[snake.Count - 1], snake[snake.Count - 1].position, Quaternion.identity, transform);
        newBody.name = "Snake";
        snake.Append(newBody);
    }
}
