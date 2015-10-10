using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    PlayerManager playerManager;
    public float TimePerTurn = 5;
    bool isEnding = false;

    int turn = -1;
    Player player;
    List<Player> playersInGame;
    DateTime turnTime = DateTime.Now;

    bool started = false;

	// Use this for initialization
	void Start ()
    {
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
        CameraScript cam = (CameraScript)(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>());
        EndGoal end = GameObject.FindGameObjectWithTag("Goal").GetComponent<EndGoal>();
        cam.DirectGoTo(new Vector3(end.transform.position.x, playerManager.players[0].transform.position.y, -10));
        Invoke("startGame", 4);
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
                FollowCameraToGoal(end, 2f);
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

    public void EndGame(Player winner)
    {
        if (!isEnding)
        {
            isEnding = true;
            Text text = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Text>();
            text.text = winner.name + " remporte les élections!";
            foreach(Player player in playerManager.players)
            {
                player.setInGame(false);
                player.setTurn(false);
            }
        }
    }
}
