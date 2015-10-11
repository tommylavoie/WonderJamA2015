using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public string name = "";
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

    bool inGame = false;
    bool isTurn = false;
    int face = 1;
    int turnNumber = 0;

    public AudioClip Debut;
    public AudioClip Attack;
    public AudioClip Victoire;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
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
            if (obstacle.gameObject.layer == LayerMask.NameToLayer("Goal"))
            {
                TurnManager turnManager = GameObject.FindGameObjectWithTag("World").GetComponent<TurnManager>();
                turnManager.EndGame(this);
            }
        }

        if(controller.collisions.above)
        {      
            Collider2D obstacle = controller.collisions.transformHitAbove.GetComponent<Collider2D>();   
            if (obstacle.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                obstacle.gameObject.layer = LayerMask.NameToLayer("ObstacleThrough");
            }
            if (obstacle.gameObject.layer == LayerMask.NameToLayer("Goal"))
            {
                TurnManager turnManager = GameObject.FindGameObjectWithTag("World").GetComponent<TurnManager>();
                turnManager.EndGame(this);
            }
        }

        if (controller.collisions.left)
        {
            velocity.y = 0;
            Collider2D obstacle = controller.collisions.transformHitLeft.GetComponent<Collider2D>();

            if (obstacle.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                obstacle.gameObject.layer = LayerMask.NameToLayer("ObstacleThrough");
            }
            if (obstacle.gameObject.layer == LayerMask.NameToLayer("Goal"))
            {
                TurnManager turnManager = GameObject.FindGameObjectWithTag("World").GetComponent<TurnManager>();
                turnManager.EndGame(this);
            }
        }

        if (controller.collisions.right)
        {
            velocity.y = 0;
            Collider2D obstacle = controller.collisions.transformHitRight.GetComponent<Collider2D>();

            if (obstacle.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                obstacle.gameObject.layer = LayerMask.NameToLayer("ObstacleThrough");
            }
            if (obstacle.gameObject.layer == LayerMask.NameToLayer("Goal"))
            {
                TurnManager turnManager = GameObject.FindGameObjectWithTag("World").GetComponent<TurnManager>();
                turnManager.EndGame(this);
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
                velocity.y = jumpVelocity;
            }

            if (Input.GetButton("Fire2"))
            {
                GetComponent<Animator>().SetBool("Swag", true);
                //GetComponent<Animator>().Play("Throw");
            }
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void initTurn()
    {
        if(isTurn)
        {
            GetComponentInChildren<Aim>().gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponentInChildren<Aim>().gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void setInGame(bool inGame)
    {
        this.inGame = inGame;
    }

    public void setTurn(bool turn)
    {
        this.isTurn = turn;
        initTurn();
    }

    public void setFace(int face)
    {
        this.face = face;
    }

    public void setTurnNumber(int number)
    {
        this.turnNumber = number;
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

    public int getTurnNumber()
    {
        return turnNumber;
    }
}
