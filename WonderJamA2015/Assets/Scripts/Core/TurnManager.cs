using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    AudioManager audioManager;
    PlayerManager playerManager;
    public float TimePerTurn = 5;
    bool isEnding = false;

    int turn = -1;
    Player player;
    List<Player> playersInGame;
    DateTime turnTime = DateTime.Now;

    bool started = false;

    public GameObject EndPlaque;
    public bool OneJoystick;

    public GameObject[] WinnerText;

    // Use this for initialization
    void Start ()
    {
        audioManager = GameObject.FindGameObjectWithTag("World").GetComponent<AudioManager>();
        playerManager = GetComponent<PlayerManager>();
        player = playerManager.players[0];
        playersInGame = new List<Player>();
        foreach(Player p in playerManager.players)
        {
            if (p.getInGame())
            {
                playersInGame.Add(p);
                p.setTurn(false);
            }
        }
        sortPlayers();
        CameraScript cam = (CameraScript)(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>());
        EndGoal end = GameObject.FindGameObjectWithTag("Goal").GetComponent<EndGoal>();
        cam.DirectGoTo(new Vector3(end.transform.position.x, playerManager.players[0].transform.position.y, -10));
        Invoke("startGame", 5);
    }

    void sortPlayers()
    {
        List<Player> temp = new List<Player>();
        foreach(Player p in playersInGame)
        {
            temp.Add(p);
        }
        foreach(Player p in temp)
        {
            playersInGame[p.getTurnNumber()] = p;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isEnding)
        {
            if (started)
            {
                FollowCameraToPlayer(player, 0.1f);
                Text text = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Text>();
                DateTime time = DateTime.Now;
                text.text = "Temps restant: " + Math.Max(TimePerTurn - time.Subtract(turnTime).TotalSeconds, 0).ToString("0.00");
            }
            else
            {
                EndGoal end = GameObject.FindGameObjectWithTag("Goal").GetComponent<EndGoal>();
                FollowCameraToGoal(end, 1f);
            }
        }
        else
        {
            if(Input.GetButtonDown("Start"))
            {
                Application.LoadLevel("Menu");
            }
        }
	}

    void startGame()
    {
        started = true;
        nextTurn();
    }

    void nextTurn()
    {
        if (turn >= playersInGame.Count-1)
        {
            turn = 0;
        }
        else
        {
            turn++;
        }
        player.setTurn(false);
        player = playersInGame[turn];
        player.setTurn(true);
        audioManager.PlaySound(player.Debut);
        turnTime = DateTime.Now;
        Invoke("EndTurn", TimePerTurn);
    }

    void FollowCameraToPlayer(Player player, float speed)
    {
        CameraScript cam = (CameraScript)(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>());
        cam.SeekCameraToPlayer(player, speed);
    }

    void FollowCameraToGoal(EndGoal player, float speed)
    {
        CameraScript cam = (CameraScript)(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>());
        cam.SeekCameraToGoal(player, speed);
    }

    public void EndTurn()
    {
        if(!isEnding)
            nextTurn();
    }

    void setWinnerText(Player winner)
    {
        if (winner.name.Equals("Duceppe"))
            WinnerText[0].GetComponent<Image>().enabled = true;
        if (winner.name.Equals("Harper"))
            WinnerText[1].GetComponent<Image>().enabled = true;
        if (winner.name.Equals("May"))
            WinnerText[2].GetComponent<Image>().enabled = true;
        if (winner.name.Equals("Mulcair"))
            WinnerText[3].GetComponent<Image>().enabled = true;
        if (winner.name.Equals("Trudeau"))
            WinnerText[4].GetComponent<Image>().enabled = true;
    }

    public void EndGame(Player winner)
    {
        if (!isEnding)
        {
            isEnding = true;
            EndPlaque.GetComponent<BoxCollider2D>().enabled = true;
            Text text = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Text>();
            GameObject goal = GameObject.FindGameObjectWithTag("Goal");
            goal.SetActive(false);
            //setWinnerText(winner);
            text.text = "";
            foreach(Player player in playerManager.players)
            {
                player.setInGame(false);
                player.setTurn(false);
            }
            audioManager.PlaySound(winner.Victoire);
            winner.GetComponent<Animator>().SetBool("Idle2Walk2", true);
        }
    }
}
