﻿using UnityEngine;
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
    bool hasShot = false;

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
                GetComponent<Animator>().SetBool("Idle2Walk", true);
                setFace(1);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                GetComponent<Animator>().SetBool("Idle2Walk2", true);
                setFace(-1);
            }
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                GetComponent<Animator>().SetBool("Idle2Walk", false);
                GetComponent<Animator>().SetBool("Idle2Walk2", false);
            }

            if (Input.GetButtonDown("Fire1") && controller.collisions.below)
            {
                velocity.y = jumpVelocity;
            }

            if (Input.GetButton("Fire2"))
            {
                Shoot();
                GetComponent<Animator>().SetBool("Idle2Throw", true);
                GetComponent<Animator>().SetBool("WalkToThrow", true);
            }
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void fermerAnimationThrow()
    {
        GetComponent<Animator>().SetBool("Idle2Throw", false);
        GetComponent<Animator>().SetBool("WalkToThrow", false);
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
            GetComponent<Animator>().SetBool("Idle2Walk", false);
            GetComponent<Animator>().SetBool("Idle2Walk2", false);
            fermerAnimationThrow();
            hasShot = false;
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

    void Shoot()
    {
        if(!hasShot)
        {
            hasShot = true;
            Aim aim = GetComponentInChildren<Aim>();
            AmmoInventory inventory = GameObject.FindGameObjectWithTag("World").GetComponent<AmmoInventory>();
            Ammo ammo = inventory.CreateAmmo(aim);
            ammo.setDirection(getFace());
            Vector2 force = new Vector3(aim.getX()*getFace(), aim.getY());
            ammo.GetComponent<Rigidbody2D>().AddForce(force*500);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag.Equals("Ammo"))
        {
            Ammo ammo = col.gameObject.GetComponent<Ammo>();
            if(!ammo.isTouched())
            {
                ammo.Touch();
                float targetVelocityX = 100f * ammo.getDirection();
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
        }
    }
}
