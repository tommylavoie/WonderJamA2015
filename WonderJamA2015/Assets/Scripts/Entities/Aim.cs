using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour
{
    Player owner;
    float y = 0;
    public float RAYON = 2;
    public float SENSIBILITY = 0.1f;
    public float ERROR_MARGIN = 0;

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

        Vector3 dir = owner.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if(owner.getFace() > 0)
            transform.localPosition = new Vector3(x + ERROR_MARGIN,y);
        else
            transform.localPosition = new Vector3(-x + ERROR_MARGIN, y);
    }

    void ManageInput()
    {
        float aimAxis = Input.GetAxis("RightV");
        if (owner.getTurnNumber() == 1 && !owner.getO())
        {
            aimAxis = Input.GetAxis("RightVB");
        }
        else if (owner.getTurnNumber() == 2 && !owner.getO())
        {
            aimAxis = Input.GetAxis("RightVC");
        }
        else if (owner.getTurnNumber() == 3 && !owner.getO())
        {
            aimAxis = Input.GetAxis("RightVD");
        }

        if (aimAxis != 0)
        {
            if(y <= RAYON)
                y -= aimAxis * SENSIBILITY;
            else if(y >= -RAYON)
                y += aimAxis * SENSIBILITY;
            if (y > RAYON)
                y = RAYON;
            else if (y < -RAYON)
                y = -RAYON;
        }
    }

    public float getX()
    {
        float x = Mathf.Sqrt(Mathf.Pow(RAYON, 2) - Mathf.Pow(y, 2));
        return x;
    }

    public float getY()
    {
        return y;
    }

    public Player getOwner()
    {
        return owner;
    }
}
