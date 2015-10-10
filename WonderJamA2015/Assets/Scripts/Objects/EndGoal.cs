using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RaycastController))]
public class EndGoal : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("GOSH");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("YOU WON THE ERECTION!");
        }
    }
}
