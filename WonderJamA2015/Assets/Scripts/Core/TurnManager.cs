using UnityEngine;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    PlayerManager playerManager;

    int turn = -1;
    Player player;
    List<Player> playersInGame;

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
        Invoke("EndTurn", 5);
    }

    public void EndTurn()
    {
        Debug.Log("NEXT TURN");
        nextTurn();
    }


}
