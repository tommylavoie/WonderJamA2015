using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartText : MonoBehaviour
{
    public Vector3 move;
    public Vector2 movementStartSelect;
    public float totalLoopTime;
    public float moveSpeed;
    private Vector3 velocity;
    private float time;

    // Use this for initialization
    void Start ()
    {
        time = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        velocity = move * Time.deltaTime;

        time += Time.deltaTime;

        if (time <= totalLoopTime / 2)
        {
            velocity.x = moveSpeed * movementStartSelect.x * Time.deltaTime;
            velocity.y = moveSpeed * movementStartSelect.y * Time.deltaTime;
        }
        else
        {
            velocity.x = -moveSpeed * movementStartSelect.x * Time.deltaTime;
            velocity.y = -moveSpeed * movementStartSelect.y * Time.deltaTime;
            if (time >= totalLoopTime)
            {
                time = 0.0f;
            }
        }

        transform.Translate(velocity);
    }
}
