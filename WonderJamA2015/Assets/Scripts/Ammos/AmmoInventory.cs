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
        ammo.transform.position = new Vector2(aim.transform.position.x+(1*aim.getOwner().getFace()), aim.transform.position.y);
        GameObject go = Instantiate(ammo);
        return go.GetComponent<Ammo>();
    }
}
