﻿using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour
{
    Player owner;
    public float y = 0;
    public float RAYON = 2;
    public float SENSIBILITY = 0.1f;

	// Use this for initialization
	void Start ()
    {
        owner = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ManageInput();
        float x = Mathf.Sqrt(Mathf.Pow(RAYON, 2) - Mathf.Pow(y, 2));
        if(owner.getFace() > 0)
            transform.localPosition = new Vector3(x,y);
        else
            transform.localPosition = new Vector3(-x, y);
    }

    void ManageInput()
    {
        float input = Input.GetAxis("RightV");
        if (input != 0)
        {
            if(y <= RAYON)
                y -= input * SENSIBILITY;
            else if(y >= -RAYON)
                y += input * SENSIBILITY;
            if (y > RAYON)
                y = RAYON;
            else if (y < -RAYON)
                y = -RAYON;
        }
    }
}