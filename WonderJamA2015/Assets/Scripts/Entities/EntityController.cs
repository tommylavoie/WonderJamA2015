using UnityEngine;
using System.Collections;

public class EntityController : MonoBehaviour
{
    Entity owner;

    public float MoveXForce;
    public float JumpForce;

	// Use this for initialization
	void Start ()
    {
        owner = GetComponentInChildren<Entity>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = 0;
        if(moveX != 0)
        {
            moveX *= MoveXForce;
        }
        if(Input.GetButtonDown("Fire2") && owner.IsGrounded())
        {
            moveY = JumpForce;
        }
        owner.AddMoveForce(new Vector2(moveX, moveY));
    }
}
