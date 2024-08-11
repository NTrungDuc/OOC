using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instace {  get { return instance; } }
    [SerializeField] private Rigidbody2D rb;
    public FixedJoystick joystick;
    [SerializeField] private float speed;
    public PlayerState playerState;
    //check out of control
    private bool canMoveUp = true;
    private bool canMoveDown = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;
    //limit move
    public float minX, maxX, minY, maxY;
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (playerState == PlayerState.Death)
        {
            return;
        }
        Movement();
    }
    void Movement()
    {
        Vector2 direction = joystick.Direction;

        if (!canMoveUp && direction.y > 0)
        {
            direction.y = 0;
        }
        if (!canMoveDown && direction.y < 0)
        {
            direction.y = 0;
        }
        if (!canMoveLeft && direction.x < 0)
        {
            direction.x = 0;
        }
        if (!canMoveRight && direction.x > 0)
        {
            direction.x = 0;
        }

        rb.velocity = direction * speed;
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY)
        );

        transform.position = clampedPosition;
    }
    public void SetDirectionLock(string direction)
    {
        canMoveUp = true;
        canMoveDown = true;
        canMoveLeft = true;
        canMoveRight = true;

        switch (direction.ToLower())
        {
            case "up":
                canMoveUp = false;
                break;
            case "down":
                canMoveDown = false;
                break;
            case "left":
                canMoveLeft = false;
                break;
            case "right":
                canMoveRight = false;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            UIManager.Instace.updateHP(10);
            Destroy(collision.gameObject);
        }
    }
}
public enum PlayerState
{
    Alive,
    Death
}
