using UnityEngine;
using System.Collections;

public class AmmoInventory : MonoBehaviour
{
    public GameObject ammo;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public Ammo CreateAmmo(Aim aim)
    {
        ammo.transform.position = aim.transform.position;
        GameObject go = Instantiate(ammo);
        return go.GetComponent<Ammo>();
    }
}
