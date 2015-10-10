using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    public List<GameObject> faces;
    int actualCursor = 0;
    int step = 0;
    bool changingCharacter = false;

    int playerSelection = 0;
    string player1choice = "";
    string player2choice = "";

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
                Text startText = GameObject.FindGameObjectWithTag("Respawn").GetComponentInChildren<Text>();
                Text choiceText = GameObject.FindGameObjectWithTag("Finish").GetComponentInChildren<Text>();
                startText.enabled = false;
                choiceText.enabled = true;
                foreach(GameObject obj in faces)
                {
                    obj.SetActive(true);
                }
                SpriteRenderer fleche = (SpriteRenderer)GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
                fleche.enabled = true;
                moveCursor();
                step = 1;
            }
        }
        if (step == 1)
        {
            if (Input.GetAxis("Horizontal") > 0 && !changingCharacter)
            {
                changingCharacter = true;
                nextCharacter();
            }
            else if (Input.GetAxis("Horizontal") < 0 && !changingCharacter)
            {
                changingCharacter = true;
                precCharacter();
            }
            else if(Input.GetAxis("Horizontal") == 0 && changingCharacter)
            {
                changingCharacter = false;
            }
        }
    }

    void nextCharacter()
    {
        if(actualCursor < faces.Count-1)
            actualCursor++;
        moveCursor();  
    }

    void precCharacter()
    {
        if (actualCursor > 0)
            actualCursor--;
        moveCursor();
    }

    void moveCursor()
    {
        GameObject fleche = GameObject.FindGameObjectWithTag("Player");
        fleche.transform.position = new Vector3(faces[actualCursor].transform.position.x, faces[actualCursor].transform.position.y + 3);
    }

    void chooseCharacter()
    {
        string choice = getCharacter(actualCursor);
        if(playerSelection == 0)
        {

        }
    }

    string getCharacter(int position)
    {
        string character = "";
        switch (position)
        {
            case 0: character = "Duceppe"; break;
            case 1: character = "Harper"; break;
            case 2: character = "May"; break;
            case 3: character = "Muclair"; break;
            case 4: character = "Trudeau"; break;
        }
        return character;
    }
}
