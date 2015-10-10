using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    float magnitude = 0;
    bool grounded = true;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public bool IsGrounded()
    {
        return grounded;
    }

    public void AddMagnitude(float value)
    {
        magnitude += value;
    }

    public void AddMoveForce(Vector2 vector)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.AddForce(vector);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.transform.position.x < transform.position.x)
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.transform.position.x < transform.position.x)
        {
            grounded = false;
        }
    }
}
