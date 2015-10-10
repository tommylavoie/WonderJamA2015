using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;

    bool inGame = true;
    bool isTurn = false;
    int face = 1;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(0, 0);
        if (isTurn && inGame)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetButtonDown("Fire2") && controller.collisions.below)
            {
                velocity.y = jumpVelocity;
            }

            if(Input.GetAxisRaw("Horizontal") < 0)
            {
                setFace(-1);
            }
            else if(Input.GetAxisRaw("Horizontal") > 0)
            {
                setFace(1);
            }
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void setInGame(bool inGame)
    {
        this.inGame = inGame;
    }

    public void setTurn(bool turn)
    {
        this.isTurn = turn;
    }

    public void setFace(int face)
    {
        this.face = face;
    }

    public bool getInGame()
    {
        return inGame;
    }

    public bool getTurn()
    {
        return isTurn;
    }

    public int getFace()
    {
        return face;
    }
}
