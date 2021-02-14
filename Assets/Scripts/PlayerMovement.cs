using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection {
    IDLE = 0,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject worldMap;

    public float speed = 4.0f;

    private Animator anim;
    private MoveDirection moveDirection = MoveDirection.IDLE;
    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        maxX = worldMap.transform.position.x + worldMap.GetComponent<Renderer>().bounds.size.x / 2;
        minX = worldMap.transform.position.x - worldMap.GetComponent<Renderer>().bounds.size.x / 2;
        maxY = worldMap.transform.position.y + worldMap.GetComponent<Renderer>().bounds.size.y / 2;
        minY = worldMap.transform.position.y - worldMap.GetComponent<Renderer>().bounds.size.y / 2;
    }

    public void ButtonDown()
    {
        moveDirection = MoveDirection.DOWN;
    }

    public void ButtonUp()
    {
        moveDirection = MoveDirection.UP;
    }

    public void ButtonLeft()
    {
        moveDirection = MoveDirection.LEFT;
    }

    public void ButtonRight()
    {
        moveDirection = MoveDirection.RIGHT;
    }

    public void ButtonReleased()
    {
        moveDirection = MoveDirection.IDLE;
    }

    void Update()
    {
        float x_movement = 0.0f;
        float y_movement = 0.0f;
        if (moveDirection == MoveDirection.IDLE)
        {
            x_movement = Input.GetAxis("Horizontal");
            y_movement = Input.GetAxis("Vertical");
        }
        else
        {
            switch (moveDirection)
            {
                case MoveDirection.UP:
                {
                    y_movement = 1.0f;
                    break;
                }
                case MoveDirection.DOWN:
                {
                    y_movement = -1.0f;
                    break;
                }
                case MoveDirection.LEFT:
                {
                    x_movement = -1.0f;
                    break;
                    }
                case MoveDirection.RIGHT:
                {
                    x_movement = 1.0f;
                    break;
                }
                case MoveDirection.IDLE:
                default:
                {
                    break;
                }
            }
        }
        Vector3 newPosition = transform.position + new Vector3(x_movement, y_movement, 0.0f) * speed * Time.deltaTime;
        if (
            (newPosition.y < maxY) &&
            (newPosition.y > minY) &&
            (newPosition.x < maxX) &&
            (newPosition.x > minX)
        )
        {
            transform.position = newPosition;
            anim.SetFloat("x_movement", x_movement);
            anim.SetFloat("y_movement", y_movement);
        }
    }
}
