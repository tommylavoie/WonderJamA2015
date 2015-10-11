using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public bool AutomaticPlay = false;
    public List<Player> players;

	// Use this for initialization
	void Start ()
    {
        if (!AutomaticPlay)
        {
            GameObject[] nodes = GameObject.FindGameObjectsWithTag("Nodes");
            string player1choice = PlayerPrefs.GetString("Player1");
            string player2choice = PlayerPrefs.GetString("Player2");
            string player3choice = PlayerPrefs.GetString("Player3");
            string player4choice = PlayerPrefs.GetString("Player4");

            foreach (Player player in players)
            {
                if (player.name.Equals(player1choice))
                {
                    player.setInGame(true);
                    foreach(GameObject node in nodes)
                    {
                        if (node.GetComponent<StartNode>().Player == 0)
                            player.transform.position = node.transform.position;
                    }
                    player.setTurnNumber(0);
                }
                if (player.name.Equals(player2choice))
                {
                    player.setInGame(true);
                    foreach (GameObject node in nodes)
                    {
                        if (node.GetComponent<StartNode>().Player == 1)
                            player.transform.position = node.transform.position;
                    }
                    player.setTurnNumber(1);
                }
                if (player.name.Equals(player3choice))
                {
                    player.setInGame(true);
                    foreach (GameObject node in nodes)
                    {
                        if (node.GetComponent<StartNode>().Player == 2)
                            player.transform.position = node.transform.position;
                    }
                    player.setTurnNumber(2);
                }
                if (player.name.Equals(player4choice))
                {
                    player.setInGame(true);
                    foreach (GameObject node in nodes)
                    {
                        if (node.GetComponent<StartNode>().Player == 3)
                            player.transform.position = node.transform.position;
                    }
                    player.setTurnNumber(3);
                }
            }
        }
        else
        {
            foreach (Player player in players)
            {
                player.setInGame(true);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
