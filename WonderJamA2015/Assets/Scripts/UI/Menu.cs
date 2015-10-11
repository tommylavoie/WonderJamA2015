﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    public List<GameObject> faces;
    int actualCursor = 0;
    int step = 0;
    bool changing = false;
    bool firing = false;

    int numberOfPlayers = 2;
    int playerSelection = 0;
    int player1choice = -1;
    int player2choice = -1;
    int[] playerChoices;

    Text[] step1Texts;
    Text[] step2Texts;

    public bool OneJoystick;

    // Use this for initialization
    void Start ()
    {
        step1Texts = new Text[] { FindTextWithName("Jouer"), FindTextWithName("Quitter") };
        step2Texts = new Text[] { FindTextWithName("2Joueurs"), FindTextWithName("3Joueurs"), FindTextWithName("4Joueurs") };
    }
	
	// Update is called once per frame
	void Update ()
    {
        ManageInput();
    }

    Text FindTextWithName(string name)
    {
        GameObject[] texts = GameObject.FindGameObjectsWithTag("MenuText");
        foreach(GameObject textObject in texts)
        {
            if (textObject.GetComponent<MenuText>().Name.Equals(name))
                return textObject.GetComponent<Text>();
        }
        return null;
    }

    void ManageInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool fire = Input.GetButtonDown("Fire1");
        if (playerSelection == 1 && !OneJoystick)
        {
            horizontal = Input.GetAxis("HorizontalB");
            vertical = Input.GetAxis("VerticalB");
            fire = Input.GetButtonDown("Fire1B");
        }

        ManageStep0(horizontal, vertical, fire);
        ManageStep1(horizontal, vertical, fire);
        ManageStep2(horizontal, vertical, fire);
        ManageStep3(horizontal, vertical, fire);
    }

    void ManageStep0(float horizontal, float vertical, bool fire)
    {
        if (Input.GetButtonDown("Start"))
        {
            if (step == 0)
            {

                Text startText = FindTextWithName("StartPress");
                Text jouerText = FindTextWithName("Jouer");
                Text quitterText = FindTextWithName("Quitter");

                jouerText.enabled = true;
                quitterText.enabled = true;
                startText.enabled = false;
                overText(jouerText.gameObject);
                step = 1;
            }
        }
    }

    void ManageStep1(float horizontal, float vertical, bool fire)
    {
        if (step == 1)
        {
            if (vertical > 0 && !changing)
            {
                changing = true;
                unoverText(step1Texts[actualCursor].gameObject);
                actualCursor++;
                if (actualCursor >= 2)
                    actualCursor = 0;
                overText(step1Texts[actualCursor].gameObject);

            }
            else if (vertical < 0 && !changing)
            {
                changing = true;
                unoverText(step1Texts[actualCursor].gameObject);
                actualCursor--;
                if (actualCursor < 0)
                    actualCursor = 1;
                overText(step1Texts[actualCursor].gameObject);
            }
            else if (vertical == 0 && changing)
            {
                changing = false;
            }

            if (fire && !firing)
            {
                firing = true;
                if (actualCursor == 1)
                    Application.Quit();
                else
                {
                    actualCursor = 0;
                    Text jouerText = FindTextWithName("Jouer");
                    Text quitterText = FindTextWithName("Quitter");
                    Text joueurs2Text = FindTextWithName("2Joueurs");
                    Text joueurs3Text = FindTextWithName("3Joueurs");
                    Text joueurs4Text = FindTextWithName("4Joueurs");

                    jouerText.enabled = false;
                    quitterText.enabled = false;
                    joueurs2Text.enabled = true;
                    joueurs3Text.enabled = true;
                    joueurs4Text.enabled = true;
                    overText(joueurs2Text.gameObject);
                    step++;
                }
            }
            if (!fire)
                firing = false;
        }
    }

    void ManageStep2(float horizontal, float vertical, bool fire)
    {
        if (step == 2)
        {
            if (vertical < 0 && !changing)
            {
                changing = true;
                unoverText(step2Texts[actualCursor].gameObject);
                actualCursor++;
                if (actualCursor >= 3)
                    actualCursor = 0;
                overText(step2Texts[actualCursor].gameObject);

            }
            else if (vertical > 0 && !changing)
            {
                changing = true;
                unoverText(step2Texts[actualCursor].gameObject);
                actualCursor--;
                if (actualCursor < 0)
                    actualCursor = 2;
                overText(step2Texts[actualCursor].gameObject);
            }
            else if (vertical == 0 && changing)
            {
                changing = false;
            }

            if (fire && !firing)
            {
                firing = true;
                bool chose = false;
                if (actualCursor == 0)
                {
                    numberOfPlayers = 2;
                    chose = true;
                }
                else if (actualCursor == 1)
                {
                    numberOfPlayers = 3;
                    chose = true;
                }
                else
                {
                    numberOfPlayers = 4;
                    chose = true;
                }
                if(chose)
                {
                    chose = false;
                    playerChoices = new int[numberOfPlayers];
                    for (int i = 0; i < playerChoices.Length; i++)
                        playerChoices[i] = -1;
                    actualCursor = 0;
                    Text choiceText = FindTextWithName("Choisir");
                    Text joueurs2Text = FindTextWithName("2Joueurs");
                    Text joueurs3Text = FindTextWithName("3Joueurs");
                    Text joueurs4Text = FindTextWithName("4Joueurs");
                    joueurs2Text.enabled = false;
                    joueurs3Text.enabled = false;
                    joueurs4Text.enabled = false;
                    choiceText.enabled = true;
                    foreach (GameObject obj in faces)
                    {
                        obj.SetActive(true);
                    }
                    SpriteRenderer fleche = (SpriteRenderer)GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
                    fleche.enabled = true;
                    moveCursor();
                    step++;
                }
            }
            if (!fire)
                firing = false;
        }
    }

    void ManageStep3(float horizontal, float vertical, bool fire)
    {
        if (step == 3)
        {
            if (horizontal > 0 && !changing)
            {
                changing = true;
                nextCharacter();
            }
            else if (horizontal < 0 && !changing)
            {
                changing = true;
                precCharacter();
            }
            else if (horizontal == 0 && changing)
            {
                changing = false;
            }

            if (fire & !firing)
            {
                firing = true;
                chooseCharacter();
            }
            if (!fire)
                firing = false;
        }
    }

    void nextCharacter()
    {
        faces[actualCursor].GetComponent<MenuFace>().Unselect();
        bool correct = false;
        while(!correct)
        {
            actualCursor++;
            if (actualCursor == faces.Count)
                actualCursor = 0;
            correct = true;
            for (int i = 0; i < playerSelection; i++)
            {
                if (playerChoices[i] == actualCursor)
                    correct = false;
            }
        }
        moveCursor();  
    }

    void precCharacter()
    {
        faces[actualCursor].GetComponent<MenuFace>().Unselect();
        bool correct = false;
        while (!correct)
        {
            actualCursor--;
            if (actualCursor < 0)
                actualCursor = faces.Count - 1;
            correct = true;
            for (int i = 0; i < playerSelection; i++)
            {
                if (playerChoices[i] == actualCursor)
                    correct = false;
            }
        }
        moveCursor();
    }

    void moveCursor()
    {
        GameObject fleche = GameObject.FindGameObjectWithTag("Player");
        fleche.transform.position = new Vector3(faces[actualCursor].transform.position.x, faces[actualCursor].transform.position.y + 3);
        faces[actualCursor].GetComponent<MenuFace>().Select();
    }

    void overText(GameObject obj)
    {
        Text text = obj.GetComponent<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, .5f);
    }

    void unoverText(GameObject obj)
    {
        Text text = obj.GetComponent<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }

    void chooseCharacter()
    {
        faces[actualCursor].GetComponent<MenuFace>().Unselect();
        AudioSource audiosource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        audiosource.clip = faces[actualCursor].GetComponent<MenuFace>().citation;
        audiosource.loop = false;
        audiosource.Play();
        SpriteRenderer renderer = faces[actualCursor].GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, .5f);
        playerChoices[playerSelection] = actualCursor;
        nextPlayer();
        if(playerSelection >= numberOfPlayers)
        {
            step++;
            Invoke("startGame", 2) ;
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
            case 3: character = "Mulcair"; break;
            case 4: character = "Trudeau"; break;
        }
        return character;
    }

    void nextPlayer()
    {
        playerSelection++;
        actualCursor = -1;
        bool nextFound = false;
        int actual = -1;
        while(!nextFound)
        {
            actual++;
            nextFound = true;
            for(int i=0;i<playerSelection;i++)
            {
                if (playerChoices[i] == actual)
                    nextFound = false;
            }
        }
        actualCursor = actual;
        moveCursor();
    }

    void startGame()
    {
        string player1 = getCharacter(player1choice);
        string player2 = getCharacter(player2choice);
        PlayerPrefs.SetString("Player1", player1);
        PlayerPrefs.SetString("Player2", player2);
        Application.LoadLevel("MainScene");
    }
}
