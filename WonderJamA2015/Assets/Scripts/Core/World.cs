using UnityEngine;
using System.Collections;

public class World : MonoBehaviour
{
    EntityManager entityManager;
    TurnManager turnManager;
    BlocManager blocManager;

	// Use this for initialization
	void Start ()
    {
        entityManager = new EntityManager();
        turnManager = new TurnManager();
        blocManager = new BlocManager();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
