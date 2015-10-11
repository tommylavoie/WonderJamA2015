using UnityEngine;
using System.Collections;

public class MenuFace : MonoBehaviour
{
    public AudioClip citation;
    public Vector3 move;
    public Vector2 movementStartSelect;
    public float totalLoopTime;
    public float moveSpeed;
    private Vector3 velocity;
    private float time;
    bool selected = false;
    Vector3 initialPosition;

    // Use this for initialization
    void Start()
    {
        time = 0.0f;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
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
        else
        {
            transform.position = initialPosition;
        }
    }

    public void Select()
    {
        selected = true;
    }

    public void Unselect()
    {
        selected = false;
    }
}
