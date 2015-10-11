using UnityEngine;
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

    Image[] step1Texts;
    Image[] step2Texts;

    public List<GameObject> PEmblem;
    public GameObject Tuto1;
    public GameObject Tuto2;
    public bool OneJoystick;

    // Use this for initialization
    void Start ()
    {
        step1Texts = new Image[] { FindImageWithName("Jouer"), FindImageWithName("Quitter") };
        step2Texts = new Image[] { FindImageWithName("2Joueurs"), FindImageWithName("3Joueurs"), FindImageWithName("4Joueurs") };
    }
	
	// Update is called once per frame
	void Update ()
    {
        ManageInput();
    }

    Image FindImageWithName(string name)
    {
        GameObject[] texts = GameObject.FindGameObjectsWithTag("MenuText");
        foreach (GameObject textObject in texts)
        {
            if (textObject.GetComponent<MenuText>().Name.Equals(name))
                return textObject.GetComponent<Image>();
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
        if (playerSelection == 2 && !OneJoystick)
        {
            horizontal = Input.GetAxis("HorizontalC");
            vertical = Input.GetAxis("VerticalC");
            fire = Input.GetButtonDown("Fire1C");
        }
        if (playerSelection == 3 && !OneJoystick)
        {
            horizontal = Input.GetAxis("HorizontalD");
            vertical = Input.GetAxis("VerticalD");
            fire = Input.GetButtonDown("Fire1D");
        }

        ManageStep0(horizontal, vertical, fire);
        ManageStep1(horizontal, vertical, fire);
        ManageStep2(horizontal, vertical, fire);
        ManageStep3(horizontal, vertical, fire);
        ManageStepA(horizontal, vertical, fire);
        ManageStepB(horizontal, vertical, fire);
    }

    void ManageStep0(float horizontal, float vertical, bool fire)
    {
        if (Input.GetButtonDown("Start"))
        {
            if (step == 0 && !firing)
            {
                Image startText = FindImageWithName("StartPress");
                Tuto1.GetComponent<SpriteRenderer>().enabled = true;
                startText.enabled = false;
                step = 20;
            }
        }
    }

    void ManageStepA(float horizontal, float vertical, bool fire)
    {
        if (fire)
        {
            if (step == 20 && !firing)
            {
                firing = true;
                Tuto1.GetComponent<SpriteRenderer>().enabled = false;
                Tuto2.GetComponent<SpriteRenderer>().enabled = true;
                step = 21;
            }
        }
        else
            firing = false;
    }

    void ManageStepB(float horizontal, float vertical, bool fire)
    {
        if (fire)
        {
            if (step == 21 && !firing)
            {
                firing = true;
                Image jouerText = FindImageWithName("Jouer");
                Image quitterText = FindImageWithName("Quitter");
                Tuto2.GetComponent<SpriteRenderer>().enabled = false;

                jouerText.enabled = true;
                quitterText.enabled = true;
                overImage(jouerText.gameObject);
                step = 1;
            }
        }
        else
            firing = false;
    }

    void ManageStep1(float horizontal, float vertical, bool fire)
    {
        if (step == 1)
        {
            if (vertical < 0 && !changing)
            {
                changing = true;
                unoverImage(step1Texts[actualCursor].gameObject);
                actualCursor++;
                if (actualCursor >= 2)
                    actualCursor = 0;
                overImage(step1Texts[actualCursor].gameObject);

            }
            else if (vertical > 0 && !changing)
            {
                changing = true;
                unoverImage(step1Texts[actualCursor].gameObject);
                actualCursor--;
                if (actualCursor < 0)
                    actualCursor = 1;
                overImage(step1Texts[actualCursor].gameObject);
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
                    Image jouerText = FindImageWithName("Jouer");
                    Image quitterText = FindImageWithName("Quitter");
                    Image joueurs2Text = FindImageWithName("2Joueurs");
                    Image joueurs3Text = FindImageWithName("3Joueurs");
                    Image joueurs4Text = FindImageWithName("4Joueurs");

                    jouerText.enabled = false;
                    quitterText.enabled = false;
                    joueurs2Text.enabled = true;
                    joueurs3Text.enabled = true;
                    joueurs4Text.enabled = true;
                    overImage(joueurs2Text.gameObject);
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
                unoverImage(step2Texts[actualCursor].gameObject);
                actualCursor++;
                if (actualCursor >= 3)
                    actualCursor = 0;
                overImage(step2Texts[actualCursor].gameObject);

            }
            else if (vertical > 0 && !changing)
            {
                changing = true;
                unoverImage(step2Texts[actualCursor].gameObject);
                actualCursor--;
                if (actualCursor < 0)
                    actualCursor = 2;
                overImage(step2Texts[actualCursor].gameObject);
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
                    playerChoices = new int[4];
                    for (int i = 0; i < 4; i++)
                        playerChoices[i] = -1;
                    actualCursor = 0;
                    Image choiceText = FindImageWithName("Choisir");
                    Image joueurs2Text = FindImageWithName("2Joueurs");
                    Image joueurs3Text = FindImageWithName("3Joueurs");
                    Image joueurs4Text = FindImageWithName("4Joueurs");
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
        fleche.transform.position = new Vector3(faces[actualCursor].transform.position.x, faces[0].transform.position.y + 2.5f);
        faces[actualCursor].GetComponent<MenuFace>().Select();
    }

    void overImage(GameObject obj)
    {
        Image text = obj.GetComponent<Image>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }

    void unoverImage(GameObject obj)
    {
        Image text = obj.GetComponent<Image>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, .5f);
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
        placeEmblem(playerSelection);
        nextPlayer();
        if(playerSelection >= numberOfPlayers)
        {
            SpriteRenderer fleche = (SpriteRenderer)GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
            fleche.enabled = false;
            faces[actualCursor].GetComponent<MenuFace>().Unselect();
            step++;
            Invoke("startGame", 2) ;
        }
    }

    void placeEmblem(int player)
    {
        PEmblem[player].transform.position = new Vector2(faces[actualCursor].transform.position.x, faces[0].transform.position.y + 2.5f);
    }

    string getCharacter(int position)
    {
        string character = "None";
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
        string player1 = getCharacter(playerChoices[0]);
        string player2 = getCharacter(playerChoices[1]);
        string player3 = getCharacter(playerChoices[2]);
        string player4 = getCharacter(playerChoices[3]);
        PlayerPrefs.SetString("Player1", player1);
        PlayerPrefs.SetString("Player2", player2);
        PlayerPrefs.SetString("Player3", player3);
        PlayerPrefs.SetString("Player4", player4);

        Application.LoadLevel("MainScene");
    }
}
