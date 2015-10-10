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
            Debug.Log(player1choice);
            string player2choice = PlayerPrefs.GetString("Player2");
            Debug.Log(player2choice);
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
