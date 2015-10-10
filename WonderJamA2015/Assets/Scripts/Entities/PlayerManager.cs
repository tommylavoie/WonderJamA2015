using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public List<Player> players;

	// Use this for initialization
	void Start ()
    {
        string player1choice = PlayerPrefs.GetString("Player1");
        string player2choice = PlayerPrefs.GetString("Player2");
        foreach(Player player in players)
        {
            if (player.name.Equals(player1choice) || player.name.Equals(player2choice))
                player.setInGame(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
