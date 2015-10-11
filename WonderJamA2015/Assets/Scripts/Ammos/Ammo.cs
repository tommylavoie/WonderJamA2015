using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{
    bool touched = false;
    int direction = 1;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Touch()
    {
        touched = true;
    }

    public void setDirection(int direction)
    {
        this.direction = direction;
    }

    public bool isTouched()
    {
        return touched;
    }

    public int getDirection()
    {
        return direction;
    }
}
