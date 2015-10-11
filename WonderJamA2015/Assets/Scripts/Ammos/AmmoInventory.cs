using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoInventory : MonoBehaviour
{
    public List<GameObject> Ammos;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    GameObject getRandomAmmo()
    {
        int rand = Random.Range(0, Ammos.Count);
        return Ammos[rand];
    }

    public Ammo CreateAmmo(Aim aim)
    {
        GameObject ammo = getRandomAmmo();
        ammo.transform.position = new Vector2(aim.transform.position.x+(1*aim.getOwner().getFace()), aim.transform.position.y);
        GameObject go = Instantiate(ammo);
        return go.GetComponent<Ammo>();
    }
}
