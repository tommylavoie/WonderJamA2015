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
        if (controller.collisions.below)
        {
            velocity.y = 0;
            Collider2D obstacle = controller.collisions.transformHitBelow.GetComponent<Collider2D>();

            if (obstacle.gameObject.layer == LayerMask.NameToLayer("ObstacleThrough"))
            {
                obstacle.gameObject.layer = LayerMask.NameToLayer("Obstacle");
            }
        }

        if(controller.collisions.above)
        {
            /*Collider2D playerCollider = gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(controller.collisions.hit, playerCollider);*/
           
            Collider2D obstacle = controller.collisions.transformHitAbove.GetComponent<Collider2D>();   
            if (obstacle.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                obstacle.gameObject.layer = LayerMask.NameToLayer("ObstacleThrough");
            }
        }

        Vector2 input = new Vector2(0, 0);
        if (isTurn && inGame)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                GetComponent<Animator>().Play("Walk");
                setFace(1);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                GetComponent<Animator>().Play("Walk2");
                setFace(-1);
            }

            if (Input.GetButtonDown("Fire1") && controller.collisions.below)
            {
                //GetComponent<Animator>().Play("Jump");
                velocity.y = jumpVelocity;
            }
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    /*void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("hit with obstacle");
        }
    }*/

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
