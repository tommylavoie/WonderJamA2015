using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
    int step = 0;
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        ManageInput();
    }

    void ManageInput()
    {
        if(Input.GetButtonDown("Start"))
        {
            if(step == 0)
            {
                Text startText = GetComponentInChildren<Text>();
                startText.enabled = false;
            }
        }
    }
}
