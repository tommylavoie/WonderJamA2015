using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    PlayerManager playerManager;
    public float TimePerTurn = 5;

    int turn = -1;
    Player player;
    List<Player> playersInGame;
    DateTime turnTime = DateTime.Now;

	// Use this for initialization
	void Start ()
    {
        playerManager = GetComponent<PlayerManager>();
        player = playerManager.players[0];
        playersInGame = new List<Player>();
        foreach(Player p in playerManager.players)
        {
            if (p.getInGame())
                playersInGame.Add(p);
        }
        nextTurn();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Text text = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Text>();
        DateTime time = DateTime.Now;
        text.text = "Temps restant: " + Math.Max(TimePerTurn - time.Subtract(turnTime).TotalSeconds,0).ToString("0.00");
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

    public void EndTurn()
    {
        Debug.Log("NEXT TURN");
        nextTurn();
    }


}
